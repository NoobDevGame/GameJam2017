using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Contract.Items
{
    public interface IItem
    {
        string Name { get; }
        string TextureName { get; }
    }
}
