using System.Drawing;
using System.Globalization;
using NoobFight.Contract;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Simulation.Events;

namespace NoobFight.Core.Simulation.Components
{
    public class EntityCollisionComponent : SimulationComponent
    {
        public override void SimulateWorld(IWorld world, GameTime gameTime)
        {
            foreach (var area in world.CurrentMap.Areas)
            {
                foreach (var entity in area.Entities)
                {
                    if (!(entity is ICharacter))
                        continue;

                    ICharacter @char = (ICharacter) entity;
                    @char.EntityInteractionList.Clear();

                    RectangleF recEntity = new RectangleF(entity.Position.X -  @char.InteractionRadius
                        , entity.Position.Y - entity.Height / 2 - @char.InteractionRadius, @char.InteractionRadius * 2, @char.InteractionRadius * 2);

                    foreach (var checkentity in area.Entities)
                    {
                        if (entity == checkentity)
                            continue;

                        RectangleF recCheck = new RectangleF(checkentity.Position.X - checkentity.Radius
                            , checkentity.Position.Y - checkentity.Height, checkentity.Radius * 2, checkentity.Height);

                        if (recEntity.IntersectsWith(recCheck))
                        {
                            @char.EntityInteractionList.Add(checkentity);
                            world.Manipulator.AddEvent(new EntityCollisionEvent(@char, checkentity));
                        }
                    }
                }
            }
        }
    }
}