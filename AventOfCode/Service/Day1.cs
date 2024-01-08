using AventOfCode.Dto;
using AventOfCode.Service.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AventOfCode.Service
{
    public class Day1 : IDay
    {
        private readonly Params param;

        public Day1(IOptions<Params> config)
        {
            param = config.Value;
        }

        public void Run()
        {
            Console.WriteLine(param.FilePathDay1);
            StreamReader streamReader = File.OpenText(param.FilePathDay1);

            long total = 0;
            string[] lines = streamReader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                Console.WriteLine($"{line}");

                string firstNumber = GetFirstNumber(line);

                string lastNumber = Reverse(GetFirstNumber(Reverse(line)));

                total+= long.Parse($"{firstNumber}{lastNumber}");
                Console.WriteLine($"{firstNumber} || {lastNumber}");
            }

            Console.WriteLine(total);
            
        }


        private static string GetFirstNumber(string s)
        {
            Regex regex = new(@"\d{1}");
            Match match = regex.Match(s);
            return match.Success ? match.Value : "";
        }
        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
