using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GroupModel {

public class PopulationCollections : IPopulationModel {

	private List<PopulationManager> groups = new List<PopulationManager>();
	
	private List<GameObject> people = new List<GameObject> ();
	private List<Statement> statements = new List<Statement>();

	private float[][][] answerReactions;

	public void generatePopulation(int populationSize, double[] populationFractions, List<GameObjectMutator> populationMutators, GameObject baseObject) {
			List<GameObject> generatedPeople = new List<GameObject>();

			for (int i = 0; i < populationSize; i++) {
				generatedPeople.Add((GameObject) Object.Instantiate(baseObject));
			}

			generateWithPremadeObjects (generatedPeople, populationFractions, populationMutators);
	}
	
	/*
	 * Same result as generatePopulation, except with premade gameObjects
	 */
	public void generateWithPremadeObjects(IEnumerable<GameObject> generatedPeople, double[] populationFractions, List<GroupModel.GameObjectMutator> populationMutators) {
		DestroyIt (); // Clean if dirty
		
		people.AddRange (generatedPeople);

			int totalGeneratedPeople = 0;
			for (int i = 0; i < populationFractions.Length; i++) {
				int popSize = (int)(people.Count * populationFractions[i]);

				List<GameObject> peopleList = people.GetRange(totalGeneratedPeople, popSize);
				PopulationManager group = new PopulationManager();
				group.mutator = populationMutators[i];
				
				group.AddPeople(peopleList);
				groups.Add(group);
				
				totalGeneratedPeople += popSize;
			}

			if (totalGeneratedPeople < people.Count) {
				// If there were some rounding errors
				
				List<GameObject> peopleList = people.GetRange(totalGeneratedPeople, people.Count - totalGeneratedPeople);
				int lastPos = groups.Count - 1;
				groups[lastPos].AddPeople(peopleList);
			}
	}
	
		List<GameObject> makePeopleForGroup(int numPeople, GameObject baseObject) {
			List<GameObject> peopleList = new List<GameObject>();

			for (int j = 0; j < numPeople; j++) {
				peopleList.Add((GameObject) Object.Instantiate(baseObject));
			}

			return peopleList;
		}

	public void setStatements(List<Statement> statements) {
		this.statements = statements;

			System.Random rnd = new System.Random ();

		answerReactions = new float[statements.Count][][];

		for (int i = 0; i < statements.Count; i++) {
			answerReactions [i] = new float[2][];

			// Yes reaction
			answerReactions[i][0] = new float[groups.Count];
			for (int j = 0; j < groups.Count; j++) {
				if (j == 0) {
					answerReactions[i][0][j] = 2*(float)rnd.NextDouble() - 1;
				} else {
					answerReactions[i][0][j] = (float)rnd.NextDouble();
					if (answerReactions[i][0][j-1] > 0) {
						answerReactions[i][0][j] *= -1;
					}
				}
			}

			// No reaction will here be opposite of yes reaction
			answerReactions [i] [1] = new float[groups.Count];
			for (int j = 0; j < groups.Count; j++) {
				answerReactions[i][1][j] = -1 * answerReactions[i][0][j];
			}
		}

		//Debug.Log ("Probabilities", answerReactions);
	}

	public List<GameObject> getPopulation() {
		return people;
	}

	public double getPopularity(int player) {
		return (double)(groups[player].GetCount()) / people.Count;
	}
	
	/**
	 * Generally there will only be _two_ answers, agree or not agree
	 */
	public void applyAnswer(int player, int statement, bool answer) {
		if (answer) {
				PerformPopulationMigration(answerReactions[statement][0]);
		} else {
				PerformPopulationMigration(answerReactions[statement][1]);
		}
	}

	public int GetNumGroups() {
		return groups.Count;
	}

	public void AddPopulation(PopulationManager group) {
		groups.Add (group);
	}

	/**
	 * @param winLoseParts has this format:
	 *   There _must_ be positive _and_ negative numbers, if not
	 *   the migration will be skipped.
	 *   The positive numbers will be summed up to a total number.
	 * 
	 *   Each positive number (bigger than 0.0f) determines how much of the total population
	 *   migration the corresponding group will get. If we have 2 positive 
	 *   numbers a and b (at position i and j, respectively),
	 *   group i will get a/(a+b) of the total population migration.
	 * 
	 *   Each negative number is a _percentage_ (between -1.0f and 0.0f) corresponding to how
	 *   much the group will lose of its population.
	 */
	public void PerformPopulationMigration(float[] winLoseParts) {
		Dictionary<int, float> winners = new Dictionary<int, float> ();
		Dictionary<int, float> losers = new Dictionary<int, float> ();
		float totalWinners = 0.0f;

		if (winLoseParts.Length != groups.Count) {
			throw new System.ApplicationException(
				string.Format(
					"winLoseParts has size {0}, it's required in this configuration to be {1}.", 
					winLoseParts.Length, groups.Count)
			);
		}

		for (int i = 0; i < winLoseParts.Length; i++) {
			if (winLoseParts[i] > 0.0f) {
				winners[i] = winLoseParts[i];
				totalWinners += winLoseParts[i];
			} else if (winLoseParts[i] < 0.0f) {
				losers[i] = System.Math.Abs(winLoseParts[i]);
				if (losers[i] > 1.0f) {
					losers[i] = 0.9f;
				}
				
			}
		}

		// No migration will be performed if winners or losers are empty
		if (winners.Count == 0 || losers.Count == 0) {
			return; // Throw exception?
		}

		foreach (KeyValuePair<int, float> loser in losers) {
			int numLost = (int)Mathf.Ceil(groups[loser.Key].GetCount() * loser.Value);

			foreach (KeyValuePair<int, float> winner in winners) {
				int numWon = (int)Mathf.Ceil((float)numLost * winner.Value / totalWinners);

				GameObject[] migratingDudes = groups[loser.Key].LoseRandom(numWon);

				groups[winner.Key].AddPeople(migratingDudes);
			}
		}
	}

	public void DestroyIt() {
		people.Clear ();

		foreach (PopulationManager group in groups) {
			group.DestroyIt();
		}
	}

    public double getDistanceFrom(double point, int question, int player)
    {
			double distance = answerReactions[question][0][player];
			distance = point - distance;
			
			return (distance + 1)/2;
	}
	
}
}