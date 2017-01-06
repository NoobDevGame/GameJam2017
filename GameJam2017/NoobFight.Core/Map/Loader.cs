using System.IO;
using Newtonsoft.Json;
using NoobFight.Contract.Map;

namespace NoobFight.Core.Map
{
    public class Loader
    {
        private string _Path;

        public Loader(string Path = null)
        {
            this._Path = GetPath(Path);
        }

        public void SaveMap(IMap map)
        {
            File.WriteAllText(this._Path, JsonConvert.SerializeObject(map));
        }

        public IMap LoadMap()
        {
            return JsonConvert.DeserializeObject<IMap>(File.ReadAllText(this._Path));
        }

        private string GetPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path = Path.Combine(Directory.GetCurrentDirectory(), "NoobFightMap");
            else
                return path;
        }
    }
}
