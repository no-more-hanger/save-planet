using UnityEngine;
using System.Diagnostics;
using TMPro;

public class TimerController : MonoBehaviour {
    Stopwatch stopwatch;
    [SerializeField] private TextMeshProUGUI timeText;

    void Start() {
        stopwatch = new Stopwatch();
        StartTimer();
        timeText = GetComponent<TextMeshProUGUI>();
        timeText.text = "00 : 00";
    }

    public void StartTimer() {
        stopwatch.Start();
    }

    public void StopTimer() {
        stopwatch.Stop();
    }

    public void ResetTimer() {
        stopwatch.Reset();
    }

    public void RestartTimer() {
        stopwatch.Restart();
    }

    private void Update() {
        int sec = (int)stopwatch.ElapsedMilliseconds / 1000;
        int min = sec / 60;

        timeText.text = string.Format("{0, 2:00} : {1, 2:00}", min, sec - min * 60);
    }
}
