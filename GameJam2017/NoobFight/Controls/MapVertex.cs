﻿using engenious;
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
            VertexDeclaration = new VertexDeclaration(new VertexElement(0,VertexElementFormat.Single,VertexElementUsage.Position,0));
        }
        VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;

        public MapVertex(Vector2 position, Vector2 texturePosition, byte textureId)
        {
            PackedData = (uint)(((uint) position.X & 0xFF) << 24 | ((uint) position.Y & 0xFF) << 16 | textureId << 8 |
                         ((uint) texturePosition.X & 0x1)<<1 | ((uint) texturePosition.Y & 0x1));
        }
        public uint PackedData { get; private set; }
    }
}
