using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GroupModel {

public class PopulationCollections {

	public List<PopulationManager> groups = new List<PopulationManager>();

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
			int numLost = (int)Mathf.Ceil(groups[loser.Key].GetSize() * loser.Value);

			foreach (KeyValuePair<int, float> winner in winners) {
				int numWon = (int)Mathf.Ceil((float)numLost * winner.Value / totalWinners);

				GameObject[] migratingDudes = groups[loser.Key].LoseRandom(numWon);

				groups[winner.Key].AddPeople(migratingDudes);
			}
		}
	}

	public void DestroyIt() {
		foreach (PopulationManager group in groups) {
			group.DestroyIt();
		}
	}
}

}