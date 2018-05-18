using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScienceAssignment02_GeneticAlgorithms
{
    class Ind
    {
        public string binary;

        public Ind() { }

        public Ind(string encoding)
        {
            this.binary = encoding;
        }

        public int value()
        {
            return Convert.ToInt32(binary, 2);
        }

        public string binaryString()
        {
            return binary;
        }
    }
}
