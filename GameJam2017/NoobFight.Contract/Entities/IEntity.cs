using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Contract.Map;
using NoobFight.Contract.Simulation;

namespace NoobFight.Contract.Entities
{
    public interface IEntity
    {
        string Name { get; }
        EntityType Type { get; }

        string TextureName { get; }

        float Radius { get; set; }
        float Height { get; set; }
        
        IArea CurrentArea { get; set; }

        bool OnGround { get; set; }

        Vector2 Position { get; set; }
        Vector2 Move { get; set; }
        Vector2 Velocity { get; set; }

        void OnClick(IWorldManipulator manipulator, IEntity entity);

        void OnEntityCollision(IWorldManipulator manipulator, IEntity collidedEntity);
    }
}
