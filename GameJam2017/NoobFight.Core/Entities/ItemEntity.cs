﻿using NoobFight.Contract;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Items;

namespace NoobFight.Core.Entities
{
    public class ItemEntity : Entity , IItemEntity
    {
        public ItemEntity(string name, string textureName, IItem item) : base(name, textureName)
        {
            Item = item;
        }

        public IItem Item { get; private set; }
    }
}