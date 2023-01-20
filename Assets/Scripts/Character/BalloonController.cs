using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour {
    private float delay = 0.01f; // 작을 수록 천천히 플레이어에게 도달
    private Vector3 originOffset;

    private Transform target;

    private void Start() {
        target = GameObject.FindWithTag("Player").transform;
        originOffset = transform.localPosition;
        //Debug.Log(originOffset);
    }

    public void Update() {
        if (target != null) {
            //Vector2 next = Vector3.Lerp(transform.localPosition, target.position * (-0.15f), delay);
            Vector2 next = Vector3.Lerp(transform.localPosition, target.position, delay);

            //Debug.Log(next);
            Vector3 offset = new Vector3(0, 0.0005f, 0);

            transform.localPosition = Vector3.Lerp(transform.localPosition, transform.parent.localPosition, delay);

        }
    }
}
