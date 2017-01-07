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
        public uint PackedData { get; private set; }

        public static readonly VertexDeclaration VertexDeclaration;

        VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;

        static MapVertex()
        {
            VertexDeclaration = new VertexDeclaration();
        }
        
        //public MapVertex(Vector2 position, Vector2 texturePosition, byte textureId)
        //{

        //}
    }
}
