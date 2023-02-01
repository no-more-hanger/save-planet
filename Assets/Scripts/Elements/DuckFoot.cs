using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckFoot : BaseElement {
    private float duration = 7.0f;
    private float speedVariation = 2.5f;
    void Start() {
        destroyDelay = 2f;
    }

    protected override void AdjustEffect() {
        playerScript.SpeedUp(duration, speedVariation);
    }
}
