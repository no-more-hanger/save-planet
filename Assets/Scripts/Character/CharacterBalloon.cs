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

    // Start is called before the first frame update
    void Start() {
        Vector3 dir = GameObject.FindWithTag("Player").transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    public void SetColor(int number) {
        anim.SetInteger("balloon_int", number);
    }

    public void DeleteBallon() {
        // play animation
        Debug.Log(anim);
        anim.SetTrigger("disappear_trig");

        // play sound effect
        SoundManager._soundInstance.OnAudio(soundEffect);

        Destroy(gameObject, destroyDelay);
    }
}
