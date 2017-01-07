﻿using NoobFight.Contract;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;

namespace NoobFight.Core.Simulation.Components
{
    public class InputSimulationComponent : SimulationComponent
    {
        public override void SimulateWorld(World world, GameTime gameTime)
        {
            foreach (var area in world.CurrentMap.Areas)
            {
                foreach (var entity in area.Entities)
                {
                    if (entity is IPlayer)
                    {
                        var player = (IPlayer) entity;

                        Vector2 velocity = new Vector2(0,player.Velocity.Y);

                        if (player.Input.MoveRight)
                            velocity += new Vector2(3,0);

                        if (player.Input.MoveLeft)
                            velocity += new Vector2(-3,0);


                        if(player.Input.Jump && player.OnGround)
                            velocity -= new Vector2(0,9f);

                        player.Velocity = velocity;
                    }
                }
            }
        }
    }
}