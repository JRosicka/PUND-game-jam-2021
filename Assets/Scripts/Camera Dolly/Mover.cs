using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class Mover : MonoBehaviour
{
    public Rail rail;
    private const string SHOOT_NAME = "Shoot";

    public int PlayerID;
    private Player PlayerInput;

    private int currentSeg;
    private float transition;
    private bool isCompleted;
    private bool startDolly;

    private void Start()
    {
        PlayerInput = ReInput.players.GetPlayer(PlayerID);
    }

    private void FixedUpdate()
    {
        if (PlayerInput.controllers.joystickCount == 0)
        {
            Joystick joystick = ReInput.controllers.GetJoystick(PlayerID);
            PlayerInput.controllers.AddController(joystick, true);
        }
    }

    private void Update()
    {
        if(!rail)
        {
            return;
        }

        if (PlayerInput.GetButton(SHOOT_NAME) || Input.GetKeyDown(KeyCode.Space))
        {
            startDolly = true;
        }

        if(startDolly)
        {
            Play();
        }
        if(currentSeg == 14)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        Debug.Log(currentSeg);
    }

    private void Play()
    {
        transition += Time.deltaTime * 1 / 2.5f;
        if(transition >1)
        {
            transition = 0;
            currentSeg++;
        }
        else if(transition < 0)
        {
            transition = 1;
            currentSeg--;
        }

        transform.position = rail.CatmullPosition(currentSeg, transition);
        transform.rotation = rail.Orientation(currentSeg, transition);

    }
}
