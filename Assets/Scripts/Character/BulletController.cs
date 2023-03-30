using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private float destroyDistance = 5.0f;

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.y + destroyDistance + GetComponent<SpriteRenderer>().sprite.bounds.size.y < transform.position.y) {
            Destroy(gameObject);
        }

        transform.Translate(Vector3.up * Time.deltaTime * speed, Space.World);
    }
}
