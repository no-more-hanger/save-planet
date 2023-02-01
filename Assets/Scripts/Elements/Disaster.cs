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
    [SerializeField] private float gravityConstant = 0.5f;

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
        //Debug.Log("destory");
        //playerScript.Hurt(30, transform.position);
        //playerRb.velocity = Vector3.zero;
    }

    protected override void AdjustEffect() {
        playerScript.Hurt(30, transform.position);
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
