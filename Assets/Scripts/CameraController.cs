using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField]
    private Transform _target;

    public void Update() {
        if (_target != null) {
            Vector2 next = Vector3.Lerp(transform.position, _target.position, 0.99f);

            transform.position = new Vector3(transform.position.x, next.y) + offset;
        }
    }

    public void CameraShake(float amount, float time, bool keepAmount = false) {
        StartCoroutine(CameraShakeRoutine(amount, time, keepAmount));
    }

    private IEnumerator CameraShakeRoutine(float amount, float time, bool keepAmount) {
        for (float t = time; t >= 0; t -= Time.deltaTime) {
            Vector3 rand = new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y, 0) * (keepAmount ? amount : Mathf.Lerp(amount, 0, 1 - t / time));

            transform.position += rand;

            yield return null;
        }
    }

    public void SetTarget(Transform target) {
        _target = target;
    }
}
