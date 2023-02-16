using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedalSpeed : MonoBehaviour
{
    [SerializeField] private float maxRange = 34;
    [SerializeField] private float standardValue = 17;

    private int numOffset = 47; // start 1
    private int alphaOffset = 56; // start 11

    // Update is called once per frame
    void Update() {
        if (Input.anyKey) {
            Debug.Log("현재 키: "+ Input.inputString);
            if (string.Compare(Input.inputString, "0") != -1 && string.Compare(Input.inputString, "9") != 1) {
                CalcSpeed((int)Input.inputString[0] - numOffset);
            }
            else if (string.Compare(Input.inputString, "C") != -1 && string.Compare(Input.inputString, "Z") != 1) {
                CalcSpeed((int)Input.inputString[0] - alphaOffset);
            }
        }
        else {
            CalcSpeed(0);
        }
    }
    private void CalcSpeed(int keyValue) {
        Debug.Log("현재 속도: " + keyValue);
        float speed = keyValue / standardValue;
        GameObject.FindWithTag("Player").GetComponent<BaseCharacter>().SetPedalSpeed(speed);
    }
}
