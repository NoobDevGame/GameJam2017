using System.Drawing;
using NoobFight.Contract.Map;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;
using NoobFight.Contract;
using System;

namespace NoobFight.Core.Map.Tiles
{
    public abstract class ActiveTile : IActiveTile
    {
        public RectangleF Region { get; private set; }

        public TileProperty Property { get; private set; }

        public ActiveTile(RectangleF region, TileProperty property)
        {
            this.Region = region;
            this.Property = property;
        }

        public virtual void OnAttack(IWorldManipulator manipulator, IEntity entity)
        {

        }

        public virtual void OnCollision(IWorldManipulator manipulator, IEntity entity)
        {

        }

        public virtual void OnClick(IWorldManipulator manipulator, IEntity entity)
        {

        }
    }
}
