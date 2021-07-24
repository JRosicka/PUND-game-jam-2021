using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using Rewired;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string HORIZONTAL_MOVEMENT_NAME = "Vertical";
    private const string VERTICAL_MOVEMENT_NAME = "Vertical";
    private const string SHOOT_NAME = "Shoot";
    private const string INTERACT_NAME = "Interact";
    private const string HANDBRAKE_NAME = "Handbrake";
    private const string PAUSE_NAME = "Pause";
    private const string RESTART_NAME = "Restart";
    private const string ABILITY_NAME = "Ability";

    public int PlayerID;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetPlayer(PlayerID);
    }

    // Update is called once per frame
    void FixedUpdate() {

        // if (playerControls.controllers.joystickCount == 0) {
        //     Joystick joystick = ReInput.controllers.GetJoystick(PlayerID);
        //     playerControls.controllers.AddController(joystick, true);
        // }
        
        // new Vector2(player.GetAxis("Move Horizontal"), player.GetAxis("Move Vertical")).magnitude > 0.15f) {


        float moveHorizontal = player.GetAxis(HORIZONTAL_MOVEMENT_NAME);
        float moveVertical = player.GetAxis(VERTICAL_MOVEMENT_NAME);

        if (player.GetButton(SHOOT_NAME)) {
            TestButton(SHOOT_NAME);
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

    private void TestButton(string buttonName) {
        Debug.Log("Test button: " + buttonName);
    }
}
