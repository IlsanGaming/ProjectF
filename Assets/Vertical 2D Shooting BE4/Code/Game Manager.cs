using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public float maxSpawnDelay;
    public float curSpawnDelay;

    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public Image[] skill5Image;

    public GameObject player;
    public ObjectManager objectManager;


    void Awake()
    {
        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL"};
        UpdateSkill5Icon(0);
    }
    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        if(curSpawnDelay > maxSpawnDelay )
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
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
    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint= Random.Range(0, 9);
        GameObject enemy = objectManager.MakeObj(enemyObjs[ranEnemy]);
        enemy.transform.position = spawnPoints[ranPoint].position;
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.gameManager = this;
        enemyLogic.objectManager = objectManager;
        if (ranPoint==5||ranPoint==6)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed , -1);
        }
        else if(ranPoint==7||ranPoint==8)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed*(-1), -1);
        }
        else
        {
            rigid.velocity = new Vector2(0,enemyLogic.speed*(-1));
        }
    }
}
