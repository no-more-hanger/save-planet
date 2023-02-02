using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckController : MonoBehaviour {
    private ParticleSystem duckEffect;      // 이펙트 | 오리발 거품 효과
    private BaseCharacter player;
    private ParticleSystem.MainModule main;

    void Start() {
        duckEffect = GetComponent<ParticleSystem>();
        main = duckEffect.main;

        player = GameObject.FindWithTag("Player").GetComponent<BaseCharacter>();
    }

    // Update is called once per frame
    void Update() {
        float y = Input.GetAxisRaw("Vertical");     // "Vertical"   : 상 방향키(1), 하 방향키(-1) 리턴
        main.simulationSpeed = y >= 1 ? 2.5f : 1f;
    }
}
