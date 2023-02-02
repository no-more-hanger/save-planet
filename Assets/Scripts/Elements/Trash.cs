using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : BaseElement {
    private float damage = 10;
    private float speed = 1;
    protected override void AdjustEffect() {
        playerScript.Hurt(damage, transform.position);
    }

    private void Update() {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
}
