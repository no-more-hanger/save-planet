using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DistanceSlider : MonoBehaviour
{
    private Slider slider;
    private float backgroundHeight;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        backgroundHeight = GameObject.Find("StageManager").GetComponent<BaseStage>().GetStageHeight();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = GameObject.FindWithTag("Player").transform.position.y / backgroundHeight;
    }
}
