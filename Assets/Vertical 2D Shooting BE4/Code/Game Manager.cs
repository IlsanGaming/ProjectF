using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.ComponentModel;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float nextSpawnDealy;
    public float curSpawnDelay;

    public float gameTime;
    public float maxGameTime;

    public int difficulty;
    public int maxDifficulty;

    public string[] enemyObjs;
    public Transform[] spawnPoints;
    public Image[] skill5Image;

    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 }; // �������� �ʿ��� ����ġ �迭
    public int level; // ���� �÷��̾� ����

    public GameObject player;
    public ObjectManager objectManager;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    void Awake()
    {
        instance = this;
        spawnList = new List<Spawn>();
        enemyObjs = new string[] { "enemyL", "enemyM", "enemyS" };
        ReadSpawnFile();
    }
    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        if(curSpawnDelay > nextSpawnDealy &&!spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }
        SetGameDifference();
    }
    void SetGameDifference()
    {
        gameTime += Time.deltaTime;
        difficulty = Mathf.FloorToInt(gameTime / (maxGameTime / 10f));
        if(difficulty > maxDifficulty)
        {
            difficulty = maxDifficulty;
        }
    }
    void ReadSpawnFile()
    {
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        TextAsset textFile=Resources.Load("Stage0") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line=stringReader.ReadLine();
            Debug.Log(line);
            if (line == null)
                break;
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        stringReader.Close();
        nextSpawnDealy = spawnList[0].delay;
    }
    void SpawnEnemy()
    {
        int enemyIndex = 0;
        switch(spawnList[spawnIndex].type)
        {
            case "L":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "S":
                enemyIndex = 2;
                break;
        }
        int enemyPoint = spawnList[spawnIndex].point;
        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.gameManager = this;
        enemyLogic.objectManager = objectManager;
        if (enemyPoint == 5||enemyPoint ==6)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed ,-0.5f);
        }
        else if(enemyPoint ==7||enemyPoint ==8)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -0.5f);
        }
        else
        {
            rigid.velocity = new Vector2(0,enemyLogic.speed*(-1));
        }

        spawnIndex++;
        if(spawnIndex==spawnList.Count)
        {
            spawnEnd=true;
            return;
        }
        nextSpawnDealy = spawnList[spawnIndex].delay;
    }
    // ����ġ�� ȹ���ϰ� ������ ó��
    public void GetExp()
    {
        /*if (!isLive) // ������ ��Ȱ��ȭ ���¸� �������� ����
            return;*/

        exp++; // ����ġ ����

        // ����ġ�� ���� ������ �䱸ġ�� �����ϸ� ������ ó��
        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++; // ���� ����
            exp = 0; // ����ġ �ʱ�ȭ
            //uiLevelUp.Show(); // ������ UI ǥ��
        }
    }
}