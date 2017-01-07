using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Contract
{
    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator +(Vector2 vec1, Vector2 vec2)
        {
            return new Vector2(vec1.X+ vec2.X,vec1.Y + vec2.Y);
        }

        public static Vector2 operator -(Vector2 vec1, Vector2 vec2)
        {
            return new Vector2(vec1.X- vec2.X,vec1.Y - vec2.Y);
        }

        public static Vector2 operator * (Vector2 vec1, Vector2 vec2)
        {
            return new Vector2(vec1.X * vec2.X,vec1.Y *  vec2.Y);
        }

        public static Vector2 operator * (Vector2 vec1, float value)
        {
            return new Vector2(vec1.X * value,vec1.Y *  value);
        }



        public static Vector2 operator * (Vector2 vec1, double value)
        {
            return new Vector2(vec1.X * (float)value,vec1.Y *  (float)value);
        }
    }
}