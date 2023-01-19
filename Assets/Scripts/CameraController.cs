using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Vector3 offset = new Vector3(0, 0.025f, -10);
    private float delay = 0.01f; // 작을 수록 천천히 플레이어에게 도달

    [SerializeField]
    private Transform target;

    public void Update() {
        if (target != null) {
            Vector2 next = Vector3.Lerp(transform.position, target.position, delay);

            transform.position = new Vector3(transform.position.x, next.y) + offset;
        }
    }

    public void CameraShake(float amount, float time, bool keepAmount = false) {
        StartCoroutine(CameraShakeRoutine(amount, time, keepAmount));
    }

    private IEnumerator CameraShakeRoutine(float amount, float time, bool keepAmount) {
        for (float t = time; t >= 0; t -= Time.deltaTime) {
            Vector3 rand = new Vector3(0, Random.insideUnitCircle.y, 0) * (keepAmount ? amount : Mathf.Lerp(amount, 0, 1 - t / time));

            transform.position += rand;

            yield return null;
        }
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }
}
