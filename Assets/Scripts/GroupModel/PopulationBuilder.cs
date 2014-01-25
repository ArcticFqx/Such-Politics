using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Distributions;

class PopulationBuilder
{
    private static Random rand = new Random();

    public static readonly double LEFTOVER_TO_NEUTRAL = .80;

    /**
        * @numPlayers
        *      Includes neutral. Iff 2 human players, numPlayers should be 3.
        * 
        */
    public static double[][] buildPopulation(int populationSize, int numPlayers, double[] populationSpread, double mean, double variance) {
        double[][] population = new double[populationSize][];
        Normal normalDistribution = Normal.WithMeanVariance(mean, variance);

        for (int i = 0; i < populationSize; i++)
        {
            int favorite = getFavoriteFor(i, populationSize, populationSpread);
            population[i] = createIndividual(numPlayers, favorite, normalDistribution);
        }

            return population;
    }

    private static double[] createIndividual(int numPlayers, int favorite, Normal normalDistribution)
    {
        double[] individual = new double[numPlayers];
            
        double xBar = normalDistribution.Sample();
        double pVal = normalDistribution.CumulativeDistribution(xBar);
        UnityEngine.Debug.Log(individual.Length + " < " + favorite);
        individual[favorite] = pVal;

        // Spread the leftovers over the rest of the players, based on weight for leftover neutral
        double leftOver = 1 - individual[favorite];
        // TODO:
        //double leftOverForNeutral = LEFTOVER_TO_NEUTRAL * leftOver; 
        //double leftOverForRest = (leftOver - leftOverForNeutral) / (numPlayers - 2);

        for (int j = 0; j < numPlayers; j++)
        {
            if(j != favorite) {
                individual[j] = leftOver / (individual.Length - 1);
            }
        }

        return individual;
    }

    private static int getFavoriteFor(int individual, int populationSize, double[] populationSpread) {
        return rand.Next(populationSpread.Length);
    }
}