using AventOfCode.Dto;
using AventOfCode.Service.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace AventOfCode.Service
{
    public class Day2 : IDay
    {
        private readonly Params param;
        

        public Day2(IOptions<Params> config)
        {
            param = config.Value;

        }

        public void Run()
        {
            Part1();

            Part2();
        }


        private void Part1()
        {
            Dictionary<string, int> refVal = new()
            {
                { "red", 12 },
                { "green", 13 },
                { "blue", 14 }
            };

            int total = 0;

            string[] lines = GetLines();
            foreach (string line in lines)
            {
                bool valid = true;

                int gameId = GetLineId(line);

                string[] throws = GetThrows(line);

                foreach (string lance in throws)
                {
                    Dictionary<string, int> map = GetDices(lance);

                    foreach (KeyValuePair<string, int> val in refVal)
                    {
                        if (map.ContainsKey(val.Key) && map[val.Key] > val.Value)
                        {
                            valid = false; break;
                        }
                    }
                }

                if (valid)
                {
                    total += gameId;
                }
            }

            Console.WriteLine($"TOTAL : {total}");
        }

        private void Part2()
        {
            int total = 0;

            string[] lines = GetLines();
            foreach (string line in lines)
            {
                string[] throws = GetThrows(line);

                Dictionary<string, int> maxVal = new();
                foreach (string lance in throws)
                {                    
                    Dictionary<string, int> map = GetDices(lance);

                    foreach(KeyValuePair<string, int> val in map)
                    {
                        if (maxVal.ContainsKey(val.Key))
                        {
                            if (maxVal[val.Key] < val.Value) {
                                maxVal[val.Key] = val.Value;
                            }    
                        }
                        else
                        {
                            maxVal.Add(val.Key, val.Value);
                        }
                    }
                }

                total+= maxVal.Aggregate(0, (acc, x) => acc ==0 ? x.Value : acc * x.Value);
            }

            Console.WriteLine($"TOTAL : {total}");
        }



        private int GetLineId(string line)
        {
            string[] game = line.Split(":");
            return  int.Parse(GetFirstNumber(game[0]));
        }

        private Dictionary<string, int> GetDices(string lance)
        {
            string[] dices = lance.Split(",");
            Dictionary<string, int> map = new Dictionary<string, int>();

            foreach (string dice in dices)
            {
                string[] diceVal = dice.Trim().Split(" ");
                map.Add(diceVal[1].ToLower(), int.Parse(diceVal[0]));
            }
            return map;
        }

        private string[] GetThrows(string line)
        {
            string[] game = line.Split(":");

            return game[1].Split(";");
        }

        private string[] GetLines()
        {
            Console.WriteLine(param.FilePathDay2);
            StreamReader streamReader = File.OpenText(param.FilePathDay2);


            return streamReader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        private static string GetFirstNumber(string s)
        {
            Regex regex = new(@"\d+");
            Match match = regex.Match(s);
            return match.Success ? match.Value : "";
        }
    }
}
