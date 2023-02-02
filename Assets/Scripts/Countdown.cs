using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    private TextMeshProUGUI countText;
    [SerializeField] private string finalText;
    [SerializeField] private float time;

    // Start is called before the first frame update
    void Start()
    {
        countText = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        float firstSize = countText.fontSize;
        StartCoroutine(Count(3, firstSize, time));
    }

    // count number
    IEnumerator Count(int startNum, float firstSize, float time) {
        while (startNum > 0) {
            countText.fontSize = firstSize;
            countText.text = startNum.ToString();
            while (countText.fontSize > 0) {
                countText.fontSize -= firstSize * (0.005f / time);
                yield return null;
            }
            startNum--;
        }
        countText.fontSize = firstSize;
        countText.text = finalText;

        // after some times, destroy object & continue game 
        int interval = 500;
        while (interval-- > 0) {
            yield return null;
        }
        ContinueGame();
    }

    private void ContinueGame() {
        GameObject.Find("Canvas").transform.Find("SettingPopup").GetComponent<SettingManager>().OnContinueGame();
        Destroy(gameObject);
    }
}
