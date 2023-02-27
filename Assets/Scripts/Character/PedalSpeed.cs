using UnityEngine;

public class PedalSpeed : MonoBehaviour {
    [SerializeField] private float maxRange = 34;
    [SerializeField] private float standardValue = 17;

    private readonly int numOffset = 47; // start 1
    private readonly int alphaOffset = 56; // start 11
    private float time = 0f;
    private float intervalT = 0f;
    private int keyValue = 0;

    // Update is called once per frame
    void Update() {
        if (intervalT > 0f) {
            time += Time.deltaTime;
        }

        if (Input.anyKey && Input.inputString.Length > 0) {
            Debug.Log("현재 키: " + Input.inputString);
            time = 0f;
            keyValue = 0;
            if (Input.inputString[0] >= '0' && Input.inputString[0] <= '9') {
                keyValue = (int)Input.inputString[0] - numOffset;
            }
            else if (Input.inputString[0] >= 'C' && Input.inputString[0] <= 'Z') {
                keyValue = (int)Input.inputString[0] - alphaOffset;
            }
            if (keyValue != 0) {
                CalcSpeed(keyValue);
                CalcInterval(keyValue);
            }
        }
        else if (intervalT > 0f && time > intervalT) {
            Debug.Log("간격만큼 시간 지나서 속도 감소한다");
            time = 0f; // time init
            keyValue /= 2; // speed down 50%
            CalcInterval(keyValue);
            CalcSpeed(keyValue);
        }
    }
    private void CalcSpeed(int keyValue) {
        float speed = keyValue / standardValue;
        Debug.Log("현재 속도: " + speed);
        GameObject.FindWithTag("Player").GetComponent<BaseCharacter>().SetPedalSpeed(speed);
    }

    private void CalcInterval(int keyValue) {
        // 60 / (key level) * 4 = time interval (s)
        if (keyValue > 0) {
            intervalT = 60f / (keyValue * 4);
            Debug.Log("간격: " + intervalT);
        }
    }
}
