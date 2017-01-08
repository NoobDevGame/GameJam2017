using NoobFight.Contract.Simulation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NoobFight.Core.Simulation.Events
{
    class EventManager
    {
        private static Dictionary<WorldEventType, Type> eventTypes;
        private static Dictionary<WorldEventType, Func<WorldEvent>> eventConstructors;
        static EventManager()
        {
            eventConstructors = new Dictionary<WorldEventType, Func<WorldEvent>>();
            eventTypes = new Dictionary<WorldEventType, Type>();
            foreach (var type in System.Reflection.Assembly.GetCallingAssembly().GetTypes())
                if (!type.IsAbstract && typeof(WorldEvent).IsAssignableFrom(type))
                    RegisterEvent(type);

        }

        public static void RegisterEvent<T>()
               where T : WorldEvent, new()
        {
            var type = typeof(T);
            var lambda = Expression.Lambda<Func<WorldEvent>>(Expression.New(type)).Compile();
            var id = new T().EventType;
            eventConstructors.Add(id, lambda);
            eventTypes.Add(id, type);
        }
        private static void RegisterEvent(Type type)
        {
            var lambda = Expression.Lambda<Func<WorldEvent>>(Expression.New(type)).Compile();
            var id = lambda().EventType;
            eventConstructors.Add(id, lambda);
            eventTypes.Add(id, type);
        }

        public static WorldEvent Deserialize(byte[] data)
        {
            if (data.Length == 1)
                return Deserialize((WorldEventType)data[0], null);

            byte[] payload = new byte[data.Length - 1];
            Array.Copy(data, 1, payload, 0, data.Length - 1);
            return Deserialize((WorldEventType)data[0], payload);
        }
        public static WorldEvent Deserialize(WorldEventType eventType, byte[] payload)
        {
            Func<WorldEvent> factory;
            if (!eventConstructors.TryGetValue(eventType, out factory))
                return null;

            var res = factory();
            res.Deserialize(payload);
            return res;
        }
    }
}
