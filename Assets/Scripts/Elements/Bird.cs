using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : BaseElement {
    private void Start() {
        direction = Vector3.left;
        destroyDelay = 2.0f;
    }
    private void Update() {
        MoveHorizontal();
    }
    protected override void AdjustEffect() {
        playerScript.Hurt(10, transform.position);
        transform.Find("SurpriseMark").GetComponent<SpriteRenderer>().enabled = true;
        direction = Vector3.zero;
    }
}
