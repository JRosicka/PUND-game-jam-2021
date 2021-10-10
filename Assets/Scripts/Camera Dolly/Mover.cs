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
    private const int Player1ID = 0;
    private const int Player2ID = 1;
    
    public IntroAudioScript AudioScript;
    
    private Player Player1Input;
    private Player Player2Input;

    private int currentSeg;
    private float transition;
    private bool startDolly;
    private bool hasStartedAudio;

    private bool allowSkipToGame;

    private void Start()
    {
        Player1Input = ReInput.players.GetPlayer(Player1ID);
        Player2Input = ReInput.players.GetPlayer(Player2ID);
    }

    private void FixedUpdate()
    {
        if (Player1Input.controllers.joystickCount == 0) {
            Joystick joystick1 = ReInput.controllers.GetJoystick(Player1ID);
            Player1Input.controllers.AddController(joystick1, true);
        }
        
        if (Player2Input.controllers.joystickCount == 0) {
            Joystick joystick2 = ReInput.controllers.GetJoystick(Player2ID);
            Player2Input.controllers.AddController(joystick2, true);
        }
    }

    private void Update()
    {
        if(!rail)
        {
            return;
        }

        if (Player1Input.GetButton(SHOOT_NAME) || Player2Input.GetButton(SHOOT_NAME) || Input.GetKeyDown(KeyCode.Space))
        {
            if (allowSkipToGame) {
                SceneManager.LoadScene("NEW MAIN SCENE 2");
            }
            startDolly = true;
            StartCoroutine(AllowSkippingToGameAfterDelay());
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

    private IEnumerator AllowSkippingToGameAfterDelay() {
        yield return new WaitForSeconds(1f);
        allowSkipToGame = true;
    }
}
