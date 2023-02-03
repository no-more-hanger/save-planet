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
    }
}
