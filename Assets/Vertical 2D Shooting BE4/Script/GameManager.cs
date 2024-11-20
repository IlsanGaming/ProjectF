using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro�� ����ϱ� ���� ���ӽ����̽�
using UnityEngine.UI; // UI ��Ҹ� �ٷ�� ���� ���ӽ����̽�
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� ���ӽ����̽�
using System.IO; // ���� �б⸦ ���� ���ӽ����̽�

public class GameManager : MonoBehaviour
{
    // ���� ���������� ��Ÿ���� ����
    public int stage;

    // �������� ����, Ŭ����, ���̵� �ִϸ��̼��� ���� �ִϸ�����
    public Animator stageAnim;
    public Animator clearAnim;
    public Animator fadeAnim;

    // �÷��̾� �ʱ� ��ġ�� �����ϱ� ���� Transform
    public Transform playerPos;

    // �� ������ �����ϴ� �迭 (��: ���� ��, �߰� ��, ū ��, ����)
    public string[] enemyObjs;

    // ���� ������ ��ġ�� �����ϴ� �迭
    public Transform[] spawnPoints;

    // ���� �� ���������� ���� �ð��� ���� ���� �ð��� ����
    public float nextSpawnDelay;
    public float curSpawnDelay;

    // �÷��̾� ���� ������Ʈ
    public GameObject player;

    // ���� �ؽ�Ʈ ǥ�ÿ� UI �̹����� (������, ��ź ��)
    public TextMeshProUGUI scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;

    // ���� ���� �� ǥ�õǴ� UI �г�
    public GameObject gameOverSet;

    // ������Ʈ �����ڸ� �����ϱ� ���� ����
    public ObjectManager objectManager;

    // �� ���� �����͸� �����ϴ� ����Ʈ
    public List<Spawn> spawnList;

    // ���� ���� �ε����� ���� ���� ����
    public int spawnIndex;
    public bool spawnEnd;

    // Awake�� ���� ������Ʈ�� Ȱ��ȭ�� �� �ʱ�ȭ �۾��� ����
    void Awake()
    {
        // ���� �����͸� ������ ����Ʈ �ʱ�ȭ
        spawnList = new List<Spawn>();

        // �� ���� �迭 �ʱ�ȭ (���� ��, �߰� ��, ū ��, ����)
        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL", "EnemyB" };

        // �������� ���� ó��
        StageStart();
    }

    // �������� ���� �� ȣ��Ǵ� �Լ�
    public void StageStart()
    {
        // �������� ���� �ִϸ��̼� ���
        stageAnim.SetTrigger("On");

        // �������� ���� �� Ŭ���� �޽��� ����
        stageAnim.GetComponent<TextMeshProUGUI>().text = "Stage " + stage + "\nStart";
        clearAnim.GetComponent<TextMeshProUGUI>().text = "Stage " + stage + "\nClear!";

        // ���� ���� �б�
        ReadSpawnFile();

        // ���̵� �� �ִϸ��̼� ���
        fadeAnim.SetTrigger("In");
    }

    // �������� ���� �� ȣ��Ǵ� �Լ�
    public void StageEnd()
    {
        // �������� Ŭ���� �ִϸ��̼� ���
        clearAnim.SetTrigger("On");

        // ���̵� �ƿ� �ִϸ��̼� ���
        fadeAnim.SetTrigger("Out");

        // �÷��̾ �ʱ� ��ġ�� �̵�
        player.transform.position = playerPos.position;

        // ���� ���������� �̵�
        stage++;
        if (stage > 2) // ������ ���������� �Ѿ��� ��� ���� ���� ó��
            Invoke("GameOver", 6);
        else
            Invoke("StageStart", 5); // ���� �������� ����
    }

