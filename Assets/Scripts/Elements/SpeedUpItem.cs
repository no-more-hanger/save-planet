using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpItem : BaseElement {
    [Header("Set item spec")]
    [SerializeField] private float duration;
    [SerializeField] private float speedVariation;
    void Start() {
        destroyDelay = 2f;
    }

    protected override void AdjustEffect() {
        playerScript.SpeedUp(duration, speedVariation);
    }
}
