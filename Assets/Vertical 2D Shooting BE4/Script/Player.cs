using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어의 이동 속도와 공격 관련 변수
    public float speed; // 이동 속도
    public float MaxShotDelay; // 최대 발사 딜레이
    public float CurShotDelay; // 현재 발사 딜레이
    public int Power; // 현재 파워 레벨
    public int maxPower; // 최대 파워 레벨
    public int boom; // 현재 폭탄 개수
    public int maxboom; // 최대 폭탄 개수

    // 플레이어의 라이프와 점수
    public int life; // 남은 라이프 수
    public int score; // 현재 점수

    // 화면 경계와의 충돌 상태를 나타내는 변수
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;

    // 플레이어가 발사하는 총알 및 폭탄 이펙트 오브젝트
    public GameObject bulletObjA; // 기본 총알
    public GameObject bulletObjB; // 강화 총알
    public GameObject boomEffect; // 폭탄 이펙트

    // 게임 매니저 및 오브젝트 매니저
    public GameManager gameManager;
    public ObjectManager objectManager;

    // 플레이어 상태 관련 변수
    public bool isHit; // 현재 플레이어가 피격 상태인지 여부
    public bool isBoomTime; // 폭탄 사용 가능 여부

    // 플레이어를 따라다니는 보조 오브젝트
    public GameObject[] followers;
    public bool isRespawnTime; // 리스폰 상태인지 여부

    // 애니메이션 및 스프라이트 렌더러
    Animator animator;
    SpriteRenderer spriteRenderer;

    // 조이스틱 및 컨트롤 관련 변수
    public bool[] joyControl;
    public bool isControl;
    public bool isButtonA; // 공격 버튼 상태
    public bool isButtonB; // 폭탄 버튼 상태

    // Awake: 초기화 작업을 수행
    void Awake()
    {
        // Animator와 SpriteRenderer 컴포넌트 가져오기
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // OnEnable: 오브젝트 활성화 시 호출
    void OnEnable()
    {
        // 무적 상태 활성화
        Unbeatable();
        Invoke("Unbeatable", 3); // 3초 후 무적 상태 해제
    }
    void Unbeatable()
    {
        isRespawnTime = !isRespawnTime; // 무적 상태를 토글
        if (isRespawnTime)
        {
            // 무적 상태에서 반투명 처리
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            foreach (var follower in followers)
            {
                follower.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        else
        {
            // 무적 상태 해제 후 원래 색상으로 복구
            spriteRenderer.color = new Color(1, 1, 1, 1);
            foreach (var follower in followers)
            {
                follower.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }
    // 매 프레임 호출되는 업데이트 함수
    void Update()
    {
        Move();   // 이동 처리
        Fire();   // 총알 발사
        Boom();   // 폭탄 사용
        Reload(); // 발사 딜레이 갱신
    }
    // 조이스틱 입력 처리 함수
    public void JoyPanel(int type)
    {
        for (int index = 0; index < 9; index++)
        {
            joyControl[index] = index == type;
        }
    }
    public void JoyDown() { isControl = true; }
    public void JoyUp() { isControl = false; }

    // 플레이어 이동 처리 함수
    void Move()
    {
        // 키보드 입력값 가져오기
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 조이스틱 입력값 적용
        if (joyControl[0]) { h = -1; v = 1; }
        if (joyControl[1]) { h = 0; v = 1; }
        if (joyControl[2]) { h = 1; v = 1; }
        if (joyControl[3]) { h = -1; v = 0; }
        if (joyControl[4]) { h = 0; v = 0; }
        if (joyControl[5]) { h = 1; v = 0; }
        if (joyControl[6]) { h = -1; v = -1; }
        if (joyControl[7]) { h = 0; v = -1; }
        if (joyControl[8]) { h = 1; v = -1; }

        // 화면 경계 충돌 시 이동 제한
        if ((isTouchRight && h == -1) || (isTouchLeft && h == 1) || !isControl)
            h = 0;
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1) || !isControl)
            v = 0;

        // 현재 위치에서 입력값에 따른 이동 계산
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        // 위치 업데이트
        transform.position = curPos + nextPos;

        // 애니메이션 업데이트
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            animator.SetInteger("Input", (int)h);
        }
    }
    void Boom()
    {
        if (!isButtonB || isBoomTime || boom == 0)
            return;

        boom--;
        isBoomTime = true;
        gameManager.UpdateBoomIcon(boom); // UI 업데이트

        // 폭탄 이펙트 활성화
        boomEffect.SetActive(true);
        Invoke("OffBomeEffect", 4f); // 4초 후 폭탄 이펙트 비활성화

        // 적과 적 총알 제거
        RemoveEnemies();
    }

    // 적 제거 처리 함수
    void RemoveEnemies()
    {
        GameObject[] enemiesL = objectManager.GetPool("EnemyL");
        GameObject[] enemiesM = objectManager.GetPool("EnemyM");
        GameObject[] enemiesS = objectManager.GetPool("EnemyS");

        // 각 적 오브젝트에 대해 제거 처리
        foreach (var enemy in enemiesL)
            if (enemy.activeSelf) enemy.GetComponent<Enemy>().OnHit(1000);

        foreach (var enemy in enemiesM)
            if (enemy.activeSelf) enemy.GetComponent<Enemy>().OnHit(1000);

        foreach (var enemy in enemiesS)
            if (enemy.activeSelf) enemy.GetComponent<Enemy>().OnHit(1000);

        // 적 총알 제거
        RemoveBullets("BulletEnemyA");
        RemoveBullets("BulletEnemyB");
    }

    // 적 총알 제거 처리 함수
    void RemoveBullets(string type)
    {
        GameObject[] bullets = objectManager.GetPool(type);
        foreach (var bullet in bullets)
            if (bullet.activeSelf) bullet.SetActive(false);
    }
    public void ButtonADown()
    {
        isButtonA = true;
    }
    public void ButtonAUp()
    {
        isButtonA = false;
    }
    public void ButtonBDown()
    {
        isButtonB = true;
    }

    void Fire()
    {


        // 현재 파워 레벨에 따른 총알 생성
        switch (Power)
        {
            case 1:
                CreateBullet("BulletPlayerA", Vector3.zero);
                break;
            case 2:
                CreateBullet("BulletPlayerA", Vector3.right * 0.1f);
                CreateBullet("BulletPlayerA", Vector3.left * 0.1f);
                break;
            default:
                CreateBullet("BulletPlayerA", Vector3.right * 0.35f);
                CreateBullet("BulletPlayerB", Vector3.zero);
                CreateBullet("BulletPlayerA", Vector3.left * 0.35f);
                break;
        }
        CurShotDelay = 0; // 발사 딜레이 초기화
    }

    // 총알 생성 함수
    void CreateBullet(string type, Vector3 offset)
    {
        GameObject bullet = objectManager.MakeObj(type);
        bullet.transform.position = transform.position + offset;
        bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }
    void Reload()
    {
        CurShotDelay += Time.deltaTime;
    }
    // 충돌 처리 함수
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 화면 경계와 충돌
        if (collision.gameObject.tag == "Border")
        {
            SetBorderTouch(collision.gameObject.name, true);
        }
        // 적 또는 적 총알과 충돌
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            HandleCollisionWithEnemy(collision);
        }
        // 아이템과 충돌
        else if (collision.gameObject.tag == "Item")
        {
            HandleItemPickup(collision.gameObject.GetComponent<Item>());
        }
    }

    // 화면 경계 충돌 상태 설정
    void SetBorderTouch(string borderName, bool state)
    {
        switch (borderName)
        {
            case "Top": isTouchTop = state; break;
            case "Bottom": isTouchBottom = state; break;
            case "Right": isTouchRight = state; break;
            case "Left": isTouchLeft = state; break;
        }
    }

    // 적 또는 적 총알과 충돌 처리
    void HandleCollisionWithEnemy(Collider2D collision)
    {
        if (isRespawnTime || isHit)
            return;

        isHit = true;
        life--;
        gameManager.UpdateLifeIcon(life);
        gameManager.CallExplosion(transform.position, "P");

        if (life == 0)
        {
            gameManager.GameOver();
        }
        else
        {
            gameManager.RespawnPlayer();
        }

        collision.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    // 아이템 획득 처리
    void HandleItemPickup(Item item)
    {
        switch (item.type)
        {
            case "Coin":
                score += 1000;
                break;
            case "Power":
                if (Power < maxPower)
                {
                    Power++;
                    AddFollower();
                }
                else
                {
                    score += 500;
                }
                break;
            case "Boom":
                if (boom < maxboom)
                {
                    boom++;
                    gameManager.UpdateBoomIcon(boom);
                }
                else
                {
                    score += 500;
                }
                break;
        }
        Destroy(item.gameObject);
    }
    // 보조 오브젝트 활성화
    void AddFollower()
    {
        if (Power >= 4 && Power - 4 < followers.Length)
        {
            followers[Power - 4].SetActive(true);
        }
    }

    // 폭탄 이펙트 비활성화
    void OffBomeEffect()
    {
        boomEffect.SetActive(false);
        isBoomTime = false;
    }

    // 화면 경계를 벗어날 때 호출
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            SetBorderTouch(collision.gameObject.name, false);
        }
    }
}
