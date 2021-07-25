using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private List<PirateShip> Players;
    public static GameManager Instance;

    public IslandManager IslandManager;
    public Transform DroppedMapFragmentsBucket;
    
    void Awake() {
        Instance = this;
    }

    private void Start() {
        IslandManager.Initialize();
    }

    public List<PirateShip> GetPlayers() {
        return Players;
    }

    public float ClosestShipDistance(Vector3 point) {
        float closestShipDistance = float.MaxValue;
        foreach (PirateShip ship in Players) {
            Vector3 shipPosition = ship.transform.position;
            float distance = Vector3.Distance(shipPosition, point);
            closestShipDistance = Mathf.Min(closestShipDistance, distance);
        }

        return closestShipDistance;
    }
}
