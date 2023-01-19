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

        speed = 2f;
        damage = 0;
        isGun = false;
        balloonCnt = 0;
    }
    private void Update() {
        // 이동
        Move();

        StateUpdate();
    }
}
