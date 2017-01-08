using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Contract.Simulation
{
    public enum WorldEventType : int
    {
        None = 0,
        AreaChange = 1,
        Collision = 2,
        PlayerWorld = 3,
        Click = 4,
        EntityCollision = 5,
    }
}
