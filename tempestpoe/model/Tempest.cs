using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Jil;

namespace tempestpoe.model
{
  public class Tempest
  {

    public static Dictionary<string, string> tempestTable { get; set; }
    public static string[] Prefixes { get; set; }
    public static string[] Suffixes { get; set; }

    public string Name { get; set; }
    public string Map { get; set; }
    public string Prefix { get; set; }
    public string Suffix { get; set; }
    public string MapName
    {
      get
      {
        return tempestTable[Map];
      }

    }

    static Tempest()
    {
      var mapJson = File.ReadAllText("out.json");
      tempestTable = JSON.Deserialize<Dictionary<string, string>>(mapJson);

      var pre = File.ReadAllText("prefix.json");
      Prefixes = JSON.Deserialize<string[]>(pre);

      var suf = File.ReadAllText("suffix.json");
      Suffixes = JSON.Deserialize<string[]>(suf);
    }

  }
}
