using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScienceAssignment02_GeneticAlgorithms
{
    class GeneticAlgorithm
    {
        double crossoverRate;
        double mutationRate;
        bool elitism;
        int populationSize;
        int numIterations;
        Random r = new Random();

        public GeneticAlgorithm(double crossoverRate, double mutationRate, bool elitism, int populationSize, int numIterations)
        {
            this.crossoverRate = crossoverRate;
            this.mutationRate = mutationRate;
            this.elitism = elitism;
            this.populationSize = populationSize;
            this.numIterations = numIterations;
        }

        public Ind Run()
        {
            // initialize the first population
            var initialPopulation = Enumerable.Range(0, populationSize).Select(i => createIndividual()).ToArray();

            var currentPopulation = initialPopulation;

            for (int generation = 0; generation < numIterations; generation++)
            {
                // compute fitness of each individual in the population
                var fitnesses = Enumerable.Range(0, populationSize).Select(i => computeFitness(currentPopulation[i])).ToArray();

                var nextPopulation = new Ind[populationSize];

                // apply elitism
                int startIndex;
                if (elitism)
                {
                    startIndex = 1;
                    var populationWithFitness = currentPopulation.Select((individual, index) => new Tuple<Ind, double>(individual, fitnesses[index]));
                    var populationSorted = populationWithFitness.OrderByDescending(tuple => tuple.Item2); // item2 is the fitness
                    var bestIndividual = populationSorted.First();
                    nextPopulation[0] = bestIndividual.Item1;
                }
                else
                {
                    startIndex = 0;
                }

                // initialize the selection function given the current individuals and their fitnesses
                var getTwoParents = selectTwoParents(currentPopulation, fitnesses);

                // create the individuals of the next generation
                for (int newInd = startIndex; newInd < populationSize; newInd++)
                {
                    // select two parents
                    var parents = getTwoParents();

                    // do a crossover between the selected parents to generate two children (with a certain probability, crossover does not happen and the two parents are kept unchanged)
                    Tuple<Ind, Ind> offspring;
                    if (r.NextDouble() < crossoverRate)
                        offspring = crossover(parents);
                    else
                        offspring = parents;

                    // save the two children in the next population (after mutation)
                    nextPopulation[newInd++] = mutation(offspring.Item1, mutationRate);
                    if (newInd < populationSize) //there is still space for the second children inside the population
                        nextPopulation[newInd] = mutation(offspring.Item2, mutationRate);
                }

                // the new population becomes the current one
                currentPopulation = nextPopulation;

                // in case it's needed, check here some convergence condition to terminate the generations loop earlier
            }

            // recompute the fitnesses on the final population and return the best individual
            var finalFitnesses = Enumerable.Range(0, populationSize).Select(i => computeFitness(currentPopulation[i])).ToArray();
            Console.WriteLine("// After running the Genetic Algorithm \\");
            Console.WriteLine("Average Fitness of last population " + finalFitnesses.Average());
            Console.WriteLine("Best Fitness of last population " + finalFitnesses.OrderBy(x => x).Last());
            return currentPopulation.Select((individual, index) => new Tuple<Ind, double>(individual, finalFitnesses[index])).OrderByDescending(tuple => tuple.Item2).First().Item1;
        }

        /*  FUNCTIONS TO DEFINE (for each problem):
            Func<Ind> createIndividual;                                 ==> input is nothing, output is a new individual
            Func<Ind,double> computeFitness;                            ==> input is one individual, output is its fitness
            Func<Ind[],double[],Func<Tuple<Ind,Ind>>> selectTwoParents; ==> input is an array of individuals (population) and an array of corresponding fitnesses, output is a function which (without any input) returns a tuple with two individuals (parents)
            Func<Tuple<Ind, Ind>, Tuple<Ind, Ind>> crossover;           ==> input is a tuple with two individuals (parents), output is a tuple with two individuals (offspring/children)
            Func<Ind, double, Ind> mutation;                            ==> input is one individual and mutation rate, output is the mutated individual
        */

        private Ind createIndividual()
        {
            string individual = "";

            for (int i = 0; i < 5; i++)
            {
                if (r.NextDouble() > 0.5)
                    individual += 1;
                else
                    individual += 0;
            }

            return new Ind(individual);
        }

        private double computeFitness(Ind individual)
        {
            // -(Pow(x)) + 7x
            var fitness = -(Math.Pow(individual.value(), 2)) + (7 * individual.value());

            return fitness;
        }

        /* Roulette selection */
        private Func<Tuple<Ind, Ind>> selectTwoParents(Ind[] population, double[] fitnesses)
        {
            // Normalize the values
            var lowestFitness = fitnesses.OrderBy(x => x).First();
            var highestFitness = fitnesses.OrderBy(x => x).Last();
            double[] normalizedFitnesses = new double[fitnesses.Length];

            // All values are between 0-1
            for (var i = 0; i < fitnesses.Length; i++)
                normalizedFitnesses[i] = (fitnesses[i] - lowestFitness) / (highestFitness - lowestFitness);

            var cumProbability = 0.0;
            var totalProbability = normalizedFitnesses.Sum();

            var avgNormalizedFitnesses = Enumerable.Range(0, populationSize).Select(i => normalizedFitnesses[i] / totalProbability).ToArray();

            Ind parent1 = new Ind();
            Ind parent2 = new Ind();

            for (int i = 0; i < populationSize; i++)
            {
                cumProbability += normalizedFitnesses[i];
                if (cumProbability >= r.NextDouble())
                    if (parent1.value() == 0)
                        parent1 = population[i];
                    else
                    {
                        parent2 = population[i];
                        break;
                    }
            }

            return () => { return new Tuple<Ind, Ind>(parent1, parent2); };
        }

        // Create 2 childs from the parents
        private Tuple<Ind, Ind> crossover(Tuple<Ind, Ind> parents)
        {
            int cutoff = parents.Item1.binary.Length / 2;
            Ind child1 = new Ind();
            Ind child2 = new Ind();

            child1.binary = parents.Item1.binary.Substring(0, cutoff) + parents.Item2.binary.Substring(cutoff);
            child2.binary = parents.Item2.binary.Substring(0, cutoff) + parents.Item1.binary.Substring(cutoff);

            return new Tuple<Ind, Ind>(child1, child2);
        }

        private Ind mutation(Ind individual, double mutationRate)
        {
            StringBuilder mutation = new StringBuilder(individual.binary);

            for (int i = 0; i < 5; i++)
            {
                if (r.NextDouble() < mutationRate)
                {
                    if (individual.binary[i] == '0')
                        mutation[i] = '1';
                    else
                        mutation[i] = '0';
                }
            }

            individual.binary = mutation.ToString();
            return individual;
        }
    }
}
