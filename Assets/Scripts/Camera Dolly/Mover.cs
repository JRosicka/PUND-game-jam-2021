using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class Mover : MonoBehaviour
{
    private const string SHOOT_NAME = "Shoot";

    [Range(0f, 10f)] public float cameraMoveSpeed = 5f;
    public Rail rail;
    public GameObject hudManager;
    public int PlayerID;

    public IntroAudioScript AudioScript;
    
    private Player PlayerInput;

    private int currentSeg;
    private float transition;
    private bool startDolly;
    private bool hasStartedAudio;

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
            hudManager.SetActive(false);
            Play();
            if (!hasStartedAudio) {
                StartCoroutine(AudioScript.RunAudioScript());
                hasStartedAudio = true;
            }
        }


        if(currentSeg == rail.nodes.Length-2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void Play()
    {
        transition += Time.deltaTime * 1 / cameraMoveSpeed;
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
