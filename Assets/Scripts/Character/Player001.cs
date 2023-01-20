using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player001 : BaseCharacter {
    private void Start() {
        // 생성
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        position = transform;

        originSpeed = 2f;
        speed = originSpeed;
        //itemSpeed = 3.5f;

        damage = 0;
        isGun = false;
        balloonCnt = 2;
    }
    private void Update() {
        // 이동
        Move();

        // 상태 관리
        StateUpdate();

        // 아이템
        transform.Find("DuckFoot").gameObject.SetActive(itemTimer > 0 ? true : false);
    }
}
