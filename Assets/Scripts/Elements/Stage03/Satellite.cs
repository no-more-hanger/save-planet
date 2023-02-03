using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : BaseElement {
    private Vector3 planetPos;
    private float rotateSpeed;

    void Start() {
        planetPos = transform.parent.transform.position;
        rotateSpeed = 80f;
    }

    void FixedUpdate() {
        transform.RotateAround(planetPos, Vector3.back, Time.deltaTime * rotateSpeed);
    }
    protected override void AdjustEffect() {
        playerScript.Hurt(10, transform.position);
    }
}
