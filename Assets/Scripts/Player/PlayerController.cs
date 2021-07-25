using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a single player
/// </summary>
public class PlayerController : MonoBehaviour {
    // 0 equals DEATH
    private int currentHP;
    private int currentMapFragmentCount;

    public int StartingHP;
    public MapFragmentCollectible MapFragmentCollectiblePrefab;
    public PirateShip Ship;

    public void Start() {
        currentHP = StartingHP;
        currentMapFragmentCount = 0;
    }

    public void ApplyDamage(int damageAmount) {
        currentHP -= damageAmount;
        Debug.Log("Damage applied!");

        if (currentHP < 0)
            currentHP = 0;
        
        EventManager.damageEvent.Invoke(Ship.PlayerID);

        if (currentHP == 0)
            DestroyShip();
    }

    public void Heal(int additionalHealth) {
        currentHP += additionalHealth;
        Debug.Log("Health get!");

        EventManager.healEvent.Invoke(Ship.PlayerID, currentHP);
    }

    public void AddMapFragment() {
        currentMapFragmentCount++;
        Debug.Log("Map fragment get!");
        EventManager.mapFragmentCollectionEvent.Invoke(Ship.PlayerID, currentMapFragmentCount);
    }

    private void DestroyShip() {
        currentHP = StartingHP;
        EventManager.healEvent.Invoke(Ship.PlayerID, currentHP);

        // Drop all the held map fragments
        if (currentMapFragmentCount > 0) {
            MapFragmentCollectible fragment = Instantiate(MapFragmentCollectiblePrefab, GameManager.Instance.DroppedMapFragmentsBucket);
            fragment.SetFragmentCount(currentMapFragmentCount);
        }
        currentMapFragmentCount = 0;
        
        Debug.Log("Ship destroyed!");

        // TODO: Does the player lose their upgrades?

        Ship.transform.position = GameManager.Instance.GetNextShipSpawnLocation();
    }
}
