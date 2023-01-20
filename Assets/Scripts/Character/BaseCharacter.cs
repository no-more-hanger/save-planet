using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour {
    private string LAYER_ENEMY = "Enemy";
    private string LAYER_DISASTER = "Disaster";
    private string LAYER_ITEM = "Item";

    private int duration = 100;// 효과 지속 상태

    private float gravityScale = 0.5f;      // 중력 크기

    protected Transform position; // 현재 위치

    public float originSpeed;// 원래 속도
    public float speed;     // 현재 속도
    //public float itemSpeed;     // 아이템 먹은 속도

    public float damage;    // 데미지

    public Animator anim;                   // 애니메이션
    public SpriteRenderer spriteRenderer;   // 외관

    public bool isGun;                      // 총 소지 여부

    protected int balloonCnt;               // 소지한 풍선 개수
    public GameObject balloonPrefab;        // 풍선 프리팹

    protected float noDamageTimer = 1f;     // 무적 시간
    protected Rigidbody2D rigidbody;

    public ParticleSystem effect;

    private Vector2 damagedVelocity;        // 데미지 받았을 시, 튕겨나가는 방향

    protected float itemTimer = 0f;           // 아이템 지속 시간

    // 항상 업데이트 될 부분들
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

        // 무적 시간 제어
        noDamageTimer -= Time.deltaTime;

        // 아이템 지속 시간 제어
        itemTimer -= Time.deltaTime;
        if (itemTimer <= 0) {
            ControlSpeed(originSpeed);
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
        position.Translate(new Vector3(moveX, moveY, 0));

        // 이동 범위 제어 : 카메라가 비추는 x 범위 내에서 move하도록 제어
        float height = Camera.main.orthographicSize;        // 화면 가운데로부터 위쪽 끝 or 아래쪽 끝까지의 범위
        float width = height * Screen.width / Screen.height;// 카메라가 비추는 영역의 가로의 절반
        if (position.position.x < -width - 0.2f) {
            position.position = new Vector3(width, position.position.y, 0);
        }
        if (position.position.x > width + 0.2f) {
            position.position = new Vector3(-width, position.position.y, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        int collsionLayer = collision.gameObject.layer;

        // 장애물 || 재해
        if (collsionLayer == LayerMask.NameToLayer(LAYER_ENEMY) || collsionLayer == LayerMask.NameToLayer(LAYER_DISASTER)) {
            // 튕겨 나감
            damagedVelocity = new Vector2(0, -1);    // 무조건 아래로 튀도록 초기화

            // 장애물 || 재해보다 오른쪽에 있으면 오른쪽(1)으로, 왼쪽에 있으면 왼쪽(-1)으로 튀도록
            damagedVelocity.x = (transform.position.x - collision.transform.position.x) > 0 ? 1 : -1;
            //Hurt(10, collision.transform.position);                           // (임시) item에서 제어
        }
        // 아이템
        if (collsionLayer == LayerMask.NameToLayer(LAYER_ITEM)) {
            Heal(50);                           // (임시) item에서 제어
        }
    }

    // 데미지 입음
    public void Hurt(float hurt) {
        if (noDamageTimer > 0) return;      // 무적일 땐 return

        noDamageTimer = 1.0f;               // 무적 시작
        damage += hurt;                     // 데미지 입음

        StartCoroutine(GetDamagedRoutine());// 데미지 입은 효과

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


    public IEnumerator GetDamagedRoutine() {
        for (int i = 0; i <= duration; i++) {
            spriteRenderer.color = new Color(1, 0.01f * i, 0.01f * i);  // 빨간색으로 변함

            Vector2 dv = damagedVelocity * (duration - i) * Time.deltaTime * 0.05f; // 튕겨나가는 정도
            position.Translate(new Vector3(dv.x, dv.y, 0));

            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator GetHealedRoutine() {
        for (int i = 0; i <= duration; i++) {
            spriteRenderer.color = new Color(0.01f * i, 1, 0.01f * i);  // 초록색
            yield return new WaitForSeconds(0.01f);
        }
    }

    // 속도 조절
    public void ControlSpeed(float variation) {
        speed = variation;
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
        if (balloonCnt <= 0) return;
        balloonCnt -= cnt;
    }

    // 아이템 지속 시간
    public void GetItemTimer(float time) {
        itemTimer = time;
    }
}
