using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AventOfCode.Dto.Day3
{
    public class Gear
    {
        public Gear(int number, int line, int index)
        {
            this.number = number;
            this.line = line;
            this.index = index;
            this.traite = false;
        }

        public int number {  get; set; }    
        public int line { get; set; }
        public int index { get; set; }
        public bool traite { get; set; }
    }
}
