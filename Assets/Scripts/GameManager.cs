using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private List<PirateShip> Players;
    public static GameManager Instance;

    public IslandManager IslandManager;
    
    public MapFragmentCollectible MapFragmentCollectiblePrefab;
    public Transform DroppedMapFragmentsBucket;

    public PickupLocation DroppedItemPrefab;

    public List<Transform> ShipSpawnLocations;
    private int nextShipSpawnIndex;

    public Transform cameraPos;
    
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

    public void DropFragment(int numberOfHeldFragments, Vector3 location) {
        PickupLocation droppedLocation = Instantiate(DroppedItemPrefab, DroppedMapFragmentsBucket);
        Transform locationTransform = droppedLocation.transform;
        locationTransform.position = location;
        
        MapFragmentCollectible fragment = Instantiate(MapFragmentCollectiblePrefab, locationTransform);
        fragment.SetFragmentCount(numberOfHeldFragments);
        fragment.WasDroppedByPlayer = true;

        droppedLocation.AssignCollectible(fragment);
    }

    public void EndGameWithWinner(PlayerController winner) {
        Debug.Log("THE WINNER IS PLAYER " + winner.Ship.PlayerID);

        PlayerPrefs.SetFloat("cameraPosX", cameraPos.position.x);
        PlayerPrefs.SetFloat("cameraPosY", cameraPos.position.y);
        PlayerPrefs.SetFloat("cameraPosZ", cameraPos.position.z);

        PlayerPrefs.SetFloat("CameraRotY", cameraPos.rotation.y);

        PlayerPrefs.Save();


        Debug.Log("Position " + cameraPos.transform.position +  "Stored Values " + PlayerPrefs.GetFloat("cameraPosX")
        +" " + PlayerPrefs.GetFloat("cameraPosY")+ " " + PlayerPrefs.GetFloat("cameraPosZ"));
    

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
