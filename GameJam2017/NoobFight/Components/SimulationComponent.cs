using engenious;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Contract.Entities;
using NoobFight.Core.Map;

namespace NoobFight.Components
{
    public class SimulationComponent : GameComponent
    {
        public new NoobFight Game { get; private set; }
        public ISimulation Simulation { get; private set; }
        public IPlayer Player { get; set; }
        public IWorld World { get; set; }

        public SimulationComponent(NoobFight game) : base(game)
        {
            Game = game;


            //Player = Simulation.CreateLocalPlayer("Local Player", "monkey")
        }

        public void CreateSinglePlayerSimulation(GameMode gamemode, string texturename,string mapname)
        {
            Simulation = new Simulation(SimulationMode.Single);
            World = Simulation.CreateNewWorld(gamemode, "Default World");
            Player = Simulation.CreateLocalPlayer(1, "Hallo", texturename); ;
            World.AddPlayer(Player);
            var map = new Map(mapname);
            map.Load();
            World.Start(map);
        }

        public void CreateNetworkSimulation()
        {
            Simulation = new Simulation(SimulationMode.Single);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Simulation != null)
                Simulation.Update(new Contract.GameTime(gameTime.ElapsedGameTime));
        }
    }
}
