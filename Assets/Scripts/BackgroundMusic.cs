using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [Header("This is background music settings")]
    [SerializeField] private AudioClip bgm;  // stage bgm
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = bgm; // set bgm
        if (GameStaticData._dataInstance.isBgm) { // if bgm setting on, start bgm
            OnStartBgm();
        }
    }

    public void OnStartBgm() {
        audioSource.Play();
    }

    public void OnSetVolumeBgm() {
        audioSource.volume = GameStaticData._dataInstance.bgmVolume;
    }

    public void OnPauseBgm() {
        audioSource.Pause();
    }
}
