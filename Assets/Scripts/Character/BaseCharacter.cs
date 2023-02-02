using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour {
    private int effectDuration = 100;       // 효과 지속 상태 | 1초
    private float gravityScale = 0.5f;      // 밑으로 끌어내릴(중력) 크기 | transform으로 작용
    private float noDamageTimer = 1f;       // 공격 받은 후, 무적 시간

    private Animator anim;                  // 애니메이션
    private SpriteRenderer spriteRenderer;  // 외관
    private Rigidbody2D rigidbody;

    private float originSpeed;              // 원래 속도
    private float speed;                    // 현재 속도

    private float damage;                   // 데미지
    private bool isGun;                     // 총 소지 여부
    private int balloonCnt;                 // 소지한 풍선 개수

    [SerializeField]
    private GameObject balloonPrefab;       // 풍선 프리팹

    [SerializeField]
    private ParticleSystem effect;          // 이펙트 | 공격 받은 후, 피 효과

    private float itemTimer = 0f;           // 아이템 지속 시간
    private void Start() {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();

        originSpeed = 2f;
        speed = originSpeed;

        damage = 0;
        isGun = false;
        balloonCnt = 0;
    }

    // class get, set
    public float GetDamage() {
        return damage;
    }
    public void SetSpeed(float variation) {
        speed = variation;
    }

    private void Update() {
        // 이동
        Move();

        // 상태 관리
        StateUpdate();

        // 아이템 지속 시간 제어
        itemTimer -= Time.deltaTime;

        // 무적 시간 제어
        noDamageTimer -= Time.deltaTime;
    }

    protected void StateUpdate() {
        // 총 소지 여부에 따라 Gun 비/활성화
        transform.Find("Gun").gameObject.SetActive(isGun);

        // 풍선 생성
        // 현재 풍선 개수 확인
        int CurrentBalloonCnt = transform.Find("Balloons").childCount;
        if (CurrentBalloonCnt >= balloonCnt) {
            Transform[] childList = transform.Find("Balloons").GetComponentsInChildren<Transform>();
            for (int i = CurrentBalloonCnt; i > balloonCnt; i--) {
                Destroy(childList[i].gameObject);
            }
        }
        else {
            GameObject balloonClone = Instantiate(balloonPrefab, GetRandomPosition(), Quaternion.identity);
            // 부모에 상속 정리
            balloonClone.transform.SetParent(transform.Find("Balloons"));
        }

        // 아이템
        transform.Find("DuckFoot").gameObject.SetActive(itemTimer > 0 ? true : false);
        if (itemTimer <= 0) {
            SetSpeed(originSpeed);
        }
    }

    // 랜덤 위치 얻기
    Vector3 GetRandomPosition() {
        GameObject balloonRange = transform.Find("Balloons").gameObject; // 풍선 생성 범위
        BoxCollider2D rangeCollider = balloonRange.GetComponent<BoxCollider2D>();

        Vector3 originPosition = balloonRange.transform.position;
        // 콜라이더의 size
        float rangeX = rangeCollider.bounds.size.x;
        float rangeY = rangeCollider.bounds.size.y;
        // 콜라이더의 offset
        Vector3 offset = rangeCollider.offset;

        rangeX = Random.Range((rangeX / 2) * -1, rangeX / 2);
        rangeY = Random.Range((rangeY / 2) * -1, rangeY / 2);

        Vector3 RandomPostion = new Vector3(rangeX, rangeY, 0f) + offset;
        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }

    // 이동
    public void Move() {
        // 이동 제어
        float x = Input.GetAxisRaw("Horizontal");   // "Horizontal" : 우 방향키(1), 좌 방향키(-1) 리턴
        float y = Input.GetAxisRaw("Vertical");     // "Vertical"   : 상 방향키(1), 하 방향키(-1) 리턴

        // 한 프레임 당 이동거리 계산
        float moveX = x * speed * Time.deltaTime;
        float moveY = (y * speed - gravityScale) * Time.deltaTime;

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
        transform.Translate(new Vector3(moveX, moveY, 0));

        // 이동 범위 제어 : 카메라가 비추는 x 범위 내에서 move하도록 제어
        float height = Camera.main.orthographicSize;        // 화면 가운데로부터 위쪽 끝 or 아래쪽 끝까지의 범위
        float width = height * Screen.width / Screen.height;// 카메라가 비추는 영역의 가로의 절반

        Vector3 pos = transform.position;
        if (pos.x < -width - 0.2f) {
            transform.position = new Vector3(width, pos.y, 0);
        }
        if (pos.x > width + 0.2f) {
            transform.position = new Vector3(-width, pos.y, 0);
        }
    }

    // 데미지 입음
    public void Hurt(float hurt, Vector3 targetPos) {
        if (noDamageTimer > 0) return;      // 무적일 땐 return

        noDamageTimer = 1.0f;               // 무적 시작
        damage += hurt;                     // 데미지 입음

        StartCoroutine(GetDamagedRoutine(targetPos));// 데미지 입은 효과

        if (damage >= 100) {                // 죽음
            rigidbody.simulated = false;
            spriteRenderer.enabled = false;
            GetComponent<Collider2D>().enabled = false;

            effect.Play();              // 피 이펙트
        }
        else {
            Camera.main.GetComponent<CameraController>()?.CameraShake(0.4f, 0.3f);
        }
    }

    // 데미지 치료
    public void Heal(float heal) {
        damage -= heal;
        StartCoroutine(GetHealedRoutine());
    }

    public void SpeedUp(float duration, float variation) {
        itemTimer = duration;
        speed = variation;
    }

    public IEnumerator GetDamagedRoutine(Vector3 targetPos) {
        for (int i = 0; i <= effectDuration; i++) {
            spriteRenderer.color = new Color(1, 0.01f * i, 0.01f * i);  // 빨간색으로 변함

            // 튕겨 나감
            Vector2 velocy = new Vector2(0, -1);    // 무조건 아래로 튀도록 초기화

            // 장애물 || 재해보다 오른쪽에 있으면 오른쪽(1)으로, 왼쪽에 있으면 왼쪽(-1)으로 튀도록
            velocy.x = (transform.position.x - targetPos.x) > 0 ? 1 : -1;

            Vector2 dv = velocy * (effectDuration - i) * Time.deltaTime * 0.05f; // 튕겨나가는 정도
            transform.Translate(new Vector3(dv.x, dv.y, 0));

            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator GetHealedRoutine() {
        for (int i = 0; i <= effectDuration; i++) {
            spriteRenderer.color = new Color(0.01f * i, 1, 0.01f * i);  // 초록색
            yield return new WaitForSeconds(0.01f);
        }
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
        if (balloonCnt - cnt <= 0) {
            balloonCnt = 0;
        }
        else {
            balloonCnt -= cnt;
        }
    }
}
