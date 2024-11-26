using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class GameManager : MonoBehaviour
{
    public float nextSpawnDealy;
    public float curSpawnDelay;

    public float gameTime;
    public float maxGameTime;

    public int difference;
    public int maxdifference;

    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public Image[] skill5Image;

    public GameObject player;
    public ObjectManager objectManager;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    void Awake()
    {
        spawnList = new List<Spawn>();
        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL"};
        ReadSpawnFile();
        UpdateSkill5Icon(0);
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
        difference = Mathf.FloorToInt(gameTime / (maxGameTime / 10f));
        if(difference > maxdifference)
        {
            difference = maxdifference;
        }
    }
    public void UpdateSkill5Icon(int skill5stack)
    {
        for(int index=0;index<3;index++)
        {
            skill5Image[index].color= new Color(1,1,1, 0);
        }
        for (int index = 0; index < skill5stack; index++)
        {
            skill5Image[index].color = new Color(1, 1, 1, 1);
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
            case "S":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
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
}
