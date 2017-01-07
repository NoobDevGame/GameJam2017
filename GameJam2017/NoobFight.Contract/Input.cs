using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Contract
{
    public struct Input
    {
        public bool MoveLeft;
        public bool MoveRight;
        public bool Jump;
        public bool LeftClick;
        public bool RightClick;
        Vector2 MousePosition;
    }
}