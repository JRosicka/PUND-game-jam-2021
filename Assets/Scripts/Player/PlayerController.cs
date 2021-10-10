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
    private int weaponLevel;
    
    public int StartingHP;
    public PirateShip Ship;
    public PlayerCrew Crew;

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

        if (currentHP == 0) {
            DestroyShip();
        } else {
            AudioManager.Instance.PlayShipHit();
        }
    }

    public void Heal(int additionalHealth) {
        currentHP += additionalHealth;
        Debug.Log("Health get!");

        EventManager.healEvent.Invoke(Ship.PlayerID, currentHP);
    }

    public void ApplySpeedBoost(float accelerationBonus, float topSpeedBonus) {
        Ship.MaxForwardSpeedInput += accelerationBonus;
        Ship.MaxForwardSpeed += topSpeedBonus;
        Debug.Log("Speed boost get!");
    }

    public void IncrementWeaponLevel() {
        weaponLevel++;
        Debug.Log("Weapon level get! Now level " + weaponLevel);
    }

    public int GetWeaponLevel() {
        return weaponLevel;
    }

    public void ApplyWeaponUpgrade(WeaponUpgradeCollectible.WeaponConfig config) {
        Ship.cannonFireDelay = config.bulletFireDelay;
        Ship.bulletSpeed = config.bulletSpeed;
        Ship.bulletScale = config.bulletScale;
    }

    public void AddMapFragment() {
        currentMapFragmentCount++;
        Debug.Log("Map fragment get!");
        EventManager.mapFragmentCollectionEvent.Invoke(Ship.PlayerID, currentMapFragmentCount);
    }

    private void DestroyShip() {
        AudioManager.Instance.PlayShipDestruction();
        
        currentHP = StartingHP;
        EventManager.healEvent.Invoke(Ship.PlayerID, currentHP);

        // Drop all the held map fragments
        if (currentMapFragmentCount > 0) {
            GameManager.Instance.DropFragment(currentMapFragmentCount, Ship.transform.position);
        }
        currentMapFragmentCount = 0;
        
        Debug.Log("Ship destroyed!");
        
        RespawnShip();
    }

    private void RespawnShip() {
        Ship.transform.position = GameManager.Instance.GetNextShipSpawnLocation();
        Crew.DisplayNewCrewMate();
    }
}
