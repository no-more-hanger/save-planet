using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : BaseElement {
    [Header("Set Damage for prefab")]
    [SerializeField] private float damage;

    protected override void AdjustEffect() {
        playerScript.Hurt(damage, transform.position);
    }
}
