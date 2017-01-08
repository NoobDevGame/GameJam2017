using NoobFight.Contract;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Simulation.Events;

namespace NoobFight.Core.Simulation.Components
{
    public class InputSimulationComponent : SimulationComponent
    {
        public override void SimulateWorld(IWorld world, GameTime gameTime)
        {
            foreach (var area in world.CurrentMap.Areas)
            {
                foreach (var entity in area.Entities)
                {
                    if (entity is IPlayer)
                    {
                        var player = (IPlayer)entity;

                        // TODO: addEvent ClickEvent
                        if (false /* Wenn gelickt wurde */ )
                        {
                            var manipulator = world.CreateNewManipulator();
                            manipulator.AddEvent(new ClickEvent(null, player, new Vector2(0, 0)));
                        }

                        Vector2 velocity = new Vector2(0, player.Velocity.Y);

                        if (player.Input.MoveRight)
                            velocity += new Vector2(3, 0);

                        if (player.Input.MoveLeft)
                            velocity += new Vector2(-3, 0);


                        if (player.Input.Jump && player.OnGround)
                            velocity -= new Vector2(0, 9f);

                        player.Velocity = velocity;
                    }
                }
            }
        }
    }
}