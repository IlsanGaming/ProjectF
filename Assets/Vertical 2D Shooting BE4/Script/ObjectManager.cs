using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // ������ ����: ���� ������Ʈ ������ ����� ���� �����յ��� ����
    public GameObject enemyBPrefab; // ���� ��
    public GameObject enemyLPrefab; // ū ��
    public GameObject enemyMPrefab; // �߰� ��
    public GameObject enemySPrefab; // ���� ��
    public GameObject itemCoinPrefab; // ���� ������
    public GameObject itemPowerPrefab; // �Ŀ� �� ������
    public GameObject itemBoomPrefab; // ��ź ������
    public GameObject bulletPlayerAPrefab; // �÷��̾��� �⺻ �Ѿ�
    public GameObject bulletPlayerBPrefab; // �÷��̾��� ��ȭ �Ѿ�
    public GameObject bulletEnemyAPrefab; // ���� �⺻ �Ѿ�
    public GameObject bulletEnemyBPrefab; // ���� ��ȭ �Ѿ�
    public GameObject bulletFollowerPrefab; // �÷��̾��� ���� �Ѿ�
    public GameObject bulletBossAPrefab; // ������ �⺻ �Ѿ�
    public GameObject bulletBossBPrefab; // ������ ��ȭ �Ѿ�
    public GameObject explosionPrefab; // ���� ����Ʈ

    // ������Ʈ Ǯ �迭: ������ �����տ� ���� ������ ������Ʈ���� ����
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

    // ���� ��� ���� ������Ʈ Ǯ
    GameObject[] targetPool;

    // Awake: ���� ������Ʈ Ȱ��ȭ �� �ʱ�ȭ �۾� ����
    void Awake()
    {
        // ������ ������Ʈ Ǯ �迭 ũ�� ����
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

        // ������Ʈ ���� �Լ� ȣ��
        Generate();
    }

    // Generate: ��� ������Ʈ Ǯ�� ���� ������Ʈ�� �̸� �����Ͽ� Ǯ��
    void Generate()
    {
        // #1 �� ������Ʈ ����
        for (int index = 0; index < enemyB.Length; index++)
        {
            enemyB[index] = Instantiate(enemyBPrefab); // ���� �� ����
            enemyB[index].SetActive(false); // ��Ȱ��ȭ
        }
        for (int index = 0; index < enemyL.Length; index++)
        {
            enemyL[index] = Instantiate(enemyLPrefab); // ū �� ����
            enemyL[index].SetActive(false);
        }
        for (int index = 0; index < enemyM.Length; index++)
        {
            enemyM[index] = Instantiate(enemyMPrefab); // �߰� �� ����
            enemyM[index].SetActive(false);
        }
        for (int index = 0; index < enemyS.Length; index++)
        {
            enemyS[index] = Instantiate(enemySPrefab); // ���� �� ����
            enemyS[index].SetActive(false);
        }

        // #2 ������ ������Ʈ ����
        for (int index = 0; index < itemCoin.Length; index++)
        {
            itemCoin[index] = Instantiate(itemCoinPrefab); // ���� ������ ����
            itemCoin[index].SetActive(false);
        }
        for (int index = 0; index < itemPower.Length; index++)
        {
            itemPower[index] = Instantiate(itemPowerPrefab); // �Ŀ� �� ������ ����
            itemPower[index].SetActive(false);
        }
        for (int index = 0; index < itemBoom.Length; index++)
        {
            itemBoom[index] = Instantiate(itemBoomPrefab); // ��ź ������ ����
            itemBoom[index].SetActive(false);
        }

        // #3 �Ѿ� ������Ʈ ����
        for (int index = 0; index < bulletPlayerA.Length; index++)
        {
            bulletPlayerA[index] = Instantiate(bulletPlayerAPrefab); // �÷��̾� �⺻ �Ѿ�
            bulletPlayerA[index].SetActive(false);
        }
        for (int index = 0; index < bulletPlayerB.Length; index++)
        {
            bulletPlayerB[index] = Instantiate(bulletPlayerBPrefab); // �÷��̾� ��ȭ �Ѿ�
            bulletPlayerB[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyA.Length; index++)
        {
            bulletEnemyA[index] = Instantiate(bulletEnemyAPrefab); // �� �⺻ �Ѿ�
            bulletEnemyA[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyB.Length; index++)
        {
            bulletEnemyB[index] = Instantiate(bulletEnemyBPrefab); // �� ��ȭ �Ѿ�
            bulletEnemyB[index].SetActive(false);
        }
        for (int index = 0; index < bulletFollower.Length; index++)
        {
            bulletFollower[index] = Instantiate(bulletFollowerPrefab); // �÷��̾� ���� �Ѿ�
            bulletFollower[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossA.Length; index++)
        {
            bulletBossA[index] = Instantiate(bulletBossAPrefab); // ���� �⺻ �Ѿ�
            bulletBossA[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossB.Length; index++)
        {
            bulletBossB[index] = Instantiate(bulletBossBPrefab); // ���� ��ȭ �Ѿ�
            bulletBossB[index].SetActive(false);
        }

        // #4 ���� ����Ʈ ����
        for (int index = 0; index < explosion.Length; index++)
        {
            explosion[index] = Instantiate(explosionPrefab);
            explosion[index].SetActive(false);
        }
    }

    // MakeObj: ��û�� Ÿ���� ������Ʈ�� Ȱ��ȭ�Ͽ� ��ȯ
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
                Debug.LogError($"'{type}'�� �ش��ϴ� ������Ʈ Ǯ�� ã�� �� �����ϴ�.");
                return null;
        }

        // ��Ȱ��ȭ�� ������Ʈ�� ã�� Ȱ��ȭ �� ��ȯ
        for (int index = 0; index < targetPool.Length; index++)
        {
            if (targetPool[index] != null && !targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        Debug.LogWarning($"'{type}' Ǯ���� ����� �� �ִ� ������Ʈ�� �����ϴ�.");
        return null;
    }

    // GetPool: Ư�� Ÿ���� ������Ʈ Ǯ �迭�� ��ȯ
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