    // �� ���� �����͸� �о���� �Լ�
    void ReadSpawnFile()
    {
        // ���� ������ �ʱ�ȭ
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // ���� ���������� �ش��ϴ� �ؽ�Ʈ ���� �б�
        TextAsset textFile = Resources.Load("Stage " + stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        // �ؽ�Ʈ ������ �� ���� �о�� ���� �����ͷ� ��ȯ
        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            Debug.Log(line); // ���� �����͸� �α׿� ���

            if (line == null)
                break;

            // ���� ������ ���� �� ����
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]); // ���� ������
            spawnData.type = line.Split(',')[1]; // ���� Ÿ��
            spawnData.point = int.Parse(line.Split(',')[2]); // ���� ����Ʈ �ε���
            spawnList.Add(spawnData);
        }

        // �ؽ�Ʈ ���� �ݱ�
        stringReader.Close();

        // ù ��° ���� ������ ����
        nextSpawnDelay = spawnList[0].delay;
    }

    // �� ������ ȣ��Ǵ� �Լ�
    void Update()
    {
        // ���� ���� �����̸� ����
        curSpawnDelay += Time.deltaTime;

        // ���� �����̰� �ʰ��Ǿ��� ������ ������ �ʾ��� ���
        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy(); // �� ����
            curSpawnDelay = 0; // ������ �ʱ�ȭ
        }

        // ���� UI ������Ʈ
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score); // õ ���� �޸� �߰�
    }

    // ���� �����ϴ� �Լ�
    void SpawnEnemy()
    {
        int enemyIndex = 0;
        // ���� �����Ϳ� ���� ���� �ε��� ����
        switch (spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0; // ���� ��
                break;
            case "M":
                enemyIndex = 1; // �߰� ��
                break;
            case "L":
                enemyIndex = 2; // ū ��
                break;
            case "B":
                enemyIndex = 3; // ����
                break;
        }

        // ���� ����Ʈ ����
        int enemyPoint = spawnList[spawnIndex].point;
        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;

        // ���� ������ �� �ӵ� ����
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.gameManager = this;
        enemyLogic.objectManager = objectManager;

        if (enemyPoint == 5 || enemyPoint == 6) // �����ʿ��� ����
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if (enemyPoint == 7 || enemyPoint == 8) // ���ʿ��� ����
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else // ���鿡�� ����
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }

        // ���� ���� �����ͷ� ��ȯ
        spawnIndex++;
        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true; // ���� ����
            return;
        }

        // ���� ���� ������ ����
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }

    // �÷��̾ �������ϴ� �Լ�
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerEXE", 2f); // 2�� �� ������ ����
    }
    public void RespawnPlayerEXE()
    {
        player.transform.position = Vector3.down * 3.5f; // �ʱ� ��ġ�� �̵�
        player.SetActive(true); // Ȱ��ȭ
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isHit = false; // ���� ���� ����
    }

    // ���� ȿ���� ȣ���ϴ� �Լ�
    public void CallExplosion(Vector3 pos, string type)
    {
        GameObject explosion = objectManager.MakeObj("Explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();
        explosion.transform.position = pos;
        explosionLogic.StartExplosion(type); // ���� �ִϸ��̼� ����
    }

    // ������ ������ UI�� ������Ʈ�ϴ� �Լ�
    public void UpdateLifeIcon(int life)
    {
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0); // �⺻ ��Ȱ��ȭ
        }
        for (int index = 0; index < life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1); // Ȱ��ȭ
        }
    }

    // ��ź ������ UI�� ������Ʈ�ϴ� �Լ�
    public void UpdateBoomIcon(int boom)
    {
        for (int index = 0; index < 3; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 0); // �⺻ ��Ȱ��ȭ
        }
        for (int index = 0; index < boom; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 1); // Ȱ��ȭ
        }
    }

    // ���� ���� ó���� ���� �Լ�
    public void GameOver()
    {
        gameOverSet.SetActive(true); // ���� ���� UI Ȱ��ȭ
    }

    // ���� ������� ���� �Լ�
    public void GameRetry()
    {
        SceneManager.LoadScene(0); // ù ��° �� �ε�
    }
}
