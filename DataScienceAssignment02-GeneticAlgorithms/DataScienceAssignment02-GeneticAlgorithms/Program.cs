using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScienceAssignment02_GeneticAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            /* FUNCTIONS TO DEFINE (for each problem):
            Func<Ind> createIndividual;                                 ==> input is nothing, output is a new individual
            Func<Ind,double> computeFitness;                            ==> input is one individual, output is its fitness
            Func<Ind[],double[],Func<Tuple<Ind,Ind>>> selectTwoParents; ==> input is an array of individuals (population) and an array of corresponding fitnesses, output is a function which (without any input) returns a tuple with two individuals (parents)
            Func<Tuple<Ind, Ind>, Tuple<Ind, Ind>> crossover;           ==> input is a tuple with two individuals (parents), output is a tuple with two individuals (offspring/children)
            Func<Ind, double, Ind> mutation;                            ==> input is one individual and mutation rate, output is the mutated individual
            */

            GeneticAlgorithm GA = new GeneticAlgorithm(0.85, 0.01, true, 100, 10); // CHANGE THE PARAMETERS VALUES
            var solution = GA.Run();
            Console.WriteLine("Best Individual from the last population " + solution.binary);

            //Ind individual = new Ind("01110");

            //double[] fin = { 0, 1, 2, 3, 4, -14, -222, 250 };
            //var small = fin.OrderBy(x => x).Last();

            //var lowestFitness = fin.OrderBy(x => x).First();
            //var highestFitness = fin.OrderBy(x => x).Last();
            //double[] news = new double[fin.Length];

            //for (var i = 0; i < fin.Length; i++)
            //{
            //    news[i] = (fin[i] - lowestFitness) / (highestFitness - lowestFitness);
            //}

            //Console.WriteLine(individual.binary + " " + individual.value() + " " + individual.binary.Substring(individual.binary.Length /2));
            //Console.WriteLine(small);
            //for (int i = 0; i < news.Length; i++)
            //{
            //    Console.WriteLine(news[i]);
            //}
            Console.ReadLine();
        }
    }
}
