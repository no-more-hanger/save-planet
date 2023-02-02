using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingManager : MonoBehaviour {
    public GameObject countdownObj;
    public TextMeshProUGUI bgmText, soundText;
    public Slider bgmSlider, soundSlider;

    const string ON = "On";
    const string OFF = "OFF";

    private GameObject backgroundMusic; // background music 

    // Start is called before the first frame update
    void Awake() {
        SettingOnOffText(GameStaticData._dataInstance.isBgm, bgmText);
        SettingOnOffText(GameStaticData._dataInstance.isSound, soundText);
        bgmSlider.value = GameStaticData._dataInstance.bgmVolume;
        soundSlider.value = GameStaticData._dataInstance.soundVolume;
        backgroundMusic = GameObject.Find("BackgroundMusic");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            // effect sound play
            SoundManager._soundInstance.OnButtonAudio();

            // inactivate object
            this.gameObject.SetActive(false);
        }
    }

    void SettingOnOffText(bool value, TextMeshProUGUI text) {
        if (value) {
            text.GetComponent<TextMeshProUGUI>().text = OFF;
        }
        else {
            text.GetComponent<TextMeshProUGUI>().text = ON;
        }
    }

    public void BgmOnOffClick() {
        // effect sound play
        SoundManager._soundInstance.OnButtonAudio();

        bool value = GameStaticData._dataInstance.isBgm;
        GameStaticData._dataInstance.isBgm = !value;

        if (!value) { // on bgm
            backgroundMusic.GetComponent<BackgroundMusic>().OnStartBgm();
        }
        else { // off bgm
            backgroundMusic.GetComponent<BackgroundMusic>().OnPauseBgm();
        }

        SettingOnOffText(!value, bgmText);
    }

    public void SoundOnOffClick() {
        // effect sound play
        SoundManager._soundInstance.OnButtonAudio();

        bool value = GameStaticData._dataInstance.isSound;
        GameStaticData._dataInstance.isSound = !value;
        SettingOnOffText(!value, soundText);
    }

    public void ChangeBgmSlider() {
        GameStaticData._dataInstance.bgmVolume = bgmSlider.value;
        backgroundMusic?.GetComponent<BackgroundMusic>().OnSetVolumeBgm();
    }

    public void ChangeSoundSlider() {
        GameStaticData._dataInstance.soundVolume = soundSlider.value;
    }

    public void OnTogglePopup(string popupName) {
        SoundManager._soundInstance.OnButtonAudio();
        GameObject popup = GameObject.Find("Canvas").transform.Find(popupName).gameObject;
        bool curActive = popup.activeSelf;
        popup.SetActive(!curActive);
    }

    // pause game
    public void OnPauseGame() {
        Time.timeScale = 0f;
        GameObject.FindWithTag("Player").GetComponent<BaseCharacter>().enabled = false;
        GameObject.FindWithTag("Timer").GetComponent<TimerController>().StopTimer();
    }

    // count down
    public void StartCountDown() {
        GameObject temp = Instantiate(countdownObj, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
        temp.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }

    // continue game
    public void OnContinueGame() {
        Time.timeScale = 1f;
        GameObject.FindWithTag("Player").GetComponent<BaseCharacter>().enabled = true;
        GameObject.FindWithTag("Timer").GetComponent<TimerController>().StartTimer();
    }
}
