using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public GameObject AudioBucket;
    
    public static AudioManager Instance;
    private AudioSource currentlyPlayingSong;

    private List<AudioSource> playingInstances = new List<AudioSource>();
    private List<AudioSource> fadingOutInstances;
    private List<AudioSource> pausedAudio = new List<AudioSource>();

    public void Start() {
        Instance = this;
        
        EventManager.dialogEvent.AddListener(PlayDialogLine);
    }

    public AudioSource PlaySong(AudioSource source) {
        if (currentlyPlayingSong != null) {
            currentlyPlayingSong.Stop();
        }

        currentlyPlayingSong = source;
        return PlayAudioSource(source, true);
    }

    public AudioSource PlaySoundEffect(AudioSource source) {
        return PlayAudioSource(source, false);
    }

    public void PauseAudio(AudioSource source) {
        if (!playingInstances.Contains(source)) return;
        
        source.Pause();
        pausedAudio.Add(source);
    }

    public void ResumeAudio(AudioSource source) {
        if (!playingInstances.Contains(source)) return;
        
        source.UnPause();
        pausedAudio.Remove(source);
    }

    private float currentSecondsOfFadeoutLeft;
    private float currentFadeoutDuration;
    
    private void Update() {
        if (currentSecondsOfFadeoutLeft <= 0)
            return;

        currentSecondsOfFadeoutLeft -= Time.deltaTime;
        if (currentSecondsOfFadeoutLeft <= 0) {
            // Fadeout is over
            currentSecondsOfFadeoutLeft = 0;
            foreach (AudioSource source in fadingOutInstances) {
                if (source != null) {
                    Destroy(source.gameObject);
                }
            }

            fadingOutInstances = null;
            return;
        }

        float newVolume = currentSecondsOfFadeoutLeft / currentFadeoutDuration;
        foreach (AudioSource source in fadingOutInstances) {
            if (source != null) {
                source.volume = newVolume;
            }
        }
    }

    public void FadeOutAllAudio(float secondsOfFadeOut = 3) {
        CleanupAudio();
        fadingOutInstances = new List<AudioSource>(playingInstances);
        playingInstances = new List<AudioSource>();
        currentSecondsOfFadeoutLeft = secondsOfFadeOut;
        currentFadeoutDuration = secondsOfFadeOut;
    }

    private void CleanupAudio() {
        foreach (AudioSource source in playingInstances.Where(e => !e.isPlaying)) {
            playingInstances.Remove(source);
            Destroy(source.gameObject);
        }
    }
    
    private AudioSource PlayAudioSource(AudioSource source, bool loop) {
        AudioSource audioInstance = Instantiate(source, AudioBucket.transform);
        playingInstances.Add(audioInstance);
        
        source.loop = loop;
        source.time = 0f;

        if (loop) {
            audioInstance.Play();
        } else {
            StartCoroutine(PlayAndDestroyAudio(audioInstance));
        }

        return audioInstance;
    }

    private IEnumerator PlayAndDestroyAudio(AudioSource source) {
        source.Play();
        do yield return null;
        while (source != null && (source.isPlaying || pausedAudio.Contains(source)));

        if (source != null) {
            Destroy(source.gameObject);
        }
    }

    private void PlayDialogLine(DialogLine dialog) {
        PlaySoundEffect(dialog.Audio);
    }

    
    public void TestFadeOutAllAudio() {
        FadeOutAllAudio();
    }
}
