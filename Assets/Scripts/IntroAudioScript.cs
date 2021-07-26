using System.Collections;
using UnityEngine;

public class IntroAudioScript : MonoBehaviour {
    public AudioManager AudioManager;

    public AudioSource SoundTrackPrefab;
    public AudioSource DialogPrefab;

    private AudioSource DialogInstance;
    
    private void Start() {
        AudioManager.PlaySong(SoundTrackPrefab);
    }

    
    public IEnumerator RunAudioScript() {
        yield return new WaitForSeconds(2);
        
        DialogInstance = AudioManager.PlaySoundEffect(DialogPrefab);
        
        yield return new WaitForSeconds(2);
        
        AudioManager.PauseAudio(DialogInstance);
        
        yield return new WaitForSeconds(1);
        
        AudioManager.ResumeAudio(DialogInstance);
        
        yield return new WaitForSeconds(2);
        
        AudioManager.FadeOutAllAudio();

        yield return null;
    }
}
