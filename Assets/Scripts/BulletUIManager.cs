using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletUIManager : MonoBehaviour
{
    BaseCharacter player;      
    TextMeshProUGUI bulletCnt;    

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<BaseCharacter>();
        bulletCnt = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        bulletCnt.text = player.GetBulletCnt().ToString();
    }
}
