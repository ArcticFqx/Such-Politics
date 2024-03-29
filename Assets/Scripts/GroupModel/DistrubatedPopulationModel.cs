﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DistrubatedPopulationModel : IPopulationModel
{

    public double populationMean = 1.0;
    public double populationVariance = .35;

    public double[] opinionsSpread = { .5, .5 };
    public double opinionsMean = 0;
    public double opinionsVariance = .75;

    private double[][] population;
    private double[][] opinions;
    private List<GameObject> gameObjectPopulation = new List<GameObject>();

    private List<GroupModel.GameObjectMutator> stateMutators;

    private int numPlayers;
    private double[] playerPopularity;

    public void generatePopulation(int populationSize, double[] populationFractions, List<GroupModel.GameObjectMutator> gameObjectMutators, GameObject baseObject)
    {
        this.numPlayers = populationFractions.Length;
        this.playerPopularity = new double[populationFractions.Length];

        this.stateMutators = gameObjectMutators;
        this.population = PopulationBuilder.buildPopulation(populationSize, numPlayers, populationFractions, populationMean, populationVariance);

        for (int i = 0; i < population.Length; i++)
        {
            GameObject newGameObject = (GameObject)Object.Instantiate(baseObject);
            int mutator = getCorrectMutator(population[i]);
            stateMutators[mutator].Mutate(newGameObject);
            gameObjectPopulation.Add(newGameObject);

            for (int j = 0; j < population[i].Length; j++)
            {
                playerPopularity[j] += population[i][j];
            }
        }
    }

    public void setStatements(System.Collections.Generic.List<Statement> questions)
    {
        this.opinions = PopulationBuilder.buildPopulation(population.Length, questions.Count, opinionsSpread, opinionsMean, opinionsVariance);
        for (int i = 0; i < opinions.Length; i++)
        {
            for (int j = 0; j < opinions[i].Length; j++)
            {
                UnityEngine.Debug.Log(opinions[i][j]);
            }
            UnityEngine.Debug.Log("");
        }
    }

    public void applyAnswer(int player, int statement, bool answer)
    {
        playerPopularity = new double[numPlayers];
        for (int person = 0; person < population.Length; person++)
        {
            double weight = opinions[person][statement];
            weight = (answer ? weight : -1 * weight);

            double oldOpinion = population[person][player];
            population[person][player] *= weight;

            double opinionDiff = oldOpinion - population[person][player];
            double opinionToSpread = (opinionDiff / (population[person].Length - 1)); // keep an extra boost for neutral
            //population[person][population[person].Length - 1] += oldOpinion - population[person][player];

            playerPopularity[player] += opinionDiff;

            for (int i = 0; i < population[person].Length; i++)
            {
                if (i != player)
                {
                    playerPopularity[i] += opinionToSpread;
                    population[person][i] += opinionToSpread;
                }
            }

            // We made an extra opnion to make a bigger impact to neutral group
            //population[person][population[person].Length - 1] += opinionToSpread;
            //playerPopularity[population[person].Length - 1] += opinionToSpread;

            // Finn riktig mutator
            int mutator = getCorrectMutator(population[person]);

            stateMutators[mutator].Mutate(gameObjectPopulation[person]);
        }
    }

    public double getPopularity(int player)
    {
        return playerPopularity[player] / population.Length;
    }

    public System.Collections.Generic.List<GameObject> getPopulation()
    {
        return gameObjectPopulation;
    }

    public double getDistanceFrom(double point, int question, int player)
    {
        double totOpinion = 0;
        for (int person = 0; person < opinions[person].Length; person++)
        {
            totOpinion += point - opinions[person][question];
        }

        return ((totOpinion / opinions.Length) + 1) / 2;
    }

    /*
	 * Same result as generatePopulation, except with premade gameObjects
	 */
    public void generateWithPremadeObjects(IEnumerable<GameObject> people, double[] populationFractions, List<GroupModel.GameObjectMutator> gameObjectMutators)
    {
        gameObjectPopulation.AddRange(people);

        this.numPlayers = populationFractions.Length;
        this.playerPopularity = new double[populationFractions.Length];

        this.stateMutators = gameObjectMutators;
        this.population = PopulationBuilder.buildPopulation(gameObjectPopulation.Count, numPlayers, populationFractions, populationMean, populationVariance);

        for (int i = 0; i < population.Length; i++)
        {
            GameObject newGameObject = gameObjectPopulation[i];
            int mutator = getCorrectMutator(population[i]);
            stateMutators[mutator].Mutate(newGameObject);
            gameObjectPopulation.Add(newGameObject);

            for (int j = 0; j < population[i].Length; j++)
            {
                playerPopularity[j] += population[i][j];
            }
        }
    }

    private int getCorrectMutator(double[] person)
    {
        int mutator = 0;
        double largestScore = 0;
        for (int i = 0; i < person.Length; i++)
        {
            if (largestScore < person[i])
            {
                largestScore = person[i];
                mutator = i;
            }
        }

        return mutator;
    }
}
