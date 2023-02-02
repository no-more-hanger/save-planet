using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Countdown : MonoBehaviour
{
    private GameObject countdownObj;
    private TextMeshProUGUI countText;
    [SerializeField] private string finalText;
    [SerializeField] private float time;

    // Start is called before the first frame update
    void Start()
    {
        countdownObj = this.gameObject;
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
                countText.fontSize -= firstSize * (Time.deltaTime / time);
                yield return null;
            }
            startNum--;
        }
        countText.fontSize = firstSize;
        countText.text = finalText;
        yield return new WaitForSeconds(time);
        DestroyObject();
    }

    private void DestroyObject() {
        Destroy(countdownObj);
    }
}
