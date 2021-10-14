using System.Collections;
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
    public GameObject ShipView;

    public float RespawnSeconds = 3f;
    
    public bool IsRespawning { get; private set; }

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
        
        // Drop all the held map fragments
        if (currentMapFragmentCount > 0) {
            GameManager.Instance.DropFragment(currentMapFragmentCount, Ship.transform.position);
        }
        currentMapFragmentCount = 0;
        
        Debug.Log("Ship destroyed!");
        
        StartCoroutine(RespawnShip());
    }

    private IEnumerator RespawnShip() {
        IsRespawning = true;
        
        ShipView.SetActive(false);
        Ship.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(RespawnSeconds);
        DoRespawnShip();
    }

    private void DoRespawnShip() {
        Ship.transform.position = GameManager.Instance.GetNextShipSpawnLocation();
        ShipView.SetActive(true);
        Ship.gameObject.SetActive(true);
        
        currentHP = StartingHP;
        EventManager.healEvent.Invoke(Ship.PlayerID, currentHP);

        IsRespawning = false;
        Crew.DisplayNewCrewMate();
    }
}
