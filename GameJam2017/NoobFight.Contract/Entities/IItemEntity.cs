using NoobFight.Contract.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Contract.Entities
{
    public interface IItemEntity : IEntity
    {
        IItem Item { get; }
    }
}
