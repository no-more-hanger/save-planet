using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : BaseElement {
    private int balloonCnt = 1;

    void Start() {
        destroyDelay = 2f;
        anim.SetInteger("balloon_int", Random.Range(0, 4));
    }
    protected override void AdjustEffect() {
        playerScript.AddBalloon(balloonCnt);
    }
}
