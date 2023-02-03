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
    private bool isActive;

    [Header("Adjust Gravity")]
    [SerializeField] private float gravityBound;
    [SerializeField] private float gravityConstant;

    [Header("Effect")]
    [SerializeField] private float scaleUpperBound;

    private void Start() {
        playerRb = player.GetComponent<Rigidbody2D>();
        transform.Rotate(Vector3.forward);
        initialScale = transform.localScale;
        destroyDelay = 5.0f;
        isActive = true;
    }

    private void FixedUpdate() {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        if (isActive) {
            ApplyGravity(player, playerRb);
        }
        else {
            ChangeScale(-2f);
        }
    }

    protected override void AdjustEffect() {
        rotationSpeed = 500.0f;
        playerScript.Hurt(30, transform.position);
        gravityBound = 0;
        isActive = false;
    }

    private void ApplyGravity(GameObject obj, Rigidbody2D rb) {
        float distance = Vector3.Distance(gameObject.transform.position, obj.transform.position);
        // apply gravity caused by disaster
        if (distance < gravityBound) {
            // get unit vector for direction of Force
            Vector3 direction = (gameObject.transform.position - obj.transform.position).normalized;
            Vector3 force = direction * gravityConstant / (distance * distance);
            //rb.AddForce(force);
            obj.transform.Translate(force * Time.deltaTime);
            if (transform.localScale.x < scaleUpperBound) {
                ChangeScale(1f);
            }
        }
        // set velocity to (0, 0, 0) 
        else {
            rb.velocity = Vector3.zero;
            if (transform.localScale.x > initialScale.x) {
                ChangeScale(-2f);
            }
        }
    }

    /// <summary>
    /// change scale for given ratio
    /// </summary>
    /// <param name="ratio"></param>
    private void ChangeScale(float ratio) {
        ratio *= Time.deltaTime;
        transform.localScale = Vector3.Scale(new Vector3(1 + ratio, 1 + ratio, 0), transform.localScale);
        return;
    }
}
