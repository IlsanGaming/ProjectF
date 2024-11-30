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
    public Animator endButton;
    public Animator endButtonText;
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
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 }; // �������� �ʿ��� ����ġ �迭
    public int level; // ���� �÷��̾� ����

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
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        spawnList = new List<Spawn>();
        enemyObjs = new string[] { "enemyL", "enemyM", "enemyS", "enemyB" };
    }
    public void StageStart()
    {
        AudioManager.instance.PlayBgm(true);
        Debug.Log("StageStart ����"); // ����� �α� �߰�
        // �ִϸ����� �ʱ�ȭ
        startAnim.ResetTrigger("On");
        startFadeAnim.ResetTrigger("In");

        // �ִϸ��̼� ����
        StartCoroutine(StartAnimation());
        
        ReadSpawnFile();
        isLive = true;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }
    IEnumerator StartAnimation()
    {
        Debug.Log("StartAnimation ����"); // ����� �α� �߰�
        yield return new WaitForSeconds(.01f);

        Debug.Log("StartAnimation �߰�"); // ����� �α� �߰�
        if (startIntro != null) startIntro.SetActive(true);
        if (startAnim != null) startAnim.SetTrigger("On");
        if (startFadeAnim != null) startFadeAnim.SetTrigger("In");

        yield return new WaitForSeconds(6f);

        Debug.Log("StartAnimation ����"); // ����� �α� �߰�
        if (startIntro != null) startIntro.SetActive(false);
    }

    public void StageWin()
    {
        isLive = false;
        StartCoroutine(WinResult());
        Player.instance.transform.position = new Vector3(0, -3.5f, 0);
        player.gameObject.transform.localScale = Vector3.zero;
        startIntro.SetActive(true);
    }
    IEnumerator WinResult()
    {
        yield return new WaitForSeconds(.01f);
        endIntro.SetActive(true);
        endFadeAnim.SetTrigger("Out");
        uiResult.Win();
        yield return new WaitForSeconds(.5f);
        endButton.SetTrigger("On");
        endButtonText.SetTrigger("On");
        yield return new WaitForSeconds(1.1f);
        Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
        AudioManager.instance.PlayBgm(false);
    }
    public void StageLose()
    {
        isLive = false;
        StartCoroutine(LoseResult());
        Player.instance.transform.position = new Vector3(0, -3.5f, 0);
        player.gameObject.transform.localScale = Vector3.zero;
        startIntro.SetActive(true);
        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }
    IEnumerator LoseResult()
    {
        yield return new WaitForSeconds(.01f);
        endIntro.SetActive(true);
        endFadeAnim.SetTrigger("Out");
        uiResult.Lose();
        yield return new WaitForSeconds(.5f);
        endButton.SetTrigger("On");
        endButtonText.SetTrigger("On");
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
        if (curSpawnDelay > nextSpawnDealy && !spawnEnd)
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
        if (difficulty > maxDifficulty)
        {
            difficulty = maxDifficulty;
        }
    }
    public void NextStage()
    {

    }
    public void StageEnd()
    {

    }
    void ReadSpawnFile()
    {
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        TextAsset textFile = Resources.Load("Stage" + stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
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
        switch (spawnList[spawnIndex].type)
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
                enemyIndex = 3; // ����
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
        if (enemyPoint == 5 || enemyPoint == 6)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -0.9f);
        }
        else if (enemyPoint == 7 || enemyPoint == 8)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -0.9f);
        }
        else
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }

        spawnIndex++;
        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }
        nextSpawnDealy = spawnList[spawnIndex].delay;
    }
    // ���� ȿ���� ȣ���ϴ� �Լ�
    public void CallExplosion(Vector3 pos, string type)
    {
        GameObject explosion = objectManager.MakeObj("explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();
        explosion.transform.position = pos;
        explosionLogic.StartExplosion(type); // ���� �ִϸ��̼� ����
    }
    // ����ġ�� ȹ���ϰ� ������ ó��
    public void GetExp(int gainexp)
    {
        if (!isLive) // ������ ��Ȱ��ȭ ���¸� �������� ����
            return;

        exp += gainexp;

        // ����ġ�� ���� ������ �䱸ġ�� �����ϸ� ������ ó��
        if (exp > nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++; // ���� ����
            exp = nextExp[Mathf.Min(level, nextExp.Length - 1)]-exp;
            Debug.Log("���� ���� : " + level);
            Debug.Log("�ʿ� ����ġ" + nextExp[level]);
            StartCoroutine(ReadyToShow());
        }
    }
    IEnumerator ReadyToShow()
    {
        yield return null;
        Player.instance.boomEffect.SetActive(false);
        yield return null;
        uiLevelUp.Show(); // ������ UI ǥ��
    }
    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}