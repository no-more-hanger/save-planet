using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageManager : MonoBehaviour {
    Slider damageBarValue;  // �������� value
    GameObject player;      // player�� ������ ���� �����ϱ� ���� �±׷� ã��
    float playerDamageValue;// player�� ������ value

    void Start() {
        damageBarValue = GetComponent<Slider>();
    }

    void Update() {
        player = GameObject.FindWithTag("Player");

        playerDamageValue = player.GetComponent<Player001>().damage; // Player001�� ������

        damageBarValue.value = playerDamageValue;
    }
}
