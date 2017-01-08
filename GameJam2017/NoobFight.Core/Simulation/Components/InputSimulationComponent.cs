using System.Linq;
using System.Drawing;
using NoobFight.Contract;
using NoobFight.Contract.Map;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Simulation.Events;

namespace NoobFight.Core.Simulation.Components
{
    public class InputSimulationComponent : SimulationComponent
    {
        private IEntity EntityAtPosition(IPlayer player, Vector2 pos)
        {
            foreach (var entity in player.CurrentArea.Entities)
            {
                var relPos = entity.Position - player.Position;
                RectangleF recCheck = new RectangleF(relPos.X * 70.0f, relPos.Y * 70.0f, entity.Radius * 2 * 70.0f, entity.Height * 70f);
                //x=m-p-r
                if (recCheck.Contains(new PointF(pos.X, pos.Y)))
                    return entity;

            }
            return null;
        }

        private IActiveTile ActiveTileAtPosition(IPlayer player, Vector2 pos)
        {
            foreach (var tile in player.CurrentArea.ActiveTiles)
            {
                var tilePos = new RectangleF((tile.Region.X + player.Radius - player.Position.X) * 70f,
                                             (tile.Region.Y + tile.Region.Height - player.Height / 2 - player.Position.Y) * 70f,
                                              tile.Region.Width * 70f, tile.Region.Height * 70f);
                if (tilePos.Contains(new PointF(pos.X, pos.Y)))
                    return tile;
            }
            return null;
        }

        public override void SimulateWorld(IWorld world, GameTime gameTime)
        {
            foreach (var area in world.CurrentMap.Areas)
            {
                foreach (var player in area.Entities.OfType<IPlayer>())
                {
                    // Click auf die Area
                    if (player.Input.LeftClick)
                    {
                        var clickPos = player.Input.MousePosition;

                        var clickedActiveTile = ActiveTileAtPosition(player, clickPos);
                        var clickedEntity = EntityAtPosition(player, clickPos);

                        if (clickedActiveTile != null)
                        {
                            var manipulator = world.CreateNewManipulator();
                            manipulator.AddEvent(new ClickEvent(player, clickedActiveTile));
                        }

                        if (clickedEntity != null)
                        {
                            var manipulator = world.CreateNewManipulator();
                            manipulator.AddEvent(new ClickEvent(player, clickedEntity));
                        }
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