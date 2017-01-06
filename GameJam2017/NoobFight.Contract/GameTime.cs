using System;

namespace NoobFight.Contract
{
    public struct GameTime
    {
        public TimeSpan ElapsedTime;

        public GameTime(TimeSpan elapsedTime)
        {
            ElapsedTime = elapsedTime;
        }
    }
}