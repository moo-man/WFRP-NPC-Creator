using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace WFRP_JSON_Parser
{
    class Program
    {
        static void Main(string[] args)
        {

            Career career = new Career("input.txt");
            career.AppendToJSON("careers.json");
        }
    }

    public class Career
    {
        string careerName;
        string[] attributes;
        int[] advanceMarkers;

        string tier1Name, tier2Name, tier3Name, tier4Name;
        string[] tier1Skills, tier2Skills, tier3Skills, tier4Skills;
        string[] tier1Talents, tier2Talents, tier3Talents, tier4Talents;
        string[] tier1Trappings, tier2Trappings, tier3Trappings, tier4Trappings;
        string tier1Standing, tier2Standing, tier3Standing, tier4Standing;
        string tier1StatusTier, tier2StatusTier, tier3StatusTier, tier4StatusTier;

        public Career(string filename)
        {
            StreamReader input = new StreamReader(filename);

            string inputLine = input.ReadLine();
            if (inputLine != "")
                careerName = inputLine;

            while (inputLine.Length < 2 || inputLine.Substring(0, 2) != "WS")
                inputLine = input.ReadLine();

            attributes = inputLine.Split('\t');
            advanceMarkers = new int[attributes.Length];

            inputLine = input.ReadLine();

            for (int i = 0; i < inputLine.Length; i++)
            {
                advanceMarkers[i] = Int32.Parse(inputLine[i].ToString());
            }

            while (inputLine != "Career Path")
                inputLine = input.ReadLine();

            inputLine = input.ReadLine();

            tier1Name = inputLine.Split(new string[] { " - " }, StringSplitOptions.None)[0].Substring(2).Trim();
            tier1StatusTier = inputLine.Split(new string[] { " - " }, StringSplitOptions.None)[1].Split(' ')[0];
            tier1Standing = inputLine.Split(new string[] { " - " }, StringSplitOptions.None)[1].Split(' ')[1];
            tier1Skills = input.ReadLine().Substring(8).Split(new string[] { ", " }, StringSplitOptions.None);
            tier1Talents = input.ReadLine().Substring(9).Split(new string[] { ", " }, StringSplitOptions.None);
            tier1Trappings = input.ReadLine().Substring(11).Split(new string[] { ", " }, StringSplitOptions.None);

            inputLine = input.ReadLine();

            inputLine = input.ReadLine();
            tier2Name = inputLine.Split(new string[] { " - " }, StringSplitOptions.None)[0].Substring(2).Trim();
            tier2StatusTier = inputLine.Split(new string[] { " - " }, StringSplitOptions.None)[1].Split(' ')[0];
            tier2Standing = inputLine.Split(new string[] { " - " }, StringSplitOptions.None)[1].Split(' ')[1];
            tier2Skills = input.ReadLine().Substring(8).Split(new string[] { ", " }, StringSplitOptions.None);
            tier2Talents = input.ReadLine().Substring(9).Split(new string[] { ", " }, StringSplitOptions.None);
            tier2Trappings = input.ReadLine().Substring(11).Split(new string[] { ", " }, StringSplitOptions.None);

            inputLine = input.ReadLine();

            inputLine = input.ReadLine();
            tier3Name = inputLine.Split(new string[] { " - " }, StringSplitOptions.None)[0].Substring(2).Trim();
            tier3StatusTier = inputLine.Split(new string[] { " - " }, StringSplitOptions.None)[1].Split(' ')[0];
            tier3Standing = inputLine.Split(new string[] { " - " }, StringSplitOptions.None)[1].Split(' ')[1];
            tier3Skills = input.ReadLine().Substring(8).Split(new string[] { ", " }, StringSplitOptions.None);
            tier3Talents = input.ReadLine().Substring(9).Split(new string[] { ", " }, StringSplitOptions.None);
            tier3Trappings = input.ReadLine().Substring(11).Split(new string[] { ", " }, StringSplitOptions.None);

            inputLine = input.ReadLine();

            inputLine = input.ReadLine();
            tier4Name = inputLine.Split(new string[] { " - " }, StringSplitOptions.None)[0].Substring(2).Trim();
            tier4StatusTier = inputLine.Split(new string[] { " - " }, StringSplitOptions.None)[1].Split(' ')[0];
            tier4Standing = inputLine.Split(new string[] { " - " }, StringSplitOptions.None)[1].Split(' ')[1];
            tier4Skills = input.ReadLine().Substring(8).Split(new string[] { ", " }, StringSplitOptions.None);
            tier4Talents = input.ReadLine().Substring(9).Split(new string[] { ", " }, StringSplitOptions.None);
            tier4Trappings = input.ReadLine().Substring(11).Split(new string[] { ", " }, StringSplitOptions.None);

            tier1Skills[tier1Skills.Length - 1] = tier1Skills[tier1Skills.Length - 1].Trim();
            tier1Talents[tier1Talents.Length - 1] = tier1Talents[tier1Talents.Length - 1].Trim();
            tier1Trappings[tier1Trappings.Length - 1] = tier1Trappings[tier1Trappings.Length - 1].Trim();

            tier2Skills[tier2Skills.Length - 1] = tier2Skills[tier2Skills.Length - 1].Trim();
            tier2Talents[tier2Talents.Length - 1] = tier2Talents[tier2Talents.Length - 1].Trim();
            tier2Trappings[tier2Trappings.Length - 1] = tier2Trappings[tier2Trappings.Length - 1].Trim();

            tier3Skills[tier3Skills.Length - 1] = tier3Skills[tier3Skills.Length - 1].Trim();
            tier3Talents[tier3Talents.Length - 1] = tier3Talents[tier3Talents.Length - 1].Trim();
            tier3Trappings[tier3Trappings.Length - 1] = tier3Trappings[tier3Trappings.Length - 1].Trim();

            tier4Skills[tier4Skills.Length - 1] = tier4Skills[tier4Skills.Length - 1].Trim();
            tier4Talents[tier4Talents.Length - 1] = tier4Talents[tier4Talents.Length - 1].Trim();
            tier4Trappings[tier4Trappings.Length - 1] = tier4Trappings[tier4Trappings.Length - 1].Trim();

            input.Close();

        }

        public void AppendToJSON(string filename)
        {
            StreamWriter output = File.AppendText(filename);

            output.WriteLine("\"{0}\":{{", careerName);
            output.WriteLine("\"Attributes\": {");

            for (int i = 0; i < attributes.Length; i++)
            {
                output.WriteLine("\"{0}\": {1},", attributes[i], advanceMarkers[i]);
            }
            output.WriteLine("},");
            output.WriteLine("\"Tiers\": [\nnull,\n");

            output.WriteLine("{{\"Name\":\"{0}\",", tier1Name);
            output.WriteLine("\"Status\": {");
            output.WriteLine("\"Tier\": \"{0}\",", tier1StatusTier);
            output.WriteLine("\"Standing\": \"{0}\"}},", tier1Standing);
            output.WriteLine("\"Skills\":[" +   PrintToJson(tier1Skills) + "],");
            output.WriteLine("\"Talents\":[" +  PrintToJson(tier1Talents) + "],");
            output.WriteLine("\"Trappings\":["+ PrintToJson(tier1Trappings) + "]");
            output.WriteLine("},");


            output.WriteLine("{{\"Name\":\"{0}\",", tier2Name);
            output.WriteLine("\"Status\": {");
            output.WriteLine("\"Tier\": \"{0}\",", tier2StatusTier);
            output.WriteLine("\"Standing\": \"{0}\"}},", tier2Standing);
            output.WriteLine("\"Skills\":[" + PrintToJson(tier2Skills) + "],");
            output.WriteLine("\"Talents\":[" + PrintToJson(tier2Talents) + "],");
            output.WriteLine("\"Trappings\":[" + PrintToJson(tier2Trappings) + "]");
            output.WriteLine("},");



            output.WriteLine("{{\"Name\":\"{0}\",", tier3Name);
            output.WriteLine("\"Status\": {");
            output.WriteLine("\"Tier\": \"{0}\",", tier3StatusTier);
            output.WriteLine("\"Standing\": \"{0}\"}},", tier3Standing);
            output.WriteLine("\"Skills\":[" + PrintToJson(tier3Skills) + "],");
            output.WriteLine("\"Talents\":[" + PrintToJson(tier3Talents) + "],");
            output.WriteLine("\"Trappings\":[" + PrintToJson(tier3Trappings) + "]");
            output.WriteLine("},");



            output.WriteLine("{{\"Name\":\"{0}\",", tier4Name);
            output.WriteLine("\"Status\": {");
            output.WriteLine("\"Tier\": \"{0}\",", tier4StatusTier);
            output.WriteLine("\"Standing\": \"{0}\"}},", tier4Standing);
            output.WriteLine("\"Skills\":[" + PrintToJson(tier4Skills) + "],");
            output.WriteLine("\"Talents\":[" + PrintToJson(tier4Talents) + "],");
            output.WriteLine("\"Trappings\":[" + PrintToJson(tier4Trappings) + "]");
            output.WriteLine("},"); // tier close
            output.WriteLine("]"); // tiers close
            output.WriteLine("},");//career close



            output.Close();
        }

        private string PrintToJson(string [] array)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                sb.Append("\"" + array[i] + "\"");
                if (i != array.Length - 1)
                    sb.Append(", ");
            }
            return sb.ToString();
        }

    }
}
