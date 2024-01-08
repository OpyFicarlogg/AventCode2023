using AventOfCode.Dto;
using AventOfCode.Dto.Day3;
using AventOfCode.Service.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AventOfCode.Service
{
    public class Day3 : IDay
    {
        private readonly Params param;

        public Day3(IOptions<Params> config)
        {
            param = config.Value;
        }

        public void Run()
        {
            PartTwo();
        }


        private void PartTwo()
        {
            Console.WriteLine(param.FilePathDay3);
            StreamReader streamReader = File.OpenText(param.FilePathDay3);

            List<int> lstExclude = new();
            List<Gear> lstGear = new List<Gear>();
            string[] lines = streamReader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int index = 0;
            int total = 0;
            foreach (string line in lines)
            {

                string preLine = null;
                string nextLine = null;

                //récupération de la ligne précédente
                if (index - 1 > 0)
                {
                    preLine = lines[index - 1];
                }

                //récupération de la ligne suivante
                if (index + 1 < lines.Length)
                {
                    nextLine = lines[index + 1];
                }



                List<int> lstNumber = GetAllNumberInALine(line);

                int idxInLine = 0;
                foreach (int number in lstNumber)
                {
                    bool validNumber = false;
                    int idxOf = line.IndexOf(number.ToString());


                    int n = 1;
                    while (idxOf < idxInLine)
                    {
                        idxOf = NthIndexOf(line, number.ToString(), n);
                        n++;
                    }

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(line.Substring(idxInLine, idxOf - idxInLine));

                    idxInLine = idxOf + number.ToString().Length;

                    //voir si adjacent à la ligne d'avant
                    if (preLine != null)
                    {
                        string value = GetAdjacentValue(preLine, idxOf, number);

                        if (DetectGear(value))
                        {
                            validNumber = true;
                            int indexOfGear = (idxOf !=0 ? idxOf -1 : idxOf )+ value.IndexOf("*");
                            lstGear.Add(new Gear(number, index-1, indexOfGear));
                        }
                    }

                    string curLineWithAdjacent = GetAdjacentValue(line, idxOf, number);

                    if (DetectGear(curLineWithAdjacent))
                    {
                        validNumber = true;
                        int indexOfGear =  curLineWithAdjacent.IndexOf("*")== 0 ? idxOf -1 : idxOf +number.ToString().Length;
                        lstGear.Add(new Gear(number, index, indexOfGear));
                    }

                    //Voir si adjacent à la ligne suivante 
                    if (nextLine != null)
                    {
                        string value = GetAdjacentValue(nextLine, idxOf, number);
                        if (DetectGear(value))
                        {
                            validNumber = true;
                            int indexOfGear = (idxOf != 0 ? idxOf - 1 : idxOf) + value.IndexOf("*");
                            lstGear.Add(new Gear(number, index+1, indexOfGear));
                        }
                    }

                    if (validNumber)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        total += number;

                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        lstExclude.Add(number);
                    }

                    Console.Write($"{number}");



                    //Console.WriteLine($"------------ {validNumber}------------------");
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("");
                //PB quand même nombre dans la même ligne
                Console.Write(line.Substring(idxInLine, line.Length - idxInLine));
                Console.WriteLine("");
                index++;
            }

            List<Gear> result = lstGear.GroupBy(g => new { g.line, g.index })
                          .Where(grp => grp.Count() > 1)
                          .SelectMany(grp => grp).ToList();

            // Afficher les résultats
            int tot = 0;
            foreach (Gear gear in result)
            {
                if (!gear.traite)
                {
                    List<Gear> sameGear = result.Where(g => g.line == gear.line && g.index == gear.index).Select(g => g).ToList();

                    if(sameGear.Count > 1)
                    {
                        int count = 0;
                        foreach (Gear g in sameGear)
                        {
                            count = count == 0 ? g.number : g.number * count;
                            g.traite = true;
                        }
                        Console.WriteLine(count);

                        tot += count;
                    }
                    
                }
                
                //result.Where(g => g.line == gear.line && g.index == gear.index).Select(g => g).
                //Console.WriteLine($"Number: {gear.number}, Line: {gear.line}, Index: {gear.index}");
            }

            Console.WriteLine(tot.ToString());

        }

        private void PartOne()
        {
            Console.WriteLine(param.FilePathDay3);
            StreamReader streamReader = File.OpenText(param.FilePathDay3);

            List<int> lstExclude = new();
            string[] lines = streamReader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int index = 0;
            int total = 0;
            foreach (string line in lines)
            {

                string preLine = null;
                string nextLine = null;

                //récupération de la ligne précédente
                if (index - 1 > 0)
                {
                    preLine = lines[index - 1];
                }

                //récupération de la ligne suivante
                if (index + 1 < lines.Length)
                {
                    nextLine = lines[index + 1];
                }



                List<int> lstNumber = GetAllNumberInALine(line);

                int idxInLine = 0;
                foreach (int number in lstNumber)
                {

                    bool validNumber = false;

                    int idxOf = line.IndexOf(number.ToString());


                    int n = 1;

                    while (idxOf < idxInLine)
                    {
                        idxOf = NthIndexOf(line, number.ToString(), n);
                        n++;
                    }

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(line.Substring(idxInLine, idxOf - idxInLine));

                    idxInLine = idxOf + number.ToString().Length;

                    //voir si adjacent à la ligne d'avant
                    if (preLine != null)
                    {
                        string value = GetAdjacentValue(preLine, idxOf, number);

                        if (DetectSpecialChar(value))
                        {
                            validNumber = true;
                        }
                    }

                    string curLineWithAdjacent = GetAdjacentValue(line, idxOf, number);

                    if (DetectSpecialChar(curLineWithAdjacent))
                    {
                        validNumber = true;
                    }

                    //Voir si adjacent à la ligne suivante 
                    if (nextLine != null)
                    {
                        string value = GetAdjacentValue(nextLine, idxOf, number);
                        if (DetectSpecialChar(value))
                        {
                            validNumber = true;
                        }
                    }

                    if (validNumber)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        total += number;

                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        lstExclude.Add(number);
                    }

                    Console.Write($"{number}");



                    //Console.WriteLine($"------------ {validNumber}------------------");
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("");
                //PB quand même nombre dans la même ligne
                Console.Write(line.Substring(idxInLine, line.Length - idxInLine));
                Console.WriteLine("");
                index++;
            }

            Console.WriteLine(total);
        }


        private string GetAdjacentValue(string line, int idxOf, int number)
        {
            if (line != null)
            {
                int startIndex = idxOf - 1 < 0 ? 0 : idxOf - 1;
                int endIndex = number.ToString().Length + (idxOf == 0 ? 1 : 2);
                endIndex = endIndex > line.Length ? line.Length : endIndex;

                endIndex = startIndex + endIndex <= line.Length ? endIndex : line.Length - startIndex;

                if (endIndex > 0)
                {
                    string ret = line.Substring(startIndex, endIndex);
                    //Console.WriteLine(ret);
                    return ret;
                }
            }
            return null;
        }

        private bool DetectSpecialChar(string text)
        {
            Regex regex = new(@"[^\w.]");

            return regex.IsMatch(text);
        }

        private bool DetectGear(string text)
        {
            return text.IndexOf("*") != -1;
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

        private int NthIndexOf(string target, string value, int n)
        {
            Match m = Regex.Match(target, "((" + Regex.Escape(value) + ").*?){" + n + "}");

            if (m.Success)
                return m.Groups[2].Captures[n - 1].Index;
            else
                return -1;
        }
    }
}
