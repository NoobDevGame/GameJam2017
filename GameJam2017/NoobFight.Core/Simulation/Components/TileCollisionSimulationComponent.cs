using System.Drawing;
using NoobFight.Contract;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Simulation.Events;

namespace NoobFight.Core.Simulation.Components
{
    public class TileCollisionSimulationComponent : SimulationComponent
    {
        public override void SimulateWorld(IWorld world, GameTime gameTime)
        {
            foreach (var area in world.CurrentMap.Areas)
            {
                foreach (var entity in area.Entities)
                {
                    RectangleF recEntity = new RectangleF(entity.Position.X - entity.Radius
                        ,entity.Position.Y - entity.Height,entity.Radius * 2,entity.Height);

                    var @char = entity as ICharacter;
                     @char?.TileInteractionList.Clear();

                    foreach (var activeTile in area.ActiveTiles)
                    {
                        if (recEntity.IntersectsWith(activeTile.Region))
                        {
                            @char?.TileInteractionList.Add(activeTile);
                            world.Manipulator.AddEvent(new CollisionEvent(activeTile, entity));
                        }
                    }
                }
            }
        }
    }
}