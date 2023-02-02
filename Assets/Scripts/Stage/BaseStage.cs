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
    [SerializeField] protected float[] itemPercentage; // item percentage
    [SerializeField] protected float[] listIntervalX; // interval x
    [SerializeField] protected float minIntervalY, maxIntervalY; // interval between items


    protected void Awake() {
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

    private int ChooseItem() {
        float random = Random.Range(0f, 100f);
        float percentage = 0f;

        for(int i = 0; i < itemPercentage.Length; i++) {
            percentage += itemPercentage[i];
            if(random < percentage) {
                return i;
            }
        }
        return 0;
    }

    // exception seaweeds
    private void SetSeaweeds(GameObject seaweeds, float x) {
        float width = Camera.main.orthographicSize / 2.2f;

        if (x < 0) { // left
            seaweeds.transform.position = new Vector2(-width, seaweeds.transform.position.y);
            seaweeds.transform.localEulerAngles = new Vector3(1f, 1f, -70f);
        }
        else { // right
            seaweeds.transform.position = new Vector2(width, seaweeds.transform.position.y);
            seaweeds.transform.localEulerAngles = new Vector3(1f, 1f, 70f);
        }
    }

    // create items
    private void CreateItems() {
        float currentY = player.transform.position.y; // current y point
        while (itemNum > 0) {
            // set item cnt in line
            int num = Random.Range(1, listIntervalX.Length);
            num = Mathf.Min(num, itemNum);

            float y = currentY + Random.Range(minIntervalY, maxIntervalY);
            currentY = y;
            for (int i = 0; i < num; i++) {
                // item minus
                itemNum--;

                // set creating point random
                float x = Random.Range(listIntervalX[i], listIntervalX[i + 1]);
                Vector2 creatingPoint = new Vector2(x, y);

                // set item type by item percentage
                int itemType = ChooseItem();
                GameObject temp = Instantiate(itemPrefabs[itemType], creatingPoint, Quaternion.identity);
                temp.transform.SetParent(this.gameObject.transform.Find("Item").transform);

                if (itemPrefabs[itemType].name == "Seaweeds") { // exception seaweeds
                    SetSeaweeds(temp, x);
                }
            }
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

    // get stage length
    public float GetStageHeight() {
        float oneHeight = backgroundPrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.y * backgroundPrefabs[0].GetComponent<Transform>().localScale.y;
        return backgroundNum * oneHeight;
    }

    // detect end stage (maybe this script apply in BasePlayer.cs)
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Goal")) {
            GameObject.Find("StageManager").GetComponent<BaseStage>().DoneStage();
        }
    }

}
