using engenious;
using engenious.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Controls
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct MapVertex: IVertexType
    {
        public static readonly VertexDeclaration VertexDeclaration;
        static MapVertex()
        {
            VertexDeclaration = new VertexDeclaration();
        }
        VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;

        public Vector2 Position { get; private set; }
    }
}
