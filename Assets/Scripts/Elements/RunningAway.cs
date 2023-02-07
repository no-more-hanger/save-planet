using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAway : MonoBehaviour {
    [Header("Speed")]
    [SerializeField] private float speed;
    [SerializeField] private float freezeDelay = 0.5f;
    private Vector3 direction;
    private float bound;

    void Start() {
        Invoke("RunAway", freezeDelay);
        bound = GameStaticData._dataInstance.GetResponsivePoint(5f);
    }
    private void FixedUpdate() {
        transform.Translate(direction * Time.deltaTime * speed);
        if (transform.position.x < -bound || transform.position.x > bound) {
            Destroy(gameObject);
        }
    }
    private void RunAway() {
        direction = Vector3.left;
    }
}
