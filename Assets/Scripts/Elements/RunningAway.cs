using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAway : MonoBehaviour {
    [Header("Speed")]
    [SerializeField] private float speed = 50.0f;
    [SerializeField] private float freezeDelay = 0.5f;

    void Start() {
        Invoke("RunAway", freezeDelay);
    }
    private void Update() {
        if (transform.position.x < -5) {
            Destroy(gameObject);
        }
    }
    private void RunAway() {
        GetComponent<Rigidbody2D>().velocity = Vector3.left * speed;
    }
}
