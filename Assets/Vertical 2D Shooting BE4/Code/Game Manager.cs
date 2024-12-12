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
    public int[] nextExp; // 레벨업에 필요한 경험치 배열
    public int level; // 현재 플레이어 레벨

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
        QualitySettings.vSyncCount = 0; // VSync 비활성화
        Application.targetFrameRate = 60; // 60FPS로 고정
        Time.fixedDeltaTime = 1f / 60f; // 물리 연산 주기 설정
    }
    public void ShowPrologue1()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("버튼1활성화됨");
        prologueFrame.SetActive(true);
        prologue[0].SetActive(true);
    }
    public void ShowPrologue2()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("버튼1비활성화됨");
        prologue[0].SetActive(false);
        prologue[1].SetActive(true);
        Debug.Log("버튼2활성화됨");
    }
    public void ShowPrologue3()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("버튼2비활성화됨");
        prologue[1].SetActive(false);
        prologue[2].SetActive(true);
        Debug.Log("버튼3활성화됨");
    }
    public void ShowPrologue4()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("버튼3비활성화됨");
        prologue[2].SetActive(false);
        prologue[3].SetActive(true);
        Debug.Log("버튼4활성화됨");
    }
    public void ShowPrologue5()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("버튼4비활성화됨");
        prologue[3].SetActive(false);
        prologue[4].SetActive(true);
        Debug.Log("버튼5활성화됨");
    }
    public void ShowPrologue6()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("버튼5비활성화됨");
        prologue[4].SetActive(false);
        prologue[5].SetActive(true);
        Debug.Log("버튼6활성화됨");
    }
    public void ShowPrologue7()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("버튼6비활성화됨");
        prologue[5].SetActive(false);
        prologue[6].SetActive(true);
        Debug.Log("버튼7활성화됨");
    }
    public void ShowPrologue8()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("버튼7비활성화됨");
        prologue[6].SetActive(false);
        prologue[7].SetActive(true);
        Debug.Log("버튼8활성화됨");
    }
    public void EndPrologue()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Debug.Log("버튼8비활성화됨");
        prologue[7].SetActive(false);
        prologueFrame.SetActive(false);
        StageStart();
    }
    public void StageStart()
    {
        AudioManager.instance.PlayBgm(true);
        Debug.Log("StageStart 실행"); // 디버깅 로그 추가
        // 애니메이터 초기화
        startFadeAnim.ResetTrigger("In");
        // 애니메이션 실행
        StartCoroutine(StartAnimation());
        
        ReadSpawnFile();
        isLive = true;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }
    IEnumerator StartAnimation()
    {
        Debug.Log("StartAnimation 시작"); // 디버깅 로그 추가
        yield return new WaitForSeconds(.01f);

        Debug.Log("StartAnimation 중간"); // 디버깅 로그 추가
        if (startIntro != null) startIntro.SetActive(true);
        if (startFadeAnim != null) startFadeAnim.SetTrigger("In");

        yield return new WaitForSeconds(2f);

        Debug.Log("StartAnimation 종료"); // 디버깅 로그 추가
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

        // 난이도가 최대치를 넘지 않도록 제한
        if (newDifficulty > maxDifficulty)
        {
            newDifficulty = maxDifficulty;
            return;
        }

        // 난이도가 변경되었을 때만 텍스트를 표시
        if (newDifficulty != previousDifficulty)
        {
            ShowDiffText(newDifficulty);
            previousDifficulty = newDifficulty; // 이전 난이도를 업데이트
        }

        difficulty = newDifficulty; // 현재 난이도 업데이트
    }
    void ShowDiffText(int difflevel)
    {
        if (difficulty + 1 >= maxDifficulty)
            return;
        // DiffupText 배열의 해당 레벨 텍스트 활성화
        DiffupText[difflevel].SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Morse);
        // 5초 후 비활성화를 위한 코루틴 실행
        StartCoroutine(HideDiffTextAfterDelay(difflevel));
    }

    IEnumerator HideDiffTextAfterDelay(int difflevel)
    {
        // 5초 대기
        yield return new WaitForSeconds(5f);
        // DiffupText 배열의 해당 레벨 텍스트 비활성화
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
                enemyIndex = 3; // 보스
                AudioManager.instance.PlaySpecificBgm(bossBgm); // 보스 전용 BGM 재생
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
    // 폭발 효과를 호출하는 함수
    public void CallExplosion(Vector3 pos, string type)
    {
        GameObject explosion = objectManager.MakeObj("explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();
        explosion.transform.position = pos;
        explosionLogic.StartExplosion(type); // 폭발 애니메이션 시작
    }
    // 경험치를 획득하고 레벨업 처리
    public void GetExp(int gainexp)
    {
        if (!isLive) // 게임이 비활성화 상태면 동작하지 않음
            return;

        exp += gainexp;

        // 경험치가 다음 레벨업 요구치를 충족하면 레벨업 처리
        if (exp > nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++; // 레벨 증가
            exp = nextExp[Mathf.Min(level, nextExp.Length - 1)]-exp;
            Debug.Log("현재 레벨 : " + level);
            Debug.Log("필요 경험치" + nextExp[level]);
            StartCoroutine(ReadyToShow());
        }
    }
    IEnumerator ReadyToShow()
    {
        yield return null;
        uiLevelUp.Show(); // 레벨업 UI 표시
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