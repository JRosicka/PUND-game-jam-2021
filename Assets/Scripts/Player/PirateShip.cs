using Rewired;
using UnityEngine;

public class PirateShip : MonoBehaviour
{
    private const string HORIZONTAL_MOVEMENT_NAME = "Move Horizontal";
    private const string VERTICAL_MOVEMENT_NAME = "Move Vertical";
    private const string SELECT_NAME = "Select";
    private const string INTERACT_NAME = "Interact";
    private const string HANDBRAKE_NAME = "Handbrake";
    private const string PAUSE_NAME = "Pause";
    private const string RESTART_NAME = "Restart";
    private const string ABILITY_NAME = "Ability";
    private const string SHOOT_RIGHT = "Shoot Right";
    private const string SHOOT_LEFT = "Shoot Left";

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

    public ProjectileBehavior cannonballPrefab;
    public GameObject cannonballSpawnPointRight;
    public GameObject cannonballSpawnPointLeft;
    
    private Player PlayerInput;
    private float currentSpeed;
    private float currentRotationSpeed;
    private float secondsSinceLastShot;
    
    public float bulletSpeed;
    public float bulletScale;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInput = ReInput.players.GetPlayer(PlayerID);
        secondsSinceLastShot = cannonFireDelay;
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (PlayerInput.controllers.joystickCount == 0) {
            Joystick joystick = ReInput.controllers.GetJoystick(PlayerID);
            PlayerInput.controllers.AddController(joystick, true);
        }
        
        secondsSinceLastShot += Time.deltaTime;

        // Disallow player input while the ship is respawning
        if (PlayerController.IsRespawning) return;

        MovePlayer(PlayerInput.GetAxis(VERTICAL_MOVEMENT_NAME) * MaxForwardSpeedInput);
        float turnInput = PlayerInput.GetAxis(HORIZONTAL_MOVEMENT_NAME);
        TurnPlayer(turnInput * MaxTurnSpeedInput);
        
        if (PlayerInput.GetButton(SHOOT_RIGHT) && secondsSinceLastShot >= cannonFireDelay) {
            ShootCannonball(true);
        }
        
        if (PlayerInput.GetButton(SHOOT_LEFT) && secondsSinceLastShot >= cannonFireDelay) {
            ShootCannonball(false);
        }
    }

    /// <param name="shootAtRight">If true, fire at the right, otherwise fire at the left</param>
    private void ShootCannonball(bool shootAtRight) {
        AudioManager.Instance.PlayCannonShot();

        GameObject spawner = shootAtRight ? cannonballSpawnPointRight : cannonballSpawnPointLeft;
        
        ProjectileBehavior cannonBall = Instantiate(cannonballPrefab, spawner.transform.position, spawner.transform.rotation);
        cannonBall.FiringPlayer = PlayerController;
        cannonBall.projectileSpeed = bulletSpeed;
        cannonBall.transform.localScale = new Vector3(bulletScale, bulletScale, bulletScale);
            
        secondsSinceLastShot = 0;
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
}
