using System.Collections;
using UnityEngine;

public class IntroAudioScript : MonoBehaviour {
    public AudioManager AudioManager;

    public AudioSource SoundTrackPrefab;
    public AudioSource DialogPrefab;

    public Mover cameraControl;

    public Animator ButtonIconIntroSkip;

    private AudioSource DialogInstance;
    
    private void Start() {
        AudioManager.PlaySong(SoundTrackPrefab);
    }

    
    public IEnumerator RunAudioScript() {

        float fastCam = 1.5f;
        float slowCam = 5f;
        
        DialogInstance = AudioManager.PlaySoundEffect(DialogPrefab);
        yield return new WaitForSeconds(6f);
        
        ButtonIconIntroSkip.Play("Button icon fade in");
        
        yield return new WaitForSeconds(5f);
        
        AudioManager.PauseAudio(DialogInstance);
        cameraControl.cameraMoveSpeed = fastCam;
        
        yield return new WaitForSeconds(4.3f);

        cameraControl.cameraMoveSpeed = slowCam;
        
        AudioManager.ResumeAudio(DialogInstance);
        
        yield return new WaitForSeconds(18.5f);

        AudioManager.PauseAudio(DialogInstance);
        cameraControl.cameraMoveSpeed = fastCam;

        yield return new WaitForSeconds(3f);
        cameraControl.cameraMoveSpeed = slowCam;
        
        AudioManager.ResumeAudio(DialogInstance);

        yield return new WaitForSeconds(11.5f);

        cameraControl.cameraMoveSpeed = fastCam;

        //yield return new WaitForSeconds(2f);

        AudioManager.FadeOutAllAudio();

        yield return null;
    }
}
