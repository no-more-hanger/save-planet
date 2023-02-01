using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : BaseElement {
    private float damage = 10;
    protected override void AdjustEffect() {
        playerScript.Hurt(damage, transform.position);
    }
}
