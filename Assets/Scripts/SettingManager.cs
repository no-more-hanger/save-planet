using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

        if (curActive) { // turn off
            if (EventSystem.current.GetComponent<KeyNavigator>()) {
                EventSystem.current.GetComponent<KeyNavigator>().enabled = true;
            }
            EventSystem.current.GetComponent<KeyNavigator>()?.SetLastSelectObj();

            if (popupName == "SettingPopup") {
                GameObject.Find("Canvas").transform.Find("PausePopup").gameObject?.SetActive(true);
                GameObject.Find("Canvas").transform.Find("PausePopup").gameObject?.GetComponent<KeyNavigator>().SetLastSelectObj();
            }
            if (popupName == "PausePopup") {
                EventSystem.current.GetComponent<BackController>()?.OnAbleKey(); // pause button activate
            }
        }
        else { // turn on
            if (EventSystem.current.GetComponent<KeyNavigator>()) {
                EventSystem.current.GetComponent<KeyNavigator>().enabled = false;
            }
            if (popupName == "SettingPopup") {
                GameObject.Find("PausePopup")?.SetActive(false);
            }
            popup.GetComponent<KeyNavigator>()?.SetFirstSelectObj();
            EventSystem.current.GetComponent<BackController>()?.OnNotAbleKey(); // pause button deactive
        }
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

        EventSystem.current.GetComponent<BackController>().OnNotAbleKey(); // pause button deactivate
    }

    // continue game
    public void OnContinueGame() {
        Time.timeScale = 1f;
        GameObject.FindWithTag("Player").GetComponent<BaseCharacter>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<BaseCharacter>().SetIsMoveX(true);
        GameObject.FindWithTag("Player").GetComponent<BaseCharacter>().SetIsMoveY(true);
        GameObject.FindWithTag("Timer").GetComponent<TimerController>().StartTimer();

        EventSystem.current.GetComponent<BackController>().OnAbleKey(); // pause button activate
    }
}
