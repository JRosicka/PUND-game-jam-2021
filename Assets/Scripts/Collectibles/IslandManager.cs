using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Sets up <see cref="Island"/>s, handles assigning <see cref="Collectible"/>s to them
/// </summary>
public class IslandManager : MonoBehaviour {
    public List<Island> Islands;
    public int DelaySecondsBeforeAssigningCollectibles = 3;
    public readonly int totalMapFragmentCount = 3;

    public MapFragmentCollectible MapFragmentPrefab;
    public NullCollectible NullCollectiblePrefab;
    public List<UpgradeCollectible> UpgradePrefabs;

    private List<MapFragmentCollectible> queuedMapFragments = new List<MapFragmentCollectible>();
    private List<Collectible> spawnedCollectibles = new List<Collectible>();

    public void Initialize() {
        for (int i = 0; i < totalMapFragmentCount; i++) {
            queuedMapFragments.Add(Instantiate(MapFragmentPrefab));
        }
        StartCoroutine(AssignCollectibles());
    }

    public void OnCollectiblePickedUp(PlayerController player, Collectible collectible, Island island) {
        collectible.ApplyCollectible(player);

        if (collectible is MapFragmentCollectible) {
            StartCoroutine(AssignCollectibles());
        }
        
        Destroy(collectible);
    }

    private IEnumerator AssignCollectibles() {
        Islands.ForEach(island => island.ClearCollectible());
        spawnedCollectibles.ForEach(collectible => {
            if (collectible != null) {
                Destroy(collectible);
            }
        });
        spawnedCollectibles = new List<Collectible>();
        
        List<Island> assignableIslands = new List<Island>(Islands.Where(island => island.IsEligibleForCollectibleAssignment()));
        List<Island> unAssignableIslands = new List<Island>(Islands.Where(island => !assignableIslands.Contains(island)));
        List<Island> assignableMapFragmentIslands = new List<Island>(Islands.Where(island => island.IsEligibleForMapFragmentAssignment()));
        
        yield return new WaitForSeconds(DelaySecondsBeforeAssigningCollectibles);

        // Map fragments
        if (queuedMapFragments.Count > 0) {
            Island chosenIsland = assignableMapFragmentIslands[Random.Range(0, assignableMapFragmentIslands.Count)];
            MapFragmentCollectible fragment = queuedMapFragments[0];
            fragment.SetFragmentCount(1);
            
            queuedMapFragments.Remove(fragment);
            chosenIsland.AssignCollectible(fragment);
            spawnedCollectibles.Add(fragment);
            
            EventManager.dialogEvent.Invoke(chosenIsland.Riddle);

            if (assignableIslands.Contains(chosenIsland)) {
                assignableIslands.Remove(chosenIsland);
            }
        }

        // Upgrades
        foreach (Island island in assignableIslands) {
            UpgradeCollectible upgradeCollectible = Instantiate(UpgradePrefabs[Random.Range(0, UpgradePrefabs.Count)]);
            island.AssignCollectible(upgradeCollectible);
            spawnedCollectibles.Add(upgradeCollectible);
        }

        // Nulls
        foreach (Island island in unAssignableIslands) {
            NullCollectible nullCollectible = Instantiate(NullCollectiblePrefab);
            island.AssignCollectible(nullCollectible);
            spawnedCollectibles.Add(nullCollectible);
        }
    }
}
