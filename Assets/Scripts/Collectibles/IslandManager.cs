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
    public GameObject CollectibleBucket;

    [Header("Collectibles")]
    public MapFragmentCollectible MapFragmentPrefab;
    public NullCollectible NullCollectiblePrefab;
    public VictoryCollectible VictoryPrefab;
    public List<UpgradeCollectible> UpgradePrefabs;
    
    private List<MapFragmentCollectible> queuedMapFragments = new List<MapFragmentCollectible>();
    private List<Collectible> spawnedCollectibles = new List<Collectible>();

    public void Initialize() {
        for (int i = 0; i < totalMapFragmentCount; i++) {
            queuedMapFragments.Add(Instantiate(MapFragmentPrefab, CollectibleBucket.transform));
        }
        StartCoroutine(AssignCollectibles());
    }

    public void OnCollectiblePickedUp(PlayerController player, Collectible collectible) {
        collectible.ApplyCollectible(player);

        // Islands get refreshed if this was a map fragment, but only if this isn't getting picked up from a dead player
        if (collectible is MapFragmentCollectible fragmentCollectible && !fragmentCollectible.WasDroppedByPlayer) {
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
        } else {
            // Pick the victory location!
            VictoryCollectible victoryCollectible = Instantiate(VictoryPrefab, CollectibleBucket.transform);

            // Hack - we don't want to pick a location with a hint telling the player to look for a map fragment there. 
            // They already have all the map fragments.
            assignableMapFragmentIslands = assignableMapFragmentIslands
                .Where(e => !e.Riddle.DialogString.ToLower().Contains("map")).ToList();
            
            Island chosenIsland = assignableMapFragmentIslands[Random.Range(0, assignableMapFragmentIslands.Count)];
            chosenIsland.AssignCollectible(victoryCollectible);
            spawnedCollectibles.Add(victoryCollectible);
            
            EventManager.dialogEvent.Invoke(chosenIsland.Riddle);

            if (assignableIslands.Contains(chosenIsland)) {
                assignableIslands.Remove(chosenIsland);
            }
        }

        // Upgrades
        foreach (Island island in assignableIslands) {
            UpgradeCollectible upgradeCollectible = Instantiate(UpgradePrefabs[Random.Range(0, UpgradePrefabs.Count)], CollectibleBucket.transform);
            island.AssignCollectible(upgradeCollectible);
            spawnedCollectibles.Add(upgradeCollectible);
        }

        // Nulls
        foreach (Island island in unAssignableIslands) {
            NullCollectible nullCollectible = Instantiate(NullCollectiblePrefab, CollectibleBucket.transform);
            island.AssignCollectible(nullCollectible);
            spawnedCollectibles.Add(nullCollectible);
        }
    }
    
    
}
