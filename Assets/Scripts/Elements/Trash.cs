using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : BaseElement {
    private float damage = 10;
    private float speed = 0;
    protected override void AdjustEffect() {
        playerScript.Hurt(damage, transform.position);
    }

    private void FixedUpdate() {
        if (transform.position.y < Camera.main.transform.position.y + 5.5f && playerScript.GetIsMoveX() && playerScript.GetIsMoveY()) {
            speed = 1;
        }
        if (transform.position.y < -2.6) {
            speed = 0;
        }
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
}
