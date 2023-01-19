using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour {
    private string LAYER_ENEMY = "Enemy";
    private float gravityScale = 0.5f;      // 중력 크기

    protected Transform position; // 현재 위치

    public float speed;     // 속도
    public float damage;    // 데미지

    public Animator anim;                   // 애니메이션
    public SpriteRenderer spriteRenderer;   // 외관

    public bool isGun;                      // 총 소지 여부

    protected int balloonCnt;               // 소지한 풍선 개수

    protected float noDamageTimer = 1f;   // 무적 시간
    protected Rigidbody2D rigidbody;

    public ParticleSystem effect;

    // 항상 업데이트 될 부분들
    protected void StateUpdate() {
        // 총 소지 여부에 따라 Gun 비/활성화
        transform.Find("Gun").gameObject.SetActive(isGun);
        // 무적 시간 제어
        noDamageTimer -= Time.deltaTime;
    }

    // 이동
    public void Move() {
        // 이동 제어
        float x = Input.GetAxisRaw("Horizontal");   // "Horizontal" : 우 방향키(1), 좌 방향키(-1) 리턴
        float y = Input.GetAxisRaw("Vertical");     // "Vertical"   : 상 방향키(1), 하 방향키(-1) 리턴

        // 한 프레임 당 이동거리 계산
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
        // Move
        position.Translate(new Vector3(moveX, moveY, 0));

        // 이동 범위 제어 : 카메라가 비추는 x 범위 내에서 move하도록 제어
        float height = Camera.main.orthographicSize;        // 화면 가운데로부터 위쪽 끝 or 아래쪽 끝까지의 범위
        float width = height * Screen.width / Screen.height;// 카메라가 비추는 영역의 가로의 절반
        if (position.position.x < -width) {
            position.position = new Vector3(-width, position.position.y, 0);
        }
        if (position.position.x > width) {
            position.position = new Vector3(width, position.position.y, 0);
        }

        // 중력 제어
        if (noDamageTimer > 0) return;  // 공격 받았을 땐 튕겨나가는 velocity 적용을 위해 중력 제어 안 받고 return
        rigidbody.velocity = new Vector3(0, -gravityScale, 0); // 오브젝트 y방향으로 끌어내림
    }

    // 속도 조절
    public void ControlSpeed(float variation) {
        speed += variation;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer(LAYER_ENEMY)) {
            Vector2 velocity = Vector2.zero;    // Vector2(0, 0) 초기화

            // 적보다 오른쪽에 있으면 오른쪽(1)으로, 왼쪽에 있으면 왼쪽(-1)으로 튀도록
            velocity.x = (transform.position.x - collision.transform.position.x) > 0 ? 1 : -1;
            velocity.y = -1; // 무조건 아래로 튀도록

            rigidbody.velocity = velocity * 1.5f;  // 튀어나가는 정도

            Hurt(50);                           // (임시) item에서 제어
        }
    }

    // 데미지 입음
    public void Hurt(float hurt) {
        if (noDamageTimer > 0) return;  // 무적일 땐 return

        noDamageTimer = 1.0f;           // 무적 시간 시작
        damage += hurt;                 // 데미지 입음

        StartCoroutine(GetDamagedRoutine());

        if (damage >= 100) {            // 죽음
            rigidbody.simulated = false;
            spriteRenderer.enabled = false;
            GetComponent<Collider2D>().enabled = false;

            effect.Play();              // 피 이펙트
        }
        else {
            Camera.main.GetComponent<CameraController>()?.CameraShake(0.4f, 0.3f);
        }
    }

    public IEnumerator GetDamagedRoutine() {
        for (int i = 0; i <= 100; i++) {
            spriteRenderer.color = new Color(1, 0.01f * i, 0.01f * i);  // 빨간색
            yield return new WaitForSeconds(0.01f);
        }
    }

    // 데미지 치료
    public void Heal(float heal) {
        damage -= heal;
    }

    // 총 메기
    public void PutOnGun() {
        isGun = true;
    }
    // 총 내려놓기
    public void PutDownGun() {
        isGun = false;
    }

    // 풍선 추가
    public void AddBalloon(int cnt) {
        balloonCnt += cnt;
    }

    // 풍선 제거
    public void RemoveBalloon(int cnt) {
        balloonCnt -= cnt;
    }
}
