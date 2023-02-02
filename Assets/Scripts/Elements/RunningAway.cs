using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAway : MonoBehaviour {
    [Header("Speed")]
    [SerializeField] private float speed = 80.0f;
    [SerializeField] private float freezeDelay = 0.5f;
    private Vector3 direction;

    void Start() {
        Invoke("RunAway", freezeDelay);
    }
    private void Update() {
        transform.Translate(direction * Time.deltaTime * speed);
        if (transform.position.x < -5 || transform.position.x > 5) {
            Destroy(gameObject);
        }
    }
    private void RunAway() {
        direction = Vector3.left;
    }
}
