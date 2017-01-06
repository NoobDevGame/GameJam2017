using NoobFight.Contract.Items;

namespace NoobFight.Core.Items
{
    public class Item : IItem
    {
        public Item(string name, string textureName)
        {
            Name = name;
            TextureName = textureName;
        }

        public string Name { get; private set; }
        public string TextureName { get; private set; }
    }
}