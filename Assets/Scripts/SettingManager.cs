using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingManager : MonoBehaviour
{
    public TextMeshProUGUI bgmText, soundText;
    public Slider bgmSlider, soundSlider;

    const string ON = "On";
    const string OFF = "OFF";

    // Start is called before the first frame update
    void Start()
    {
        SettingOnOffText(GameStaticData._dataInstance.isBgm, bgmText);
        SettingOnOffText(GameStaticData._dataInstance.isSound, soundText);
        bgmSlider.value = GameStaticData._dataInstance.bgmVolume;
        soundSlider.value = GameStaticData._dataInstance.soundVolume;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.gameObject.SetActive(false);
        }
    }

    void SettingOnOffText(bool value, TextMeshProUGUI text)
    {
        if (value)
        {
            text.GetComponent<TextMeshProUGUI>().text = OFF;
        }
        else
        {
            text.GetComponent<TextMeshProUGUI>().text = ON;
        }
    }

    public void BgmOnOffClick()
    {
        bool value = GameStaticData._dataInstance.isBgm;
        GameStaticData._dataInstance.isBgm = !value;
        SettingOnOffText(!value, bgmText);
    }

    public void SoundOnOffClick()
    {
        bool value = GameStaticData._dataInstance.isSound;
        GameStaticData._dataInstance.isSound = !value;
        SettingOnOffText(!value, soundText);
    }

    public void ChangeBgmSlider()
    {
        GameStaticData._dataInstance.bgmVolume = bgmSlider.value;
    }

    public void ChangeSoundSlider()
    {
        GameStaticData._dataInstance.soundVolume = soundSlider.value;
    }
}
