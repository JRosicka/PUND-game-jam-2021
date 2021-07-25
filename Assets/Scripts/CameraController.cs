using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private const float playableAreaScale = 16f / 7.6667f;
    
    public Camera Camera;
    public float ExtraSpaceMultiplier;
    public float MinCameraLength = 50;
    public float CameraHeight = 500;
    public AnimationCurve CameraZAdjustmentByCameraSizeAmount;
    
    // Bottom 15% of screen right now is canvas. 4/27ths    16 x 7.666666

    // Start is called before the first frame update
    void Start()
    {
        AdjustCamera();
    }

    // Update is called once per frame
    void Update()
    {
        AdjustCamera();
    }

    private void AdjustCamera() {
        // Figure out how big the camera needs to be based on the player locations
        List<PirateShip> players = GameManager.Instance.GetPlayers();
        List<Vector3> playerLocations = players.Select(e => e.PerceivedShipCenter.position).ToList();
        float minX = playerLocations.Min(vector => vector.x);
        float maxX = playerLocations.Max(vector => vector.x);
        float minZ = playerLocations.Min(vector => vector.z);
        float maxZ = playerLocations.Max(vector => vector.z);

        // Add extra space
        // minX -= ExtraSpaceInEachDirection;
        // maxX += ExtraSpaceInEachDirection;
        // minZ -= ExtraSpaceInEachDirection;
        // maxZ += ExtraSpaceInEachDirection;

        // // Check to make sure the camera isn't too small
        float xDistance = Mathf.Abs(maxX - minX);
        float zDistance = Mathf.Abs(maxZ - minZ);
        // if (xDistance < MinCameraLength) {
        //     float multiplyFactor = MinCameraLength / xDistance;
        //     xDistance *= multiplyFactor;
        //     zDistance *= multiplyFactor;
        // }
        // if (zDistance < MinCameraLength * playableAreaScale) {
        //     float multiplyFactor = MinCameraLength * playableAreaScale / zDistance;
        //     xDistance *= multiplyFactor;
        //     zDistance *= multiplyFactor;
        // }

        // Determine camera position
        // float cameraY = Mathf.Max(xDistance / 2, zDistance * playableAreaScale / 2);
        float cameraX = (minX + maxX) / 2;
        float cameraZ = (minZ + maxZ) / 2;
        
        // Update camera
        // Camera.transform.position = new Vector3(cameraX, cameraY, cameraZ);
        Camera.orthographicSize = Mathf.Max(xDistance / playableAreaScale, zDistance) * ExtraSpaceMultiplier;
        cameraZ -= Mathf.Abs(maxZ - minZ) * CameraZAdjustmentByCameraSizeAmount.Evaluate(Camera.orthographicSize * 1.4f / ExtraSpaceMultiplier);
        Camera.orthographicSize = Mathf.Max(Camera.orthographicSize, MinCameraLength);
        Camera.transform.position = new Vector3(cameraX, CameraHeight, cameraZ);

        // Debug.Log("Camera position: " + Camera.transform.position + ", ship 1: " + playerLocations[0] + ", ship 2: " + playerLocations[1] + ", xDistance: " + xDistance + ", zDistance: " + zDistance);
        
        
        // Camera.main.WorldToViewportPoint()
    }
}
