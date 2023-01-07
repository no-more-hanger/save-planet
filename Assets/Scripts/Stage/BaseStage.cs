using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStage : MonoBehaviour
{
    // character
    protected GameObject characterObj;

    // stage level
    protected int stageLevel;

    // background image count (stage height)
    protected int backgroundNum;

    // background image routine count
    protected int backgroudRoutine = 2;

    // items
    [SerializeField] protected GameObject[] itemPrefabs;

    // obstacles
    [SerializeField] protected GameObject[] obstaclePrefabs;

    // bgm
    [SerializeField] protected AudioClip backgroundMusic;

    // background sprites
    [SerializeField] protected GameObject[] backgroundPrefabs;
    

    // Start is called before the first frame update
    void Start()
    {
        // create background
        CreateBackground();

        // create items
        // create obstacles
        // fade in (camera)
    }

    // create background
    public void CreateBackground() {
        float startPoint = backgroundPrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.y - Camera.main.orthographicSize;
        for (int i = 0; i < backgroundNum; i++) {
            startPoint = CreateOneBackground(backgroundPrefabs[i % backgroudRoutine], startPoint);
        }
        // 꼭대기 배경
        CreateOneBackground(backgroundPrefabs[backgroundPrefabs.Length - 1], startPoint);
    }
    float CreateOneBackground(GameObject obj, float startPoint) {
        float height = obj.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        float newY = startPoint + height / 2;

        Vector2 creatingPoint = new Vector2(0, newY);
        GameObject temp = Instantiate(obj, creatingPoint, Quaternion.identity);
        temp.transform.parent = this.gameObject.transform;

        return startPoint + height;
    }

    // create items


    // create obstacles

    // fade in (camera)

    // fade out 

    // load next stage

}
