using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : BaseElement {
    [SerializeField] GameObject runningFish = null;
    private float damage = 5;

    private void Start() {
        destroyDelay = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        direction = Vector3.left;
    }

    private void Update() {
        MoveHorizontal();
    }
    protected override void AdjustEffect() {
        playerScript.Hurt(damage, transform.position);
        Instantiate(runningFish, transform.position, transform.rotation);
    }
}
