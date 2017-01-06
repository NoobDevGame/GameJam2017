using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight
{
    class Program
    {
        static void Main(string[] args)
        {
            NoobFight game;
            using (game = new NoobFight())
                game.Run(60, 60);
        }
    }
}
