using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStage : MonoBehaviour
{
    protected GameObject player; // character
    protected float fadeTime = 3f;

    [Header("This is background settings")]
    [SerializeField] protected int backgroundNum; // background image count (stage height)
    [SerializeField] protected int backgroudRoutine = 2; // background image routine count
    [SerializeField] protected GameObject[] backgroundPrefabs; // background sprites
    [SerializeField] protected SpriteRenderer backgroundBlack; // black background

    [Header("This is item settings")]
    [SerializeField] protected int itemNum;
    [SerializeField] protected GameObject[] itemPrefabs; // item objects
    [SerializeField] protected float minIntervalY, maxIntervalY; // interval between items

    protected void Awake()
    {
        player = GameObject.FindWithTag("Player");
        CreateBackground(); // create background
        CreateItems(); // create items
        FadeIn(fadeTime); // fade in
    }

    // create background
    private void CreateBackground() {
        if (backgroundPrefabs.Length == 0) {
            return;
        }
        float startPoint = -backgroundPrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.y * backgroundPrefabs[0].GetComponent<Transform>().localScale.y;
        for (int i = 0; i < backgroundNum; i++) {
            startPoint = CreateOneBackground(backgroundPrefabs[i % backgroudRoutine], startPoint);
        }
        // 꼭대기 배경
        CreateOneBackground(backgroundPrefabs[backgroundPrefabs.Length - 1], startPoint);
    }
    private float CreateOneBackground(GameObject obj, float startPoint) {
        float height = obj.GetComponent<SpriteRenderer>().sprite.bounds.size.y * obj.GetComponent<Transform>().localScale.y;
        float newY = startPoint + height;

        Vector2 creatingPoint = new Vector2(0, newY);
        GameObject temp = Instantiate(obj, creatingPoint, Quaternion.identity);
        temp.transform.SetParent(this.gameObject.transform.Find("Background").transform);

        return startPoint + height;
    }

    // create items
    private void CreateItems() {
        float minX = -Camera.main.orthographicSize / 2.0f; // range min x
        float maxX = Camera.main.orthographicSize / 2.0f; // range max x
        float currentY = player.transform.position.y; // current y point
        for (int i = 0; i < itemNum; i++) {
            // set creating point random
            float x = Random.Range(minX, maxX);
            float y = currentY + Random.Range(minIntervalY, maxIntervalY);
            Vector2 creatingPoint = new Vector2(x, y);
            currentY = y;

            // set item type random
            int itemType = Random.Range(0, itemPrefabs.Length);
            GameObject temp = Instantiate(itemPrefabs[itemType], creatingPoint, Quaternion.identity);
            temp.transform.SetParent(this.gameObject.transform.Find("Item").transform);
        }
    }

    // fade in
    private void FadeIn(float time) {
        StartCoroutine(FadeEffect(1f, -time));
    }


    // fade out
    private void FadeOut(float time) {
        StartCoroutine(FadeEffect(0f, time));
    }

    IEnumerator FadeEffect(float start, float time) {
        backgroundBlack.gameObject.SetActive(true);
        backgroundBlack.color = new Color(backgroundBlack.color.r, backgroundBlack.color.g, backgroundBlack.color.b, start);
        while (backgroundBlack.color.a != (1f - start)) {
            backgroundBlack.color = new Color(backgroundBlack.color.r, backgroundBlack.color.g, backgroundBlack.color.b, backgroundBlack.color.a + Time.deltaTime / time);
            yield return null;
        }
        backgroundBlack.gameObject.SetActive(false);
    }

    // load function next scene in ChangeScene
    private void OnLoadNextScene() {
        GameObject.Find("ChangeScene").GetComponent<ChangeScene>().LoadNextScene();
    }

    // done stage
    public void DoneStage() {
        FadeOut(fadeTime);
        Invoke(nameof(OnLoadNextScene), fadeTime);
    }

    // detect end stage (maybe this script apply in BasePlayer.cs)
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Goal")) {
            GameObject.Find("StageManager").GetComponent<BaseStage>().DoneStage();
        }
    }

}
