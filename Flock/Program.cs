using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flock.Client;
using Flock.Client.Client;
using Flock.Client.Params;
using Newtonsoft.Json;

namespace Flock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var content = File.ReadAllText("config.json");
            var configuration = JsonConvert.DeserializeObject<Configuration>(content);

            new Flock(configuration).Run();
        }
    }
}