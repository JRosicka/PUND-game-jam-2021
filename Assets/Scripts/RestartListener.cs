using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartListener : MonoBehaviour {
    private const string SHOOT_NAME = "Shoot";
    private Player Player1Input;
    private Player Player2Input;

    public Animator ButtonIconIntroSkip;
    
    void Start() {
        Player1Input = ReInput.players.GetPlayer(0);
        Player2Input = ReInput.players.GetPlayer(1);

        StartCoroutine(ShowSkipButton());
    }

    private IEnumerator ShowSkipButton() {
        yield return new WaitForSeconds(6f);
        
        ButtonIconIntroSkip.Play("Button icon fade in");
    }

    private void Update() {
        if (Player1Input.GetButton(SHOOT_NAME) || Player2Input.GetButton(SHOOT_NAME) || Input.GetKeyDown(KeyCode.Space)) {
            RestartGame();
        }
    }

    private void RestartGame() {
        SceneManager.LoadScene("Intro Scene");
    }
}
