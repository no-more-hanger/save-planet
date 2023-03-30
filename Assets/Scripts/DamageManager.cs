using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageManager : MonoBehaviour {
    Slider damageBarValue;  // 데미지바 value
    BaseCharacter player;      // player의 데미지 값에 접근하기 위해 태그로 찾음
    float playerDamageValue;// player의 데미지 value

    void Start() {
        damageBarValue = GetComponent<Slider>();
        player = GameObject.FindWithTag("Player").GetComponent<BaseCharacter>();
    }

    void Update() {
        playerDamageValue = player.GetDamage(); // Character의 데미지

        damageBarValue.value = playerDamageValue;
    }
}
