using System;
using NoobFight.Contract;
using NoobFight.Contract.Simulation;
using System.Linq;
using System.Collections.Generic;
using NoobFight.Contract.Map;
using NoobFight.Core.Simulation.Events;

namespace NoobFight.Core.Simulation.Components
{
    public class CollisionSimulationComponent : SimulationComponent
    {
        private float gap = 0.00005f;

        public override void SimulateWorld(World world, GameTime gameTime)
        {
            foreach (var area in world.CurrentMap.Areas)
            {
                foreach (var entity in area.Entities)
                {
                    bool collision = false;
                    int loops = 0;

                    entity.OnGround = false;

                    do
                    {
                        // Grenzbereiche für die zu überprüfenden Zellen ermitteln
                        Vector2 position = entity.Position + entity.Move;
                        int minCellX = (int)Math.Floor(position.X - entity.Radius);
                        int maxCellX = (int)Math.Ceiling(position.X + entity.Radius);
                        int minCellY = (int)Math.Floor(position.Y - entity.Height);
                        int maxCellY = (int)Math.Ceiling(position.Y);

                        collision = false;
                        float minImpact = 2f;
                        int minAxis = 0;

                        List<IActiveTile> collidingTiles = new List<IActiveTile>();

                        // Schleife über alle betroffenen Zellen zur Ermittlung der ersten Kollision
                        for (int x = minCellX; x <= maxCellX; x++)
                        {
                            for (int y = minCellY; y <= maxCellY; y++)
                            {
                                var activeTile = area.ActiveTiles.FirstOrDefault(tile => tile.Position.X == x && tile.Position.Y == y);

                                // Zellen ignorieren die vom Spieler nicht berührt werden
                                if (position.X - entity.Radius > x + 1 ||
                                    position.X + entity.Radius < x ||
                                    position.Y - entity.Height > y + 1 ||
                                    position.Y < y)
                                    continue;

                                if (activeTile != null)
                                {
                                    collidingTiles.Add(activeTile);
                                }


                                // Zellen ignorieren die den Spieler nicht blockieren
                                if (!area.IsCellBlocked(x, y))
                                    continue;

                                collision = true;


                                // Kollisionszeitpunkt auf X-Achse ermitteln
                                float diffX = float.MaxValue;
                                if (entity.Move.X > 0)
                                    diffX = position.X + entity.Radius - x + gap;
                                if (entity.Move.X < 0)
                                    diffX = position.X - entity.Radius - (x + 1) - gap;

                                float impactX = 1f - (diffX / entity.Move.X);



                                // Kollisionszeitpunkt auf Y-Achse ermitteln
                                float diffY = float.MaxValue;
                                if (entity.Move.Y > 0)
                                    diffY = position.Y /*+ entity.Height*/ - y + gap;
                                if (entity.Move.Y < 0)
                                    diffY = position.Y - entity.Height - (y + 1) - gap;

                                float impactY = 1f - (diffY / entity.Move.Y);

                                // Relevante Achse ermitteln
                                // Ergibt sich aus dem spätesten Kollisionszeitpunkt
                                int axis = 0;
                                float impact = 0;
                                if (impactX > impactY)
                                {
                                    axis = 1;
                                    impact = impactX;
                                }
                                else
                                {
                                    axis = 2;
                                    impact = impactY;
                                }

                                // Ist diese Kollision eher als die bisher erkannten
                                if (impact < minImpact)
                                {
                                    minImpact = impact;
                                    minAxis = axis;
                                }
                            }
                        }

                        if (collidingTiles.Count > 0)
                        {
                            foreach (var tile in collidingTiles)
                                world.AddEvent(new CollisionEvent(tile, entity));
                        }

                        // Im Falle einer Kollision in diesem Schleifendurchlauf...
                        if (collision)
                        {
                            // X-Anteil ab dem Kollisionszeitpunkt kürzen
                            if (minAxis == 1)
                            {
                                entity.Move *= new Vector2(minImpact, 1f);
                                entity.Velocity = new Vector2(0, entity.Velocity.Y);
                            }

                            // Y-Anteil ab dem Kollisionszeitpunkt kürzen
                            if (minAxis == 2)
                            {
                                entity.Move *= new Vector2(1f, minImpact);
                                entity.Velocity = new Vector2(entity.Velocity.X, 0);
                                if (minImpact > 0)
                                {
                                    entity.OnGround = true;
                                }
                            }
                        }
                        loops++;
                    } while (collision && loops < 2);

                    entity.Position += entity.Move;
                }
            }
        }
    }
}