using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player001 : BaseCharacter {
    private void Start() {
        // »ý¼º
        position = transform;
        speed = 1.0f;
        anim = GetComponent<Animator>();
        clothes = GetComponent<SpriteRenderer>();
        damage = 0;
        isGun = false;
    }
    private void Update() {
        // ÀÌµ¿
        Move();

        // ÃÑ
        transform.Find("Gun").gameObject.SetActive(isGun);
    }
}
