using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // 프리팹 변수: 게임 오브젝트 생성에 사용할 원본 프리팹들을 선언
    public GameObject enemyBPrefab; // 보스 적
    public GameObject enemyLPrefab; // 큰 적
    public GameObject enemyELPrefab; // 엘리트큰 적
    public GameObject enemyMPrefab; // 중간 적
    public GameObject enemyEMPrefab; // 중간 적
    public GameObject enemySPrefab; // 작은 적
    public GameObject enemyESPrefab; // 작은 적
    public GameObject itemSkillPrefab; // 코인 아이템
    public GameObject itemExpPrefab; // 파워 업 아이템
    public GameObject itemHealthPrefab; // 폭탄 아이템
    public GameObject bulletPlayerAPrefab; // 플레이어의 기본 총알
    public GameObject bulletPlayerBPrefab; // 플레이어의 강화 총알
    public GameObject bulletEnemyAPrefab; // 적의 기본 총알
    public GameObject bulletEnemyBPrefab; // 적의 강화 총알
    public GameObject bulletFollowerPrefab; // 플레이어의 보조 총알
    public GameObject bulletChasePrefab; // 플레이어의 보조 총알
    public GameObject bulletBossAPrefab; // 보스의 기본 총알
    public GameObject bulletBossBPrefab; // 보스의 강화 총알
    public GameObject explosionPrefab; // 폭발 이펙트

    // 오브젝트 풀 배열: 각각의 프리팹에 대해 생성된 오브젝트들을 보관
    GameObject[] enemyB;
    GameObject[] enemyEL;
    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyEM;
    GameObject[] enemyS;
    GameObject[] enemyES;

    GameObject[] itemSkill;
    GameObject[] itemExp;
    GameObject[] itemHealth;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletFollower;
    GameObject[] bulletChase;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;
    GameObject[] explosion;

    // 현재 사용 중인 오브젝트 풀
    GameObject[] targetPool;

    // Awake: 게임 오브젝트 활성화 시 초기화 작업 수행
    void Awake()
    {
        // 각각의 오브젝트 풀 배열 크기 설정
        enemyB = new GameObject[5];
        enemyL = new GameObject[30];
        enemyEL = new GameObject[30];
        enemyM = new GameObject[30];
        enemyEM = new GameObject[30];
        enemyS = new GameObject[50];
        enemyES = new GameObject[50];

        itemSkill = new GameObject[30];
        itemExp = new GameObject[30];
        itemHealth = new GameObject[30];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletEnemyA = new GameObject[100];
        bulletEnemyB = new GameObject[500];
        bulletFollower = new GameObject[100];
        bulletChase = new GameObject[100];
        bulletBossA = new GameObject[500];
        bulletBossB = new GameObject[500];

        explosion = new GameObject[50];

        // 오브젝트 생성 함수 호출
        Generate();
    }

    // Generate: 모든 오브젝트 풀에 대해 오브젝트를 미리 생성하여 풀링
    void Generate()
    {
        // #1 적 오브젝트 생성
        for (int index = 0; index < enemyB.Length; index++)
        {
            enemyB[index] = Instantiate(enemyBPrefab); // 보스 적 생성
            enemyB[index].SetActive(false); // 비활성화
        }
        for (int index = 0; index < enemyL.Length; index++)
        {
            enemyL[index] = Instantiate(enemyLPrefab); // 큰 적 생성
            enemyL[index].SetActive(false);
        }
        for (int index = 0; index < enemyEL.Length; index++)
        {
            enemyEL[index] = Instantiate(enemyELPrefab); // 큰 적 생성
            enemyEL[index].SetActive(false);
        }
        for (int index = 0; index < enemyM.Length; index++)
        {
            enemyM[index] = Instantiate(enemyMPrefab); // 중간 적 생성
            enemyM[index].SetActive(false);
        }
        for (int index = 0; index < enemyEM.Length; index++)
        {
            enemyEM[index] = Instantiate(enemyEMPrefab); // 중간 적 생성
            enemyEM[index].SetActive(false);
        }
        for (int index = 0; index < enemyS.Length; index++)
        {
            enemyS[index] = Instantiate(enemySPrefab); // 작은 적 생성
            enemyS[index].SetActive(false);
        }
        for (int index = 0; index < enemyES.Length; index++)
        {
            enemyES[index] = Instantiate(enemyESPrefab); // 작은 적 생성
            enemyES[index].SetActive(false);
        }
        // #2 아이템 오브젝트 생성
        for (int index = 0; index < itemSkill.Length; index++)
        {
            itemSkill[index] = Instantiate(itemSkillPrefab); // 스킬 아이템 생성
            itemSkill[index].SetActive(false);
        }
        for (int index = 0; index < itemExp.Length; index++)
        {
            itemExp[index] = Instantiate(itemExpPrefab); // 경험치 아이템 생성
            itemExp[index].SetActive(false);
        }
        for (int index = 0; index < itemHealth.Length; index++)
        {
            itemHealth[index] = Instantiate(itemHealthPrefab); // 체력 아이템 생성
            itemHealth[index].SetActive(false);
        }

        // #3 총알 오브젝트 생성
        for (int index = 0; index < bulletPlayerA.Length; index++)
        {
            bulletPlayerA[index] = Instantiate(bulletPlayerAPrefab); // 플레이어 기본 총알
            bulletPlayerA[index].SetActive(false);
        }
        for (int index = 0; index < bulletPlayerB.Length; index++)
        {
            bulletPlayerB[index] = Instantiate(bulletPlayerBPrefab); // 플레이어 강화 총알
            bulletPlayerB[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyA.Length; index++)
        {
            bulletEnemyA[index] = Instantiate(bulletEnemyAPrefab); // 적 기본 총알
            bulletEnemyA[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyB.Length; index++)
        {
            bulletEnemyB[index] = Instantiate(bulletEnemyBPrefab); // 적 강화 총알
            bulletEnemyB[index].SetActive(false);
        }
        for (int index = 0; index < bulletFollower.Length; index++)
        {
            bulletFollower[index] = Instantiate(bulletFollowerPrefab); // 플레이어 보조 총알
            bulletFollower[index].SetActive(false);
        }
        for (int index = 0; index < bulletChase.Length; index++)
        {
            bulletChase[index] = Instantiate(bulletChasePrefab); // 플레이어 보조 총알
            bulletChase[index].SetActive(false);
        }
        
        for (int index = 0; index < bulletBossA.Length; index++)
        {
            bulletBossA[index] = Instantiate(bulletBossAPrefab); // 보스 기본 총알
            bulletBossA[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossB.Length; index++)
        {
            bulletBossB[index] = Instantiate(bulletBossBPrefab); // 보스 강화 총알
            bulletBossB[index].SetActive(false);
        }
        
        // #4 폭발 이펙트 생성
        for (int index = 0; index < explosion.Length; index++)
        {
            explosion[index] = Instantiate(explosionPrefab);
            explosion[index].SetActive(false);
        }
    }

    // MakeObj: 요청된 타입의 오브젝트를 활성화하여 반환
    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "enemyB":
                targetPool = enemyB;
                break;
            case "enemyL":
                targetPool = enemyL;
                break;
            case "enemyEL":
                targetPool = enemyEL;
                break;
            case "enemyM":
                targetPool = enemyM;
                break;
            case "enemyEM":
                targetPool = enemyEM;
                break;
            case "enemyS":
                targetPool = enemyS;
                break;
            case "enemyES":
                targetPool = enemyES;
                break;
            case "itemSkill":
                targetPool = itemSkill;
                break;
            case "itemExp":
                targetPool = itemExp;
                break;
            case "itemHealth":
                targetPool = itemHealth;
                break;
            case "bulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "bulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "bulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "bulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "bulletFollower":
                targetPool = bulletFollower;
                break;
            case "bulletChase":
                targetPool = bulletChase;
                break;
             case "bulletBossA":
                targetPool = bulletBossA;
                break;
            case "bulletBossB":
                targetPool = bulletBossB;
                break;    
            case "explosion":
                targetPool = explosion;
                break;
            default:
                Debug.LogError($"'{type}'에 해당하는 오브젝트 풀을 찾을 수 없습니다.");
                return null;
        }
        // 비활성화된 오브젝트를 찾아 활성화 후 반환
        for (int index = 0; index < targetPool.Length; index++)
        {
            if (targetPool[index] != null && !targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        Debug.LogWarning($"'{type}' 풀에서 사용할 수 있는 오브젝트가 없습니다.");
        return null;
    }

    // GetPool: 특정 타입의 오브젝트 풀 배열을 반환
    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "enemyB":
                targetPool = enemyB;
                break;
            case "enemyL":
                targetPool = enemyL;
                break;
            case "enemyEL":
                targetPool = enemyEL;
                break;
            case "enemyM":
                targetPool = enemyM;
                break;
            case "enemyEM":
                targetPool = enemyEM;
                break;
            case "enemyS":
                targetPool = enemyS;
                break;
            case "enemyES":
                targetPool = enemyES;
                break;
            case "itemSkill":
                targetPool = itemSkill;
                break;
            case "itemExp":
                targetPool = itemExp;
                break;
            case "itemHealth":
                targetPool = itemHealth;
                break;
            case "bulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "bulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "bulletFollower":
                targetPool = bulletFollower;
                break;
            case "bulletChase":
                targetPool = bulletChase;
                break;
            case "bulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "bulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "bulletBossA":
                targetPool = bulletBossA;
                break;
            case "bulletBossB":
                targetPool = bulletBossB;
                break;
            case "explosion":
                targetPool = explosion;
                break;
        }
        return targetPool;
    }
}
