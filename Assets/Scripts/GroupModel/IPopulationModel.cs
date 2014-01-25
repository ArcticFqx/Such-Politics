using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GroupModel;

public interface IPopulationModel {

    /*
     * Used in initialization
     * 
     * @populationSize
     *   Total population
     * @populationFraction
     *   How the population is fractioned into parties + "undecided".
     *   Sums up to 1.
     */
    void generatePopulation(int populationSize, double[] populationFractions, List<GroupModel.GameObjectMutator> gameObjectMutators, GameObject initialGameObject);

    /*
     * Used in initialization
     * 
     * @statements
     *   List of statements that will be used in this game round
     */
    void setStatements(List<Statement> questions);

    /*
     * Applies changes to the population based on answer to a statement.
     * 
     * @statement
     *   Which statement (in answer sequence) that is answered.
     * @answer
     *   True - Positive answer
     *   False - Negative answer
     */
    void applyAnswer(int player, int statement, bool answer);

    /*
     * Returns the population of a player in range 0 to 1,
     * such that the sum of popularity of all players equals 1.
     */
    double getPopularity(int player);

    /*
     * Retrieves all the game objects representing the population
     * 
     */
    List<GameObject> getPopulation();
}
