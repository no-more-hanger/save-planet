using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Disaster class - child class of BaseElement
/// 
/// </summary>
public class Disaster : BaseElement {
    new Rigidbody2D rigidbody;
    private Rigidbody2D playerRb;
    private float rotationSpeed = 120.0f;

    [Header("Adjust Gravity")]
    private float gravityBound = 3.0f;
    private float gravityConstant = 1.0f;

    private void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.mass = 1000000;
        playerRb = player.GetComponent<Rigidbody2D>();
        transform.Rotate(Vector3.forward);
    }

    private void FixedUpdate() {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        ApplyGravity(player, playerRb);
    }

    private void OnDestroy() {
        playerRb.velocity = Vector3.zero;
    }

    private void ApplyGravity(GameObject obj, Rigidbody2D rb) {
        float distance = Vector3.Distance(gameObject.transform.position, obj.transform.position);

        // apply gravity caused by disaster
        if (distance < gravityBound) {
            // get unit vector for direction of Force
            Vector3 direction = (gameObject.transform.position - obj.transform.position).normalized;
            rb.AddForce(direction * gravityConstant / (distance * distance));
        }
        // set velocity to (0, 0, 0) 
        else {
            rb.velocity = Vector3.zero;
        }
    }
}
