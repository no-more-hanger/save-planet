using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBalloon : MonoBehaviour {
    [SerializeField] private AudioClip soundEffect = null;   // when destroyed.
    private float destroyDelay = 3f;
    private Animator anim;
    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public void SetColor(int number) {
        anim.SetInteger("balloon_int", number);
    }

    public void DeleteBalloon() {
        // play animation
        anim.SetTrigger("disappear_trig");

        // play sound effect
        SoundManager._soundInstance.OnAudio(soundEffect);

        Destroy(gameObject, destroyDelay);
    }
}
