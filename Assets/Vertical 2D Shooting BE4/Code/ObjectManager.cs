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
    public GameObject itemSkillPrefab; // ���� ������
    public GameObject itemExpPrefab; // �Ŀ� �� ������
    public GameObject itemHealthPrefab; // ��ź ������
    public GameObject bulletPlayerAPrefab; // �÷��̾��� �⺻ �Ѿ�
    public GameObject bulletPlayerBPrefab; // �÷��̾��� ��ȭ �Ѿ�
    public GameObject bulletEnemyAPrefab; // ���� �⺻ �Ѿ�
    public GameObject bulletEnemyBPrefab; // ���� ��ȭ �Ѿ�
    public GameObject bulletFollowerPrefab; // �÷��̾��� ���� �Ѿ�
    public GameObject bulletChasePrefab; // �÷��̾��� ���� �Ѿ�
    public GameObject bulletBossAPrefab; // ������ �⺻ �Ѿ�
    public GameObject bulletBossBPrefab; // ������ ��ȭ �Ѿ�
    public GameObject explosionPrefab; // ���� ����Ʈ

    // ������Ʈ Ǯ �迭: ������ �����տ� ���� ������ ������Ʈ���� ����
    GameObject[] enemyB;
    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;

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

    // ���� ��� ���� ������Ʈ Ǯ
    GameObject[] targetPool;

    // Awake: ���� ������Ʈ Ȱ��ȭ �� �ʱ�ȭ �۾� ����
    void Awake()
    {
        // ������ ������Ʈ Ǯ �迭 ũ�� ����
        enemyB = new GameObject[5];
        enemyL = new GameObject[30];
        enemyM = new GameObject[30];
        enemyS = new GameObject[50];

        itemSkill = new GameObject[30];
        itemExp = new GameObject[30];
        itemHealth = new GameObject[30];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletEnemyA = new GameObject[100];
        bulletEnemyB = new GameObject[100];
        bulletFollower = new GameObject[100];
        bulletChase = new GameObject[100];
        bulletBossA = new GameObject[50];
        bulletBossB = new GameObject[500];

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
        for (int index = 0; index < itemSkill.Length; index++)
        {
            itemSkill[index] = Instantiate(itemSkillPrefab); // ��ų ������ ����
            itemSkill[index].SetActive(false);
        }
        for (int index = 0; index < itemExp.Length; index++)
        {
            itemExp[index] = Instantiate(itemExpPrefab); // ����ġ ������ ����
            itemExp[index].SetActive(false);
        }
        for (int index = 0; index < itemHealth.Length; index++)
        {
            itemHealth[index] = Instantiate(itemHealthPrefab); // ü�� ������ ����
            itemHealth[index].SetActive(false);
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
        for (int index = 0; index < bulletChase.Length; index++)
        {
            bulletChase[index] = Instantiate(bulletChasePrefab); // �÷��̾� ���� �Ѿ�
            bulletChase[index].SetActive(false);
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
            case "enemyB":
                targetPool = enemyB;
                break;
            case "enemyL":
                targetPool = enemyL;
                break;
            case "enemyM":
                targetPool = enemyM;
                break;
            case "enemyS":
                targetPool = enemyS;
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
                Debug.LogError($"'{type}'�� �ش��ϴ� ������Ʈ Ǯ�� ã�� �� �����ϴ�.");
                return null;
        }

        // ��� ������ ��Ȱ��ȭ�� ������Ʈ ã��
        foreach (var obj in targetPool)
        {
            if (obj != null && !obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // ���� ���� �õ�
        Debug.LogWarning($"'{type}' ������Ʈ Ǯ�� ��� �ֽ��ϴ�. ���� ������ �õ��մϴ�.");
        GameObject prefab = null;

        if (prefab == null)
        {
            Debug.LogError($"'{type}' �������� �������� �ʾҽ��ϴ�.");
            return null;
        }

        GameObject newObj = Instantiate(prefab);
        if (newObj != null)
        {
            newObj.SetActive(false);
            return newObj;
        }
        else
        {
            Debug.LogError($"'{type}' ������Ʈ ������ �����߽��ϴ�.");
            return null;
        }
    }

    // GetPool: Ư�� Ÿ���� ������Ʈ Ǯ �迭�� ��ȯ
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
            case "enemyM":
                targetPool = enemyM;
                break;
            case "enemyS":
                targetPool = enemyS;
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
