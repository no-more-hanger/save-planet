using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingManager : MonoBehaviour {
    public TextMeshProUGUI bgmText, soundText;
    public Slider bgmSlider, soundSlider;

    const string ON = "On";
    const string OFF = "OFF";

    private GameObject stageManager; // stage manager

    // Start is called before the first frame update
    void Awake() {
        SettingOnOffText(GameStaticData._dataInstance.isBgm, bgmText);
        SettingOnOffText(GameStaticData._dataInstance.isSound, soundText);
        bgmSlider.value = GameStaticData._dataInstance.bgmVolume;
        soundSlider.value = GameStaticData._dataInstance.soundVolume;
        stageManager = GameObject.Find("StageManager");
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
            stageManager.GetComponent<BaseStage>().OnStartBgm();
        }
        else { // off bgm
            stageManager.GetComponent<BaseStage>().OnPauseBgm();
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
        stageManager?.GetComponent<BaseStage>().OnSetVolumeBgm();
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

    public void OnPauseGame() {
        if(Time.timeScale == 0f) {  // current game paused -> restart
            GameObject.FindWithTag("Player").GetComponent<Player001>().enabled = true;
        }
        else {  // current game ing -> pause
            GameObject.FindWithTag("Player").GetComponent<Player001>().enabled = false;
        }
        Time.timeScale = (1f - Time.timeScale);
    }
}
