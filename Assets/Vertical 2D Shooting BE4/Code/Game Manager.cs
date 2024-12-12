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
    public bool StoryClear;
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
    public GameObject[] DiffupText;
    public int previousDifficulty = -1;

    public int exp;
    public int[] nextExp; // �������� �ʿ��� ����ġ �迭
    public int level; // ���� �÷��̾� ����

    public GameObject player;
    public ObjectManager objectManager;
    public LevelUp uiLevelUp;
    public GameObject startIntro;
    public GameObject endIntro;
    public Result uiResult;
    public GameObject[] prologue;
    public GameObject[] epilogue;
    public GameObject prologueFrame;
    public GameObject epilogueFrame;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    public AudioClip bossBgm;

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
        enemyObjs = new string[] { "enemyL", "enemyM", "enemyS", "enemyB", "enemyEL", "enemyEM", "enemyES" };
    }
    void Start()
    {
        QualitySettings.vSyncCount = 0; // VSync ��Ȱ��ȭ
        Application.targetFrameRate = 60; // 60FPS�� ����
        Time.fixedDeltaTime = 1f / 60f; // ���� ���� �ֱ� ����
    }
    public void ShowPrologue1()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("��ư1Ȱ��ȭ��");
        prologueFrame.SetActive(true);
        prologue[0].SetActive(true);
    }
    public void ShowPrologue2()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("��ư1��Ȱ��ȭ��");
        prologue[0].SetActive(false);
        prologue[1].SetActive(true);
        Debug.Log("��ư2Ȱ��ȭ��");
    }
    public void ShowPrologue3()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("��ư2��Ȱ��ȭ��");
        prologue[1].SetActive(false);
        prologue[2].SetActive(true);
        Debug.Log("��ư3Ȱ��ȭ��");
    }
    public void ShowPrologue4()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("��ư3��Ȱ��ȭ��");
        prologue[2].SetActive(false);
        prologue[3].SetActive(true);
        Debug.Log("��ư4Ȱ��ȭ��");
    }
    public void ShowPrologue5()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("��ư4��Ȱ��ȭ��");
        prologue[3].SetActive(false);
        prologue[4].SetActive(true);
        Debug.Log("��ư5Ȱ��ȭ��");
    }
    public void ShowPrologue6()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("��ư5��Ȱ��ȭ��");
        prologue[4].SetActive(false);
        prologue[5].SetActive(true);
        Debug.Log("��ư6Ȱ��ȭ��");
    }
    public void ShowPrologue7()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("��ư6��Ȱ��ȭ��");
        prologue[5].SetActive(false);
        prologue[6].SetActive(true);
        Debug.Log("��ư7Ȱ��ȭ��");
    }
    public void ShowPrologue8()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("��ư7��Ȱ��ȭ��");
        prologue[6].SetActive(false);
        prologue[7].SetActive(true);
        Debug.Log("��ư8Ȱ��ȭ��");
    }
    public void EndPrologue()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("��ư8��Ȱ��ȭ��");
        prologue[7].SetActive(false);
        prologueFrame.SetActive(false);
        StageStart();
    }
    public void StageStart()
    {
        AudioManager.instance.PlayBgm(true);
        Debug.Log("StageStart ����"); // ����� �α� �߰�
        // �ִϸ����� �ʱ�ȭ
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
        if (startFadeAnim != null) startFadeAnim.SetTrigger("In");

        yield return new WaitForSeconds(2f);

        Debug.Log("StartAnimation ����"); // ����� �α� �߰�
        if (startIntro != null) startIntro.SetActive(false);
    }

    public void StageWin()
    {
        AudioManager.instance.PlayBgm(false);
        epilogueFrame.SetActive(true);
        epilogue[0].SetActive(true);
    }
    public void ReadyToEnd()
    {
        epilogue[0].SetActive(false);
        epilogue[1].SetActive(true);
    }
    public void PrintWinResult()
    {
        epilogue[1].SetActive(false);
        epilogueFrame.SetActive(false);
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
        if (difficulty+1 >= maxDifficulty)
            return;
        gameTime += Time.deltaTime;
        int newDifficulty = Mathf.FloorToInt(gameTime / (maxGameTime / 10f));

        // ���̵��� �ִ�ġ�� ���� �ʵ��� ����
        if (newDifficulty > maxDifficulty)
        {
            newDifficulty = maxDifficulty;
            return;
        }

        // ���̵��� ����Ǿ��� ���� �ؽ�Ʈ�� ǥ��
        if (newDifficulty != previousDifficulty)
        {
            ShowDiffText(newDifficulty);
            previousDifficulty = newDifficulty; // ���� ���̵��� ������Ʈ
        }

        difficulty = newDifficulty; // ���� ���̵� ������Ʈ
    }
    void ShowDiffText(int difflevel)
    {
        if (difficulty + 1 >= maxDifficulty)
            return;
        // DiffupText �迭�� �ش� ���� �ؽ�Ʈ Ȱ��ȭ
        DiffupText[difflevel].SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Morse);
        // 5�� �� ��Ȱ��ȭ�� ���� �ڷ�ƾ ����
        StartCoroutine(HideDiffTextAfterDelay(difflevel));
    }

    IEnumerator HideDiffTextAfterDelay(int difflevel)
    {
        // 5�� ���
        yield return new WaitForSeconds(5f);
        // DiffupText �迭�� �ش� ���� �ؽ�Ʈ ��Ȱ��ȭ
        DiffupText[difflevel].SetActive(false);
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
                AudioManager.instance.PlaySpecificBgm(bossBgm); // ���� ���� BGM ���
                break;
            case "EL":
                enemyIndex = 4;
                break;
            case "EM":
                enemyIndex = 5;
                break;
            case "ES":
                enemyIndex = 6;
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