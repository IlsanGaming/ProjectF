using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 적의 이름 (e.g., "S", "M", "L", "B"로 구분)
    public string enemyname;
    // 적을 처치했을 때 플레이어가 얻는 점수
    public int enemyscore;

    // 적의 이동 속도와 체력
    public float speed;
    public int health;

    // 적의 스프라이트 배열 (기본 및 히트 상태 등)
    public Sprite[] sprites;

    // 적의 스프라이트 렌더러와 애니메이터를 위한 변수
    SpriteRenderer spriteRenderer;
    Animator anim;

    // 적이 사용할 총알 오브젝트
    public GameObject bulletObjA;
    public GameObject bulletObjB;

    // 플레이어 오브젝트를 참조하기 위한 변수
    public GameObject player;

    // 오브젝트 및 게임 관리자를 참조하기 위한 변수
    public ObjectManager objectManager;
    public GameManager gameManager;

    // 아이템 드롭을 위한 오브젝트
    public GameObject itemCoin;
    public GameObject itemBoom;
    public GameObject itemPower;

    // 총알 발사 딜레이 변수
    public float MaxShotDelay;
    public float CurShotDelay;

    // 적의 패턴을 관리하기 위한 변수
    public int patternIndex; // 현재 패턴 인덱스
    public int curPatternCount; // 현재 패턴 반복 횟수
    public int[] maxPatternCount; // 각 패턴의 최대 반복 횟수

    // Awake 함수는 적 오브젝트가 활성화될 때 초기화 작업을 수행
    void Awake()
    {
        // 스프라이트 렌더러 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 적 이름이 "B"인 경우 애니메이터 컴포넌트 가져오기 (보스만 해당)
        if (enemyname == "B")
            anim = GetComponent<Animator>();
    }

    // 적 오브젝트가 활성화될 때 초기화 작업 수행
    void OnEnable()
    {
        // 적 이름에 따라 체력을 설정
        switch (enemyname)
        {
            case "L":
                health = 40; // 큰 적
                break;
            case "M":
                health = 10; // 중간 적
                break;
            case "S":
                health = 3;  // 작은 적
                break;
            case "B":
                health = 100; // 보스 적
                Invoke("Stop", 2); // 초기 2초 후 정지 상태로 전환
                break;
        }
    }

    // 보스 적을 멈추게 하는 함수
    void Stop()
    {
        if (!gameObject.activeSelf) return;

        // Rigidbody2D를 이용해 적의 속도를 0으로 설정
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        // 패턴 전환을 위한 Think 함수 호출
        Invoke("Think", 2);
    }

    // 다음 공격 패턴을 결정하는 함수
    void Think()
    {
        // 패턴 인덱스 순환 (0~3)
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0; // 현재 패턴 카운트를 초기화

        // 패턴에 따라 다른 공격 수행
        switch (patternIndex)
        {
            case 0:
                FireForward(); // 직선 발사
                break;
            case 1:
                FireShot(); // 샷건 형태 발사
                break;
            case 2:
                FireArc(); // 부채꼴 형태 발사
                break;
            case 3:
                FireAround(); // 원형 발사
                break;
        }
    }

    // 직선 발사 패턴
    void FireForward()
    {
        if (health <= 0) return;

        Debug.Log("앞으로 4발 발사.");
        // 적의 위치를 기준으로 좌우에 총알 생성
        GameObject bulletR = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletRR = objectManager.MakeObj("BulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 0.45f;
        GameObject bulletL = objectManager.MakeObj("BulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;
        GameObject bulletLL = objectManager.MakeObj("BulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 0.45f;

        // 생성된 총알에 힘을 가해 아래 방향으로 이동
        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

        rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);

        // 패턴 반복 관리
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireForward", 2);
        else
            Invoke("Think", 2);
    }

    // 샷건 패턴 (플레이어 방향으로 여러 발사체)
    void FireShot()
    {
        if (health <= 0) return;

        Debug.Log("플레이어 방향으로 샷건.");
        for (int index = 0; index < 5; index++)
        {
            // 총알 생성
            GameObject bullet = objectManager.MakeObj("BulletBossB");
            bullet.transform.position = transform.position;

            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
            // 플레이어 방향으로 약간 랜덤한 궤적 추가
            Vector3 dirVec = player.transform.position - transform.position;
            Vector3 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;

            rigidbody.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 3.5f);
        else
            Invoke("Think", 3);
    }

    // 부채꼴 패턴
    void FireArc()
    {
        if (health <= 0) return;

        Debug.Log("부채모양으로 발사");
        GameObject bullet = objectManager.MakeObj("BulletEnemyA");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        // 각도를 계산하여 부채꼴 형태로 총알 발사
        Vector3 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 10 * curPatternCount / maxPatternCount[patternIndex]), -1);
        rigidbody.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.15f);
        else
            Invoke("Think", 3);
    }

    // 원형 발사 패턴
    void FireAround()
    {
        if (health <= 0) return;

        Debug.Log("원 형태로 전체 공격");
        int roundNum = curPatternCount % 2 == 0 ? 50 : 40; // 발사체 수 변경
        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletBossB");
            bullet.transform.position = transform.position;

            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
            // 원형으로 발사체 방향 계산
            Vector3 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum), Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigidbody.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);
        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 3);
    }

    // 업데이트 함수 (적의 상태를 매 프레임 갱신)
    void Update()
    {
        if (enemyname == "B") return; // 보스는 별도 패턴

        Fire();   // 일반 발사 처리
        Reload(); // 발사 딜레이 갱신
    }

    // 발사 처리
    void Fire()
    {
        if (CurShotDelay < MaxShotDelay) return; // 딜레이가 아직 차지 않으면 리턴

        // 적의 이름에 따라 다른 발사 방식
        if (enemyname == "S") { /* 작은 적 로직 */ }
        else if (enemyname == "L") { /* 큰 적 로직 */ }
        CurShotDelay = 0;
    }

    // 발사 딜레이 갱신
    void Reload()
    {
        CurShotDelay += Time.deltaTime;
    }
    // 적이 맞았을 때 처리
    public void OnHit(int dmg)
    {
        // 적의 체력이 이미 0 이하이면 아무 작업도 수행하지 않음
        if (health <= 0)
            return;

        // 받은 데미지를 체력에서 차감
        health -= dmg;

        // 보스 적일 경우 애니메이터 트리거 호출
        if (enemyname == "B")
        {
            anim.SetTrigger("OnHit");
        }
        else
        {
            // 일반 적일 경우 맞은 상태의 스프라이트로 변경
            spriteRenderer.sprite = sprites[1];
            // 0.1초 후 원래 스프라이트로 복구
            Invoke("ReturnSprite", 0.1f);
        }

        // 체력이 0 이하로 떨어진 경우 처리
        if (health <= 0)
        {
            // 플레이어 점수 증가
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyscore;

            // 적이 아이템을 떨어뜨릴 확률 계산 (보스는 항상 아이템을 드롭하지 않음)
            int ran = enemyname == "B" ? 0 : Random.Range(0, 10);
            if (ran < 3)
            {
                // 30% 확률로 아무 아이템도 드롭하지 않음
                Debug.Log("Not Item");
            }
            else if (ran < 6)
            {
                // 30% 확률로 코인 아이템 드롭
                GameObject itemCoin = objectManager.MakeObj("ItemCoin");
                itemCoin.transform.position = transform.position;
            }
            else if (ran < 8)
            {
                // 20% 확률로 파워업 아이템 드롭
                GameObject itemPower = objectManager.MakeObj("ItemPower");
                itemPower.transform.position = transform.position;
            }
            else if (ran < 10)
            {
                // 20% 확률로 폭탄 아이템 드롭
                GameObject itemBoom = objectManager.MakeObj("ItemBoom");
                itemBoom.transform.position = transform.position;
            }

            // 적 비활성화 및 초기화
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;

            // 폭발 이펙트를 호출
            gameManager.CallExplosion(transform.position, enemyname);

            // 보스 적을 처치한 경우 스테이지 종료 처리
            if (enemyname == "B")
            {
                gameManager.StageEnd();
            }
        }
    }

    // 적 스프라이트를 원래 상태로 복구
    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    // 충돌 처리 함수
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 적이 특정 태그를 가진 오브젝트와 충돌했을 때 처리
        if (collision.gameObject.tag == "Borderbullet" && enemyname != "B")
        {
            // 적이 화면 경계를 벗어난 경우 비활성화
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            // 플레이어의 총알과 충돌했을 경우
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                // 데미지를 적용
                OnHit(bullet.dmg);
                Debug.Log("hit");

                // 체력이 0 이하일 경우 적을 비활성화
                if (health <= 0)
                {
                    gameObject.SetActive(false);
                    transform.rotation = Quaternion.identity;
                }
            }
        }
    }
}
