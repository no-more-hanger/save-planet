using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour {
    protected Transform position; // 현재 위치

    public float speed;     // 속도
    public float damage;    // 데미지

    public Animator anim;           // 애니메이션
    public SpriteRenderer clothes;  // 옷

    public bool isGun;      // 총 소지 여부

    // 이동
    public void Move() {
        float x = Input.GetAxisRaw("Horizontal");   // "Horizontal" : 우 방향키(1), 좌 방향키(-1) 리턴
        float y = Input.GetAxisRaw("Vertical");     // "Vertical"   : 상 방향키(1), 하 방향키(-1) 리턴

        // 1 프레임 당 이동거리 계산
        float moveX = x * speed * Time.deltaTime;
        float moveY = y * speed * Time.deltaTime;

        // Idle
        if (x == 0.0f) {
            anim.SetBool("isRun", false);
        }
        // Run
        else {
            anim.SetBool("isRun", true);
            anim.SetFloat("DirX", x);   // left(0), right(1)
        }

        position.Translate(new Vector3(moveX, moveY, 0));
    }

    // 속도 조절
    public void ControlSpeed(float variation) {
        speed += variation;
    }

    // 데미지 입음
    public void Hurt(float hurt) {
        damage += hurt;
    }

    // 데미지 치료
    public void Heal(float heal) {
        damage -= heal;
    }

    // 총 메기
    public void PutOnGun() {
        isGun = true;
    }
    // 총 내려놓기
    public void PutDownGun() {
        isGun = false;
    }
}
