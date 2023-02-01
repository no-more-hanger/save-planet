using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : BaseElement {
    [SerializeField] GameObject runningFish = null;
    private float damage = 5;
    private Vector3 direction;

    private void Start() {
        destroyDelay = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        direction = Vector3.left;
    }

    private void Update() {
        transform.Translate(direction * Time.deltaTime);

        // fish movement (go right)
        if (transform.position.x < -2.5) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        // fish movement (go left)
        else if (transform.position.x > 2.5) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    protected override void AdjustEffect() {
        playerScript.Hurt(damage, transform.position);
    }

    private void OnDestroy() {
        Instantiate(runningFish, transform.position, transform.rotation);
    }
}
