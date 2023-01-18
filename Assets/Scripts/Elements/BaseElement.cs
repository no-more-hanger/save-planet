using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BaseElement - Top class of element(Item, Obstacle, Disaster)
/// 
/// Child Class should have
///     - OnDestory() : game effect
/// </summary>
public class BaseElement : MonoBehaviour {
    protected GameObject player;

    [Header("SETTING for Effect on destroy")]
    [SerializeField] protected AudioClip soundEffect = null;   // when destroyed.
    [SerializeField] protected ParticleSystem particle = null; // when destroyed.

    private float destroyDistance = 5.0f;

    private void Awake() {
        player = GameObject.FindWithTag("Player");
    }

    /// <summary>
    /// delete item if item is behind the player
    /// </summary>
    private void Update() {
        if (player.transform.position.y - transform.position.y > destroyDistance) {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// play sound effect & destory gameObject
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision) {
        if (soundEffect != null) {
            ButtonSound._buttonInstance.OnAudio(soundEffect);
        }
        if (particle != null) {
            ParticleSystem collisionEffect = Instantiate(particle, transform.position, transform.rotation);
            collisionEffect.Play();
            Destroy(collisionEffect, collisionEffect.main.duration);
        }
        Destroy(gameObject);
    }
}
