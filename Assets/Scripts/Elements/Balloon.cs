using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : BaseElement {
    private int balloonCnt = 1;
    private int color;

    void Start() {
        destroyDelay = 2f;
        color = Random.Range(0, 4);
        anim.SetInteger("balloon_int", color);
    }
    protected override void AdjustEffect() {
        playerScript.AddBalloon(balloonCnt, color);
    }
}
