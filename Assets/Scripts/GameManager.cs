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

    public List<Transform> ShipSpawnLocations;
    private int nextShipSpawnIndex;
    
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
    
    public Vector3 GetNextShipSpawnLocation() {
        Vector3 ret = ShipSpawnLocations[nextShipSpawnIndex].position;

        nextShipSpawnIndex = (nextShipSpawnIndex + 1) % ShipSpawnLocations.Count;

        return ret;
    }
}
