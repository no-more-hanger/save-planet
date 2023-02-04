using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseCharacter : MonoBehaviour {
    private TimerController timerController;

    private int effectDuration = 100;       // 효과 지속 상태 | 1초
    private float gravityScale = 0.5f;      // 밑으로 끌어내릴(중력) 크기 | transform으로 작용
    private float noDamageTimer = 1f;       // 공격 받은 후, 무적 시간

    private Animator anim;                  // 애니메이션
    private SpriteRenderer spriteRenderer;  // 외관

    private float originSpeed;              // 원래 속도
    private float speed;                    // 현재 속도

    private float damage;                   // 데미지
    private bool isGun;                     // 총 소지 여부
    private int bulletCnt;                  // bullet cnt
    const int bulletMaxCnt = 10;         // bullet max cnt
    private int balloonCnt;                 // 소지한 풍선 개수

    [SerializeField]
    private GameObject balloonPrefab;       // 풍선 프리팹
    private bool isBalloon;                 // 풍선 화면에 보여줄지 여부
    private float balloonCoefficient = 0.01f;       // balloon coefficient 

    [SerializeField]
    private ParticleSystem effect;          // 이펙트 | 공격 받은 후, 거품 or 피 효과

    private float itemTimer;                // 아이템 지속 시간

    [SerializeField]
    public AnimationCurve dieAnimCurve;     // 죽을 때, 아래로 떨어지는 애니메이션을 위한 효과
    private float floatingY = -0.25f;
    private float timer = 0.0f;
    private float posX, posY;
    //private float loopTime = 0.5f;
    //private float mult = 1.0f;


    private bool isMoveX = true; // character move 
    private bool isMoveY = true; // character move

    private float alienAttackTimer = 0.0f;

    private void Start() {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        originSpeed = 2f;
        speed = originSpeed;

        damage = 0;
        isGun = false;
        isBalloon = true;
        bulletCnt = 0;
        balloonCnt = 0;

        timerController = GameObject.FindWithTag("Timer").GetComponent<TimerController>();
    }

    // class get, set
    public float GetDamage() {
        return damage;
    }
    public void SetSpeed(float variation) {
        speed = variation;
    }
    public int GetBalloonCnt() {
        return balloonCnt;
    }
    public float GetSpeed() {
        return speed;
    }
    public bool GetIsMoveX() {
        return isMoveX;
    }
    public bool GetIsMoveY() {
        return isMoveY;
    }
    public void SetIsMoveX(bool flag) {
        isMoveX = flag;
    }
    public void SetIsMoveY(bool flag) {
        isMoveY = flag;
    }
    public void SetAlienAttack(float duration) {
        alienAttackTimer = duration;
        StartCoroutine(GetAlienAttackRoutine());
    }
    public int GetBulletCnt() {
        return bulletCnt;
    }

    private void Update() {
        // 죽음
        if (damage >= 100) { return; }

        // 이동
        Move();

        // 상태 관리
        StateUpdate();

        // 아이템 지속 시간 제어
        itemTimer -= Time.deltaTime;

        // 무적 시간 제어
        noDamageTimer -= Time.deltaTime;

        alienAttackTimer -= Time.deltaTime;
    }

    protected void StateUpdate() {
        // 총 소지 여부에 따라 Gun 비/활성화
        transform.Find("Gun").gameObject.SetActive(isGun);

        // 풍선 생성
        // 현재 풍선 개수 확인
        if (isBalloon) {
            int CurrentBalloonCnt = transform.Find("Balloons").childCount;
            if (CurrentBalloonCnt < balloonCnt) {
                GameObject balloonClone = Instantiate(balloonPrefab, GetRandomPosition(), Quaternion.identity);
                // 부모에 상속 정리
                balloonClone.transform.SetParent(transform.Find("Balloons"));
            }
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
        float x = isMoveX ? Input.GetAxisRaw("Horizontal") : 0;   // "Horizontal" : 우 방향키(1), 좌 방향키(-1) 리턴
        float y = isMoveY ? Input.GetAxisRaw("Vertical") : 0;     // "Vertical"   : 상 방향키(1), 하 방향키(-1) 리턴
        
        if (isBalloon) {
            y += balloonCnt * balloonCoefficient;
        }

        // 외계인 효과 적용
        x = (alienAttackTimer > 0) ? -x : x;

        // 한 프레임 당 이동거리 계산
        float moveX = x * speed * Time.deltaTime;
        float moveY = ((y < 0 ? 0 : y) * speed - gravityScale) * Time.deltaTime; // 아래로는 못 가게 막기

        // Idle
        if (x == 0.0f) {
            anim.SetBool("isRun", false);
        }
        // Run
        else {
            // 아이템 지속 동안엔 애니메이션 x
            if (itemTimer <= 0) {
                anim.SetBool("isRun", true);
            }
            transform.localScale = new Vector3(-x, transform.localScale.y, transform.localScale.z);// left(1), right(-1)
            //anim.SetFloat("DirX", x);   // left(0), right(1)
        }
        // Move
        transform.Translate(new Vector3(moveX, moveY, 0));

        // 이동 범위 제어 : 카메라가 비추는 x 범위 내에서 move하도록 제어
        float height = Camera.main.orthographicSize;        // 화면 가운데로부터 위쪽 끝 or 아래쪽 끝까지의 범위
        float width = height * Screen.width / Screen.height;// 카메라가 비추는 영역의 가로의 절반

        Vector3 pos = transform.position;
        if (pos.x < -width + 0.2f) {
            transform.position = new Vector3(-width + 0.2f, pos.y, 0);
        }
        if (pos.x > width - 0.2f) {
            transform.position = new Vector3(width - 0.2f, pos.y, 0);
        }
    }

    // 데미지 입음
    public void Hurt(float hurt, Vector3 targetPos) {
        if (noDamageTimer > 0) return;      // 무적일 땐 return

        float velocity = 0.05f;              // 반동 시, 튕겨나가는 정도

        noDamageTimer = 1.0f;               // 무적 시작
        damage += hurt;                     // 데미지 입음

        if (damage >= 100) {
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(Die());          // 죽음 효과
            effect.Play();                  // 스테이지 01 : 거품 이펙트

            Camera.main.GetComponent<CameraController>()?.SetTarget(null);

        }
        else {
            if (hurt > 10) {
                StartCoroutine(GetDamagedRedRoutine());                     // 데미지 10 이상일 때만 빨갛게 변함
            }
            StartCoroutine(GetDamagedReboundRoutine(targetPos, velocity));  // 반동 효과
            Camera.main.GetComponent<CameraController>()?.CameraShake(0.4f, 0.3f); // 카메라 흔듦
        }
    }

    // 빨갛게 변하는 효과
    public IEnumerator GetDamagedRedRoutine() {
        for (int i = 0; i <= effectDuration; i++) {
            spriteRenderer.color = new Color(1, 0.01f * i, 0.01f * i);  // 빨간색으로 변함
            yield return new WaitForSeconds(0.01f);
        }
    }
    // 반동 효과
    public IEnumerator GetDamagedReboundRoutine(Vector3 targetPos, float velocity) {
        for (int i = 0; i <= effectDuration; i++) {
            // 튕겨 나감
            Vector2 velocy = new Vector2(0, -1);    // 무조건 아래로 튀도록 초기화

            // 장애물 || 재해보다 오른쪽에 있으면 오른쪽(1)으로, 왼쪽에 있으면 왼쪽(-1)으로 튀도록
            velocy.x = (transform.position.x - targetPos.x) > 0 ? 1 : -1;

            Vector2 dv = velocy * (effectDuration - i) * Time.deltaTime * velocity; // 튕겨나가는 정도
            transform.Translate(new Vector3(dv.x, dv.y, 0));

            yield return new WaitForSeconds(0.01f);
        }
    }

    // 죽음
    public IEnumerator Die() {
        for (int i = 0; i <= 200; i++) {
            //rigidbody.simulated = false;
            //spriteRenderer.enabled = false;

            // 현재 캐릭터 위치
            posX = gameObject.transform.position.x;
            posY = gameObject.transform.position.y;

            timer += Time.deltaTime;

            //if (timer > loopTime) {
            //    mult = 1.0f;
            //}
            //else if (timer < 0.0f) {
            //    mult = -1.0f;
            //}

            //transform.position = new Vector3(posX, posY + dieAnimCurve.Evaluate(timer / loopTime) * floatingY);
            transform.position = new Vector3(posX, posY + dieAnimCurve.Evaluate(timer) * floatingY);
            yield return new WaitForSeconds(0.01f);
        }

        // stop time & show popup
        timerController.StopTimer();
        GameObject.Find("Canvas").transform.Find("DyingPopup").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("SettingPopup").GetComponent<SettingManager>().OnPauseGame();
        GameObject.Find("PlayTimeText").GetComponent<TextMeshProUGUI>().text = timerController.GetTimeString();
    }

    public IEnumerator GetHealedRoutine() {
        for (int i = 0; i <= effectDuration; i++) {
            spriteRenderer.color = new Color(0.01f * i, 1, 0.01f * i);  // 초록색
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator GetAlienAttackRoutine() {
        int toggle = 0;
        while (alienAttackTimer > 0) {
            spriteRenderer.color = new Color(1, toggle, 1);  // 보라색
            toggle = 1 - toggle;
            yield return new WaitForSeconds(0.1f);
        }
        // set back to original
        spriteRenderer.color = new Color(1, 1, 1);
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

    // 총 메기
    public void PutOnGun() {
        isGun = true;
        bulletCnt = bulletMaxCnt;
    }

    // 총 내려놓기
    public void PutDownGun() {
        isGun = false;
        bulletCnt = 0;
    }

    // 총 쏘기
    public void ShootGun() {
        bulletCnt--;
        if (bulletCnt == 0) {
            PutDownGun();
        }
    }

    // 풍선 추가
    public void AddBalloon(int cnt) {
        balloonCnt += cnt;
    }

    // 풍선 제거
    public void RemoveBalloon(int cnt) {
        if (balloonCnt <= 0) {
            return;
        }
        transform.Find("Balloons").transform.GetChild(balloonCnt-1).GetComponent<CharacterBalloon>().DeleteBallon();
        if (balloonCnt - cnt <= 0) {
            
            balloonCnt = 0;
        }
        else {
            balloonCnt -= cnt;
        }
    }
}
