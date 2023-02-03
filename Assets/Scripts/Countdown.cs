using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    Stopwatch stopwatch;

    private TextMeshProUGUI countText;
    [SerializeField] private string finalText;
    [SerializeField] private float time;

    // Start is called before the first frame update
    void Start()
    {
        stopwatch = new Stopwatch();
        countText = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        float firstSize = countText.fontSize;
        StartCoroutine(Count(3, firstSize, time));
    }

    // count number
    IEnumerator Count(int startNum, float firstSize, float time) {
        while (startNum > 0) {
            stopwatch.Restart();
            countText.fontSize = firstSize;
            countText.text = startNum.ToString();
            while ((int)stopwatch.ElapsedMilliseconds / 1000 < time) {
                countText.fontSize = (time - stopwatch.ElapsedMilliseconds / 1000f) * firstSize;
                yield return null;
            }
            startNum--;
        }
        countText.fontSize = firstSize;
        countText.text = finalText;

        // after some times, destroy object & continue game 
        stopwatch.Restart();
        while ((int)stopwatch.ElapsedMilliseconds / 1000 < time) {
            yield return null;
        }
        stopwatch.Stop();
        ContinueGame();
    }

    private void ContinueGame() {
        GameObject.Find("Canvas").transform.Find("SettingPopup").GetComponent<SettingManager>().OnContinueGame();
        Destroy(gameObject);
    }
}
