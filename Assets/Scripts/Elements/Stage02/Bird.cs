using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : BaseElement {
    [SerializeField] private Sprite[] birdImg;
    [SerializeField] private GameObject runningBirdPrefab;
    private SpriteRenderer spriteRenderer;
    private void Start() {
        direction = Vector3.left;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = birdImg[Random.Range(0, birdImg.Length)];
    }
    private void FixedUpdate() {
        MoveHorizontal();
    }
    protected override void AdjustEffect() {
        playerScript.RemoveBalloon();
        playerScript.Hurt(10, transform.position);
        GameObject runningBird =
        Instantiate(runningBirdPrefab, transform.position, transform.rotation);
        runningBird.GetComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;
    }
}
