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
        if (GameStaticData._dataInstance.isSound) {
            audioSource.clip = buttonBgm;
            audioSource.volume = GameStaticData._dataInstance.soundVolume;
            audioSource.Play();
        }
    }

    public void OnAudio(AudioClip audioClip) {
        if (GameStaticData._dataInstance.isSound) {
            audioSource.clip = audioClip;
            audioSource.volume = GameStaticData._dataInstance.soundVolume;
            audioSource.Play();
        }
    }
}