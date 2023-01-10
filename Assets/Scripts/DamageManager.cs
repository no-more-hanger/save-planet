using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageManager : MonoBehaviour {
    Slider damageBarValue;  // 데미지바 value
    GameObject player;      // player의 데미지 값에 접근하기 위해 태그로 찾음
    float playerDamageValue;// player의 데미지 value

    void Start() {
        damageBarValue = GetComponent<Slider>();
    }

    void Update() {
        player = GameObject.FindWithTag("Player");

        playerDamageValue = player.GetComponent<Player001>().damage; // Player001의 데미지

        damageBarValue.value = playerDamageValue;
    }
}
