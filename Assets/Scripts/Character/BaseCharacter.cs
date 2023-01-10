using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour {
    protected Transform position; // ���� ��ġ

    public float speed;     // �ӵ�
    public float damage;    // ������

    public Animator anim;           // �ִϸ��̼�
    public SpriteRenderer clothes;  // ��

    public bool isGun;      // �� ���� ����

    // �̵�
    public void Move() {
        float x = Input.GetAxisRaw("Horizontal");   // "Horizontal" : �� ����Ű(1), �� ����Ű(-1) ����
        float y = Input.GetAxisRaw("Vertical");     // "Vertical"   : �� ����Ű(1), �� ����Ű(-1) ����

        // 1 ������ �� �̵��Ÿ� ���
        float moveX = x * speed * Time.deltaTime;
        float moveY = y * speed * Time.deltaTime;

        // Idle
        if (x == 0.0f) {
            anim.SetBool("isRun", false);
        }
        // Run
        else {
            anim.SetBool("isRun", true);
            anim.SetFloat("DirX", x);   // left(0), right(1)
        }

        position.Translate(new Vector3(moveX, moveY, 0));
    }

    // �ӵ� ����
    public void ControlSpeed(float variation) {
        speed += variation;
    }

    // ������ ����
    public void Hurt(float hurt) {
        damage += hurt;
    }

    // ������ ġ��
    public void Heal(float heal) {
        damage -= heal;
    }

    // �� �ޱ�
    public void PutOnGun() {
        isGun = true;
    }
    // �� ��������
    public void PutDownGun() {
        isGun = false;
    }
}
