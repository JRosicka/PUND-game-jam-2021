using System.Collections;
using UnityEngine;

public class IntroAudioScript : MonoBehaviour {
    public AudioManager AudioManager;

    public AudioSource SoundTrackPrefab;
    public AudioSource DialogPrefab;

    public Mover cameraControl;

    private AudioSource DialogInstance;
    
    private void Start() {
        AudioManager.PlaySong(SoundTrackPrefab);
    }

    
    public IEnumerator RunAudioScript() {

        float fastCam = 1.5f;
        float slowCam = 5f;
        
        DialogInstance = AudioManager.PlaySoundEffect(DialogPrefab);
        
        yield return new WaitForSeconds(11f);
        
        AudioManager.PauseAudio(DialogInstance);
        cameraControl.cameraMoveSpeed = fastCam;
        
        yield return new WaitForSeconds(2.3f);

        cameraControl.cameraMoveSpeed = slowCam;
        
        AudioManager.ResumeAudio(DialogInstance);
        
        yield return new WaitForSeconds(18.5f);

        AudioManager.PauseAudio(DialogInstance);
        cameraControl.cameraMoveSpeed = fastCam;

        yield return new WaitForSeconds(3f);
        cameraControl.cameraMoveSpeed = slowCam;
        
        AudioManager.ResumeAudio(DialogInstance);

        //AudioManager.FadeOutAllAudio();

        yield return null;
    }
}
