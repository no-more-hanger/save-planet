using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    public AnimationCurve animationCurve;   // 상하로 흔듦
    public GameObject bulletPrefab; // bullet
    public AudioClip soundEffect;

    private float floatingY = 0.25f;
    private float loopTime = 0.5f;

    private float timer = 0.0f;
    private float posX, posY;
    private float mult = 1.0f;

    private void Start() {
    }

    private void Update() {
        // shooting
        Shooting();

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

    private void Shooting() {
        if (Input.GetKeyDown(KeyCode.A)) {
            SoundManager._soundInstance.OnAudio(soundEffect);
            gameObject.transform.parent.gameObject.GetComponent<BaseCharacter>().ShootGun();
            GameObject temp = Instantiate(bulletPrefab, gameObject.transform.parent.transform.position, Quaternion.identity); // create bullet
            temp.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
        }
    }
}
