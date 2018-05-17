﻿using System;
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

            //GeneticAlgorithm fakeProblemGA = new GeneticAlgorithm(0.0, 0.0, false, 0, 0); // CHANGE THE PARAMETERS VALUES
            //var solution = fakeProblemGA.Run();
            //Console.WriteLine("Solution: ");
            //Console.WriteLine(solution);

            Ind individual = new Ind("01010");
            Console.WriteLine(individual.binary + " " + individual.value());
            Console.ReadLine();
        }
    }
}