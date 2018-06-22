using System;
using System.Linq;
using WebAPIODataV4.Client.Default;

namespace WebAPIODataV4.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new SqliteContext(new Uri("http://localhost:60474/odata/"));
            context.Format.UseJson();

            // Call some basic Get
            var eventDataItems = context.EventData.ToList();
            var animalsItems = context.AnimalType.ToList();

            // This is the singleton object
            var skillLevels = context.SkillLevels.Expand("Levels").GetValue();
            var players = context.Player.Expand(c => c.PlayerStats).Where(u => u.PlayerStats.SkillLevel == 2).ToList();
        }
    }
}
