using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public class Trait
    {
        public static List<Trait> TraitList = new List<Trait>();


        public string Name { get; private set; }
        public string Description { get; private set; }

        public Trait(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }

        static class TraitReader
        {
            public static bool read = false;
            static TraitReader()
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("WFRP_NPC_Creator.Data.traits.txt"));
                string[] traitData = sr.ReadToEnd().Split('\n');
                string name, description;
                List<int> timeMarkers = new List<int>();
                for (int i = 0; i < traitData.Length; i++)
                {
                    if (traitData[i].Contains("12:"))
                        timeMarkers.Add(i);
                }
            try
            {
                for (int i = 0; i < timeMarkers.Count; i++)
                {
                    description = "";
                    name = traitData[timeMarkers[i] - 3];
                    for (int j = timeMarkers[i] + 2; j < timeMarkers[i + 1] - 3; j++)
                        description += traitData[j];
                    Trait.TraitList.Add(new Trait(name, description));
                }
            }
            catch { }

                sr.Close();

            }
        }
    }
