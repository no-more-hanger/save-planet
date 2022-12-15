using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour {
    private AudioSource audioSource;
    public static ButtonSound _buttonInstance;

    public AudioClip buttonBgm;

    // Start is called before the first frame update
    void Awake() {
        _buttonInstance = this;
    }

    private void Start() {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void OnButtonAudio() {
        audioSource.clip = buttonBgm;
        if (GameStaticData._dataInstance.isSound) {
            audioSource.volume = GameStaticData._dataInstance.soundVolume;
            audioSource.Play();
        }
    }

    public void OnAudio(AudioClip audioClip) {
        audioSource.clip = audioClip;
        if (GameStaticData._dataInstance.isSound) {
            audioSource.volume = GameStaticData._dataInstance.soundVolume;
            audioSource.Play();
        }
    }
}