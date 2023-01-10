using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player001 : BaseCharacter {
    private void Start() {
        // 생성
        position = transform;
        speed = 1.0f;
        anim = GetComponent<Animator>();
        clothes = GetComponent<SpriteRenderer>();
        damage = 0;
        isGun = false;
    }
    private void Update() {
        // 이동
        Move();

        // 총
        transform.Find("Gun").gameObject.SetActive(isGun);
    }
}
