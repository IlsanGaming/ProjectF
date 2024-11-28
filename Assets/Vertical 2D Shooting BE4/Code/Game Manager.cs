using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.ComponentModel;


public class GameManager : MonoBehaviour
{
    public int stage;
    public bool storyClear;

    public Animator startAnim;
    public Animator endAnim;
    public Animator startFadeAnim;
    public Animator endFadeAnim;
    public static GameManager instance;

    public bool isLive;
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
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 }; // 레벨업에 필요한 경험치 배열
    public int level; // 현재 플레이어 레벨

    public GameObject player;
    public ObjectManager objectManager;
    public LevelUp uiLevelUp;
    public GameObject startIntro;
    public GameObject endIntro;
    public Result uiResult;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    void Awake()
    {
        instance = this;
        spawnList = new List<Spawn>();
        enemyObjs = new string[] { "enemyL", "enemyM", "enemyS","enemyB" };
        StageStart();
    }
    public void StageStart()
    {
        StartCoroutine(StartAnimation());
        ReadSpawnFile();
    }
    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(.01f);
        startIntro.SetActive(true);
        startAnim.SetTrigger("On");
        startFadeAnim.SetTrigger("In");
    }
    public void StageEnd()
    {
        isLive = false;
        StartCoroutine(EndAnimation());
        Player.instance.transform.position = new Vector3(0, -3.5f, 0);
        player.gameObject.transform.localScale=Vector3.zero;
    }
    IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(.01f);
        endIntro.SetActive(true);
        endFadeAnim.SetTrigger("Out");
        yield return new WaitForSeconds(1.1f);
        Stop();
    }
    void Update()
    {
        if (!isLive)
        {
            return;
        }
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

        TextAsset textFile=Resources.Load("Stage"+stage) as TextAsset;
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
            case "B":
                enemyIndex = 3; // 보스
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
    // 폭발 효과를 호출하는 함수
    public void CallExplosion(Vector3 pos, string type)
    {
        GameObject explosion = objectManager.MakeObj("explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();
        explosion.transform.position = pos;
        explosionLogic.StartExplosion(type); // 폭발 애니메이션 시작
    }
    // 경험치를 획득하고 레벨업 처리
    public void GetExp()
    {
        if (!isLive) // 게임이 비활성화 상태면 동작하지 않음
            return;

        exp++; // 경험치 증가

        // 경험치가 다음 레벨업 요구치를 충족하면 레벨업 처리
        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++; // 레벨 증가
            exp = 0; // 경험치 초기화
            uiLevelUp.Show(); // 레벨업 UI 표시
        }
    }
    public void Stop()
    {
        isLive=false;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
