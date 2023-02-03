using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : BaseElement {
    private float duration = 5.0f;
    private void Start() {
        direction = Vector3.left;
        anim.SetInteger("alienMode_int", Random.Range(1, 5));
    }
    private void FixedUpdate() {
        MoveHorizontal();
    }
    protected override void AdjustEffect() {
        playerScript.Hurt(10, transform.position);
        playerScript.SetAlienAttack(duration);
    }
}