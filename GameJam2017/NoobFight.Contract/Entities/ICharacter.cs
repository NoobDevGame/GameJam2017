using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Contract.Map;

namespace NoobFight.Contract.Entities
{
    public interface ICharacter : IEntity
    {
        int Health { get; set; }

        float InteractionRadius { get; set; }

        List<IEntity> EntityInteractionList { get; }
        List<IActiveTile> TileInteractionList { get; }
    }
}
