using AventOfCode.Dto;
using AventOfCode.Service.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AventOfCode.Service
{
    public class Day4 : IDay
    {
        private readonly Params param;

        public Day4(IOptions<Params> config)
        {
            param = config.Value;
        }

        public void Run()
        {
            PartOne();
        }

        private void PartOne()
        {
            Console.WriteLine(param.FilePathDay4);
            StreamReader streamReader = File.OpenText(param.FilePathDay4);

            string[] lines = streamReader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int index = 0;
            int total = 0;
            foreach (string line in lines)
            {
                int lineTotal = 0;

                string[] card = line.Split(":")[1].Split("|");
                List<int> lstWinningNumber = GetAllNumberInALine(card[0]);
                List<int> lstNumber = GetAllNumberInALine(card[1]);

                List<int> duplicates = lstWinningNumber.Intersect(lstNumber).ToList();

                if(duplicates.Count > 0)
                {
                    lineTotal = duplicates.Count > 1 ?  Pow(2,duplicates.Count-1)  : 1;
                }

                total += lineTotal;
                
                index++;
            }

            Console.WriteLine($"Total : {total}");
        }



        private void PartTwo()
        {
            Console.WriteLine(param.FilePathDay4);
            StreamReader streamReader = File.OpenText(param.FilePathDay4);

            string[] lines = streamReader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int index = 0;
            int total = 0;
            foreach (string line in lines)
            {

                
                index++;
            }
        }

        private int Pow(int bas, int exp)
        {
            return Enumerable
                  .Repeat(bas, exp)
                  .Aggregate(1, (a, b) => a * b);
        }

        private List<int> GetAllNumberInALine(string line)
        {
            List<int> lstNumber = new();

            Regex regex = new(@"\d+");

            foreach (Match match in regex.Matches(line))
            {
                lstNumber.Add(int.Parse(match.Value));
            }
            return lstNumber;
        }
    }
}
