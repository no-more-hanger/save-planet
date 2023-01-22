using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAway : MonoBehaviour {
    [Header("Speed")]
    [SerializeField] protected float speed = 50.0f;
    void Start() {
        Invoke("RunAway", 0.5f);
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
