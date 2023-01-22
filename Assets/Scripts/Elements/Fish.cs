using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : BaseElement {
    [SerializeField] GameObject runningFish = null;
    Rigidbody2D rb = null;

    private void Start() {
        destroyDelay = 0;
        transform.rotation = Quaternion.Euler(0, 180, 0);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right;
    }

    private void Update() {
        // fish movement (go right)
        if (transform.position.x < -2.5) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rb.velocity = Vector2.right;

        }
        // fish movement (go left)
        else if (transform.position.x > 2.5) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rb.velocity = Vector2.left;
        }
    }

    private void OnDestroy() {
        Instantiate(runningFish, transform.position, Quaternion.Euler(0, 0, 0));
    }
}
