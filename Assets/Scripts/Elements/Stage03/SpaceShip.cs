using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : BaseElement {
    private bool isMoving = false;
    private float speed = 2.0f;
    [SerializeField] private GameObject alienPrefab;

    void FixedUpdate() {
        if (isMoving) {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (playerScript.GetIsMoveX() && playerScript.GetIsMoveY() && transform.position.y <= Camera.main.transform.position.y + 6) {
            anim.SetTrigger("move_trig");
            Vector3 dir = player.transform.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
            isMoving = true;
        }
    }

    protected override void AdjustEffect() {
        playerScript.Hurt(10, transform.position);
        GameObject alien = Instantiate(alienPrefab, transform.position, Quaternion.Euler(Vector3.zero));
        alien.GetComponent<Animator>().SetInteger("alienMode_int", Random.Range(1, 5));

    }
}
