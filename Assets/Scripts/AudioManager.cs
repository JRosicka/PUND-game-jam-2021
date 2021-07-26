using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public GameObject AudioBucket;
    
    private static AudioManager Instance;
    private AudioSource currentlyPlayingSong; 

    public void Start() {
        Instance = this;
        
        EventManager.dialogEvent.AddListener(PlayDialogLine);
    }

    public void PlaySong(AudioSource source) {
        if (currentlyPlayingSong != null) {
            currentlyPlayingSong.Stop();
        }

        currentlyPlayingSong = source;
        PlayAudioSource(source, true);
    }

    public void PlaySoundEffect(AudioSource source) {
        PlayAudioSource(source, false);
    }
    
    private void PlayAudioSource(AudioSource source, bool loop) {
        AudioSource audioInstance = Instantiate(source, AudioBucket.transform);
        
        source.loop = loop;
        source.time = 0f;

        if (loop) {
            source.Play();
        } else {
            StartCoroutine(PlayAndDestroyAudio(audioInstance));
        }
    }

    private IEnumerator PlayAndDestroyAudio(AudioSource source) {
        source.Play();
        do yield return null; 
        while (source.isPlaying);
        Destroy(source.gameObject);
    }

    private void PlayDialogLine(DialogLine dialog) {
        PlaySoundEffect(dialog.Audio);
    }
}
