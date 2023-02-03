using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : BaseElement {
    private float duration = 5.0f;
    protected override void AdjustEffect() {
        playerScript.SetAlienAttack(duration);
    }
}
