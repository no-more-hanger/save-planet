using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Disaster class - child class of BaseElement
/// </summary>
public class Disaster : BaseElement {
    private Rigidbody2D playerRb;
    private float rotationSpeed = 120.0f;
    private Vector3 initialScale;

    [Header("Adjust Gravity")]
    [SerializeField] private float gravityBound = 1.5f;
    [SerializeField] private float gravityConstant = 0.3f;

    [Header("Effect")]
    [SerializeField] private float scaleUpperBound = 2.5f;
    [SerializeField] private float scaleChangingRate = 0.01f;

    private void Start() {
        playerRb = player.GetComponent<Rigidbody2D>();
        transform.Rotate(Vector3.forward);
        initialScale = transform.localScale;
        destroyDelay = 3.0f;
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
        Debug.Log(distance);
        // apply gravity caused by disaster
        if (distance < gravityBound) {
            // get unit vector for direction of Force
            Vector3 direction = (gameObject.transform.position - obj.transform.position).normalized;
            rb.AddForce(direction * gravityConstant / (distance * distance));
            if (transform.localScale.x < scaleUpperBound) {
                changeScale(scaleChangingRate);
            }
        }
        // set velocity to (0, 0, 0) 
        else {
            rb.velocity = Vector3.zero;
            if (transform.localScale.x > initialScale.x) {
                changeScale(-scaleChangingRate);
            }
        }
    }

    /// <summary>
    /// change scale for given ratio
    /// </summary>
    /// <param name="ratio"></param>
    private void changeScale(float ratio) {
        transform.localScale = Vector3.Scale(new Vector3(1 + ratio, 1 + ratio, 0), transform.localScale);
        return;
    }
}
