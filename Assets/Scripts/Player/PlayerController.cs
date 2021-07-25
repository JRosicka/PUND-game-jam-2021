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

    public void ApplyDamage(int damageAmount) {
        currentHP -= damageAmount;
        Debug.Log("Damage applied!");

        if (currentHP < 0)
            currentHP = 0;
        if (currentHP == 0)
            DestroyShip();
        

        EventManager.damageEvent.Invoke(Ship.PlayerID);
    }

    public void Heal(int additionalHealth) {
        currentHP += additionalHealth;
        Debug.Log("Health get!");

        EventManager.healEvent.Invoke(Ship.PlayerID);
    }

    public void AddMapFragment() {
        currentMapFragmentCount++;
        Debug.Log("Map fragment get!");
        EventManager.mapFragmentCollectionEvent.Invoke(Ship.PlayerID);
    }

    private void DestroyShip() {
        currentHP = StartingHP;
        
        // Drop all the held map fragments
        if (currentMapFragmentCount > 0) {
            MapFragmentCollectible fragment = Instantiate(MapFragmentCollectiblePrefab, GameManager.Instance.DroppedMapFragmentsBucket);
            fragment.SetFragmentCount(currentMapFragmentCount);
        }
        currentMapFragmentCount = 0;
        
        Debug.Log("Ship destroyed!");

        // TODO: Does the player lose their upgrades

        // TODO: Respawn (move the ship to its new spot)
    }
}
