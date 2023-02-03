using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : BaseElement {
    [SerializeField] public GameObject[] inkImg = new GameObject[4];
    [SerializeField] private GameObject[] inkInstance = new GameObject[4];
    [SerializeField] private AudioClip inkSound;

    private void Start() {
        destroyDelay = 5.0f;
    }
    protected override void AdjustEffect() {
        StartCoroutine(SpreadInk());
        GetComponent<SpriteRenderer>().enabled = false;
        playerScript.Hurt(0, transform.position);
    }

    private IEnumerator SpreadInk() {
        float[] dx = { -2.5f, 0, -2.5f, 0 };
        float[] dy = { 0, 0, -5, -5 };

        float y = Camera.main.transform.position.y;

        for (int i = 0; i < 4; i++) {
            SoundManager._soundInstance.OnAudio(inkSound);
            inkInstance[i] = Instantiate(inkImg[i], new Vector3(Random.Range(dx[i], dx[i] + 2), Random.Range(y + dy[i], y + dy[i] + 4), 0), transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 4; i++) {
            Destroy(inkInstance[i]);
        }
    }
}
