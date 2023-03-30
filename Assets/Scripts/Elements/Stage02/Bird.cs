using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : BaseElement {
    [SerializeField] private Sprite[] birdImg;
    [SerializeField] private GameObject runningBirdPrefab;
    public AnimationCurve animationCurve;

    private int birdNum;
    private float timer = 0.0f;
    private float posY;
    private float[] speed = { 1.5f, 1.25f, 1f, 0.75f, 0.5f };

    private void Start() {
        birdNum = Random.Range(0, birdImg.Length);
        GetComponent<SpriteRenderer>().sprite = birdImg[birdNum];
        posY = transform.position.y;
    }
    private void FixedUpdate() {
        timer += Time.deltaTime;
        transform.Translate(new Vector3(-speed[birdNum] * Time.deltaTime, posY + animationCurve.Evaluate(timer) * speed[birdNum] - transform.position.y));

        if (transform.position.x < GameStaticData._dataInstance.GetResponsivePoint(-2.5f)) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x > GameStaticData._dataInstance.GetResponsivePoint(2.5f)) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    protected override void AdjustEffect() {
        playerScript.RemoveBalloon();
        playerScript.Hurt(10, transform.position);
        GameObject runningBird =
        Instantiate(runningBirdPrefab, transform.position, transform.rotation);
        runningBird.GetComponent<SpriteRenderer>().sprite = birdImg[birdNum];
    }
}
