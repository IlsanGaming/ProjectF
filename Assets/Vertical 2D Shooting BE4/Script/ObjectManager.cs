using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // 프리팹 변수: 게임 오브젝트 생성에 사용할 원본 프리팹들을 선언
    public GameObject enemyBPrefab; // 보스 적
    public GameObject enemyLPrefab; // 큰 적
    public GameObject enemyMPrefab; // 중간 적
    public GameObject enemySPrefab; // 작은 적
    public GameObject itemCoinPrefab; // 코인 아이템
    public GameObject itemPowerPrefab; // 파워 업 아이템
    public GameObject itemBoomPrefab; // 폭탄 아이템
    public GameObject bulletPlayerAPrefab; // 플레이어의 기본 총알
    public GameObject bulletPlayerBPrefab; // 플레이어의 강화 총알
    public GameObject bulletEnemyAPrefab; // 적의 기본 총알
    public GameObject bulletEnemyBPrefab; // 적의 강화 총알
    public GameObject bulletFollowerPrefab; // 플레이어의 보조 총알
    public GameObject bulletBossAPrefab; // 보스의 기본 총알
    public GameObject bulletBossBPrefab; // 보스의 강화 총알
    public GameObject explosionPrefab; // 폭발 이펙트

    // 오브젝트 풀 배열: 각각의 프리팹에 대해 생성된 오브젝트들을 보관
    GameObject[] enemyB;
    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;

    GameObject[] itemCoin;
    GameObject[] itemPower;
    GameObject[] itemBoom;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletFollower;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;
    GameObject[] explosion;

    // 현재 사용 중인 오브젝트 풀
    GameObject[] targetPool;

    // Awake: 게임 오브젝트 활성화 시 초기화 작업 수행
    void Awake()
    {
        // 각각의 오브젝트 풀 배열 크기 설정
        enemyB = new GameObject[10];
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[20];

        itemCoin = new GameObject[20];
        itemPower = new GameObject[10];
        itemBoom = new GameObject[10];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletEnemyA = new GameObject[100];
        bulletEnemyB = new GameObject[100];
        bulletFollower = new GameObject[100];
        bulletBossA = new GameObject[50];
        bulletBossB = new GameObject[1000];

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
        for (int index = 0; index < enemyM.Length; index++)
        {
            enemyM[index] = Instantiate(enemyMPrefab); // 중간 적 생성
            enemyM[index].SetActive(false);
        }
        for (int index = 0; index < enemyS.Length; index++)
        {
            enemyS[index] = Instantiate(enemySPrefab); // 작은 적 생성
            enemyS[index].SetActive(false);
        }

        // #2 아이템 오브젝트 생성
        for (int index = 0; index < itemCoin.Length; index++)
        {
            itemCoin[index] = Instantiate(itemCoinPrefab); // 코인 아이템 생성
            itemCoin[index].SetActive(false);
        }
        for (int index = 0; index < itemPower.Length; index++)
        {
            itemPower[index] = Instantiate(itemPowerPrefab); // 파워 업 아이템 생성
            itemPower[index].SetActive(false);
        }
        for (int index = 0; index < itemBoom.Length; index++)
        {
            itemBoom[index] = Instantiate(itemBoomPrefab); // 폭탄 아이템 생성
            itemBoom[index].SetActive(false);
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
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletFollower":
                targetPool = bulletFollower;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;
            case "Explosion":
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
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
        }

        return targetPool;
    }
}
