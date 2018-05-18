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
            Console.WriteLine("|| Genetic Algorithms ||");
            Console.WriteLine("");
            Console.WriteLine("The crossover and mutation rate should be between 0 and 1, where crossover should be big and mutation should be small");
            Console.WriteLine("");
            // Aks the user's info
            Console.Write("How large should the population be: ");
            var population = int.Parse(Console.ReadLine());
            Console.Write("What do you want the crossover rate be: ");
            var crossover = double.Parse(Console.ReadLine());
            Console.Write("What do you want the mutation rate be: ");
            var mutation = double.Parse(Console.ReadLine());
            Console.Write("How many iterations would you like to make: ");
            var iterations = int.Parse(Console.ReadLine());
            Console.WriteLine("");
            

            GeneticAlgorithm GA = new GeneticAlgorithm(crossover, mutation, true, population, iterations); // CHANGE THE PARAMETERS VALUES
            var solution = GA.Run();
            Console.WriteLine("Best Individual from the last population " + solution.binary);
            Console.ReadLine();
        }
    }
}
