using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    public AnimationCurve animationCurve;   // 상하로 흔듦

    private float floatingY = 0.25f;
    private float loopTime = 0.5f;

    private float timer = 0.0f;
    private float posX, posY;
    private float mult = 1.0f;

    private void Start() {
    }

    private void Update() {
        Vector3 parentPos = gameObject.transform.parent.gameObject.transform.position; // 현재 캐릭터 위치
        posX = parentPos.x + 0.115f;
        posY = parentPos.y - 0.28f;

        timer += Time.deltaTime * mult;

        if (timer > loopTime) {
            mult = -1.0f;
        }
        else if (timer < 0.0f) {
            mult = 1.0f;
        }

        transform.position = new Vector3(posX, posY + animationCurve.Evaluate(timer / loopTime) * floatingY);
    }
}
