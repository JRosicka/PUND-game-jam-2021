using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private List<PirateShip> Players;
    public static GameManager Instance;
    
    void Awake() {
        Instance = this;
    }

    public List<PirateShip> GetPlayers() {
        return Players;
    }
}
