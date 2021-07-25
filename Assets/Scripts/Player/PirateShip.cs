using Rewired;
using UnityEngine;

public class PirateShip : MonoBehaviour
{
    private const string HORIZONTAL_MOVEMENT_NAME = "Move Horizontal";
    private const string VERTICAL_MOVEMENT_NAME = "Move Vertical";
    private const string SHOOT_NAME = "Shoot";
    private const string INTERACT_NAME = "Interact";
    private const string HANDBRAKE_NAME = "Handbrake";
    private const string PAUSE_NAME = "Pause";
    private const string RESTART_NAME = "Restart";
    private const string ABILITY_NAME = "Ability";

    public int PlayerID;
    public float MaxForwardSpeedInput;
    public float MaxTurnSpeedInput;
    public float MaxForwardSpeed;
    public float MaxTurnSpeed;
    public float MinTurnSpeed;
    public float cannonFireDelay;
    public AnimationCurve RotationBySpeed;

    public Transform PerceivedShipCenter;
    public PlayerController PlayerController;

    public GameObject cannonballPrefab;
    public GameObject cannonballSpawnPoint;
    
    private Player player;
    private float currentSpeed;
    private float currentRotationSpeed;
    private float secondsSinceLastShot;

    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetPlayer(PlayerID);
        secondsSinceLastShot = cannonFireDelay;
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (player.controllers.joystickCount == 0) {
            Joystick joystick = ReInput.controllers.GetJoystick(PlayerID);
            player.controllers.AddController(joystick, true);
        }

        MovePlayer(player.GetAxis(VERTICAL_MOVEMENT_NAME) * MaxForwardSpeedInput);
        float turnInput = player.GetAxis(HORIZONTAL_MOVEMENT_NAME);
        TurnPlayer(turnInput * MaxTurnSpeedInput);

        secondsSinceLastShot += Time.deltaTime;

        if (player.GetButton(SHOOT_NAME) && secondsSinceLastShot >= cannonFireDelay) {
            // Shoot cannonball
            Instantiate(cannonballPrefab, cannonballSpawnPoint.transform.position, cannonballSpawnPoint.transform.rotation);
            secondsSinceLastShot = 0;
        }
        
        if (player.GetButton(INTERACT_NAME)) {
            TestButton(INTERACT_NAME);
        }
        
        if (player.GetButton(HANDBRAKE_NAME)) {
            TestButton(HANDBRAKE_NAME);
        }
        
        if (player.GetButton(PAUSE_NAME)) {
            TestButton(PAUSE_NAME);
        }
        
        if (player.GetButton(RESTART_NAME)) {
            TestButton(RESTART_NAME);
        }
        
        if (player.GetButton(ABILITY_NAME)) {
            TestButton(ABILITY_NAME);
        }
    }

    private void TurnPlayer(float turnInput) {
        // We want the ship to rotate slower when traveling slower
        float speedMultiplyer = RotationBySpeed.Evaluate(Mathf.Abs(currentSpeed / MaxForwardSpeed));
        currentRotationSpeed += turnInput;
        currentRotationSpeed = Mathf.Clamp(turnInput, -MaxTurnSpeed, MaxTurnSpeed) * speedMultiplyer;
        if (Mathf.Abs(currentRotationSpeed) > MinTurnSpeed) {
            transform.RotateAround(transform.position, Vector3.up, currentRotationSpeed);
        }
    }

    /// <summary>
    /// Returns current speed
    /// </summary>
    private void MovePlayer(float moveInput) {
        Transform t = transform;
        currentSpeed += moveInput;
        currentSpeed = Mathf.Clamp(currentSpeed, -MaxForwardSpeed, MaxForwardSpeed);
        t.localPosition += t.rotation * Vector3.forward * currentSpeed;
    }

    private void TestButton(string buttonName) {
        // Debug.Log("Test button: " + buttonName);
    }
}
