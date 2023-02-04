using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BaseElement - Top class of element(Item, Obstacle, Disaster)
/// 
/// Child Class should have
///     - AdjustEffect() : game effect
/// </summary>
public class BaseElement : MonoBehaviour {
    protected GameObject player;
    protected BaseCharacter playerScript;

    [Header("SETTING for Effect on destroy")]
    [SerializeField] protected AudioClip soundEffect = null;   // when destroyed.
    [SerializeField] protected ParticleSystem particle = null; // when destroyed.
    [SerializeField] protected float destroyDelay = 0;
    protected Animator anim = null;


    private float destroyDistance = 5.0f;
    protected Vector3 direction;

    private void Awake() {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<BaseCharacter>();
        anim = GetComponent<Animator>();
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
    /// effect of each items
    /// </summary>
    protected virtual void AdjustEffect() { }

    /// <summary>
    /// play sound effect & destory gameObject
    /// </summary>
    /// <param name="collision"></param>
    protected void OnCollisionEnter2D(Collision2D collision) {
        // play animation if exist
        GetComponent<Collider2D>().enabled = false;

        if (collision.gameObject.CompareTag("Player")) {
            AdjustEffect();
        }

        if (collision.gameObject.CompareTag("Bullet")) {
            Destroy(collision.gameObject);
        }

        // play animation
        if (anim != null) {
            anim.SetTrigger("disappear_trig");
        }
        // give particle effect if exist
        PlayParticleEffect();
        // play sound effect
        if (soundEffect != null) {
            SoundManager._soundInstance.OnAudio(soundEffect);
        }
        Destroy(gameObject, destroyDelay);
    }

    protected void MoveHorizontal() {
        transform.Translate(direction * Time.deltaTime);

        // fish movement (go right)
        if (transform.position.x < -2.5) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        // fish movement (go left)
        else if (transform.position.x > 2.5) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    protected void PlayParticleEffect() {
        if (particle != null) {
            ParticleSystem collisionEffect = Instantiate(particle, transform.position, transform.rotation);
            collisionEffect.Play();
            Destroy(collisionEffect.gameObject, collisionEffect.main.duration);
        }
    }
}
