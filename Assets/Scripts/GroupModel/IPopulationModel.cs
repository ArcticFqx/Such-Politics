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
    public void generatePopulation(int populationSize, double[] populationFractions);

    /*
     * Used in initialization
     * 
     * @statements
     *   List of statements that will be used in this game round
     */
    public void setStatements(List<Statement> questions);

    /*
     * Applies changes to the population based on answer to a statement.
     * 
     * @statement
     *   Which statement (in answer sequence) that is answered.
     * @answer
     *   True - Positive answer
     *   False - Negative answer
     */
    public void applyAnswer(int statement, bool answer);

    /*
     * Retrieves all the game objects representing the population
     * 
     */
    public List<GameObject> getPopulation();

    /*
     * 
     */
    public void addMutators(IEnumerable<GroupModel.GameObjectMutator> gameObjectMutators);
}

