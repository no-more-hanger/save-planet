using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour {
    public TimerController timer;
    private void Start() {
        timer = GameObject.FindWithTag("Timer").GetComponent<TimerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        timer.StopTimer();
        GameObject.Find("Canvas").transform.Find("EndingPopup").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("SettingPopup").GetComponent<SettingManager>().OnPauseGame();
        // save balloon count after stage 1
        if (GameStaticData._dataInstance.currentStage == 1) {
            GameStaticData._dataInstance.SaveCurrentBalloonCnt(collision.gameObject.GetComponent<BaseCharacter>().GetBalloonCnt());
        }
        // reset balloon after clear stage 2
        else if (GameStaticData._dataInstance.currentStage == 2) {
            GameStaticData._dataInstance.SaveCurrentBalloonCnt(0);
        }
    }
}
