using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallonUIManager : MonoBehaviour {
    BaseCharacter player;      // player의 풍선 개수 값에 접근하기 위해 태그로 찾음
    TextMeshProUGUI balloonCnt;    // player의 풍선 개수 UI

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<BaseCharacter>();
        balloonCnt = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        balloonCnt.text = player.GetBalloonCnt().ToString();
    }
}
