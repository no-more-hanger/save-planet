using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Disaster class - child class of BaseElement
/// </summary>
public class Disaster : BaseElement {
    private float rotationSpeed = 120.0f;
    private Vector3 initialScale;
    private bool isActive;

    [Header("Adjust Gravity")]
    private float gravityBound;
    private float gravityConstant = 1.2f;

    [Header("Effect")]
    private float scaleUpperBound;

    private void Start() {
        gravityBound = GameStaticData._dataInstance.GetResponsivePoint(1.8f);
        scaleUpperBound = GameStaticData._dataInstance.GetResponsivePoint(3f);

        transform.Rotate(Vector3.forward);
        initialScale = transform.localScale;
        destroyDelay = 5.0f;
        isActive = true;
    }

    private void FixedUpdate() {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);

        // after hit
        if (!isActive) {
            ChangeScale(-2f);
        }
        // before hit && player is not in damaged layer
        else if (player.layer == LayerMask.NameToLayer("Player")) {
            ApplyGravity(player);
        }
    }

    protected override void AdjustEffect() {
        rotationSpeed = 500.0f;
        playerScript.Hurt(30, transform.position);
        gravityBound = 0;
        isActive = false;
    }

    private void ApplyGravity(GameObject obj) {
        float distance = Vector3.Distance(gameObject.transform.position, obj.transform.position);
        // apply gravity caused by disaster
        if (distance < gravityBound) {
            // get unit vector for direction of Force
            Vector3 direction = (gameObject.transform.position - obj.transform.position).normalized;
            Vector3 force = direction * gravityConstant / (distance * distance);
            obj.transform.Translate(force * Time.deltaTime);
            if (transform.localScale.x < scaleUpperBound) {
                ChangeScale(1f);
            }
        }
        else {
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
