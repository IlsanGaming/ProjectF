using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro를 사용하기 위한 네임스페이스
using UnityEngine.UI; // UI 요소를 다루기 위한 네임스페이스
using UnityEngine.SceneManagement; // 씬 전환을 위한 네임스페이스
using System.IO; // 파일 읽기를 위한 네임스페이스

public class GameManager : MonoBehaviour
{
    // 현재 스테이지를 나타내는 변수
    public int stage;

    // 스테이지 시작, 클리어, 페이드 애니메이션을 위한 애니메이터
    public Animator stageAnim;
    public Animator clearAnim;
    public Animator fadeAnim;

    // 플레이어 초기 위치를 설정하기 위한 Transform
    public Transform playerPos;

    // 적 종류를 저장하는 배열 (예: 작은 적, 중간 적, 큰 적, 보스)
    public string[] enemyObjs;

    // 적이 스폰될 위치를 저장하는 배열
    public Transform[] spawnPoints;

    // 다음 적 스폰까지의 지연 시간과 현재 지연 시간을 저장
    public float nextSpawnDelay;
    public float curSpawnDelay;

    // 플레이어 게임 오브젝트
    public GameObject player;

    // 점수 텍스트 표시와 UI 이미지들 (라이프, 폭탄 등)
    public TextMeshProUGUI scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;

    // 게임 오버 시 표시되는 UI 패널
    public GameObject gameOverSet;

    // 오브젝트 관리자를 참조하기 위한 변수
    public ObjectManager objectManager;

    // 적 스폰 데이터를 저장하는 리스트
    public List<Spawn> spawnList;

    // 현재 스폰 인덱스와 스폰 종료 여부
    public int spawnIndex;
    public bool spawnEnd;

    // Awake는 게임 오브젝트가 활성화될 때 초기화 작업을 수행
    void Awake()
    {
        // 스폰 데이터를 저장할 리스트 초기화
        spawnList = new List<Spawn>();

        // 적 종류 배열 초기화 (작은 적, 중간 적, 큰 적, 보스)
        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL", "EnemyB" };

        // 스테이지 시작 처리
        StageStart();
    }

    // 스테이지 시작 시 호출되는 함수
    public void StageStart()
    {
        // 스테이지 시작 애니메이션 재생
        stageAnim.SetTrigger("On");

        // 스테이지 시작 및 클리어 메시지 설정
        stageAnim.GetComponent<TextMeshProUGUI>().text = "Stage " + stage + "\nStart";
        clearAnim.GetComponent<TextMeshProUGUI>().text = "Stage " + stage + "\nClear!";

        // 스폰 파일 읽기
        ReadSpawnFile();

        // 페이드 인 애니메이션 재생
        fadeAnim.SetTrigger("In");
    }

    // 스테이지 종료 시 호출되는 함수
    public void StageEnd()
    {
        // 스테이지 클리어 애니메이션 재생
        clearAnim.SetTrigger("On");

        // 페이드 아웃 애니메이션 재생
        fadeAnim.SetTrigger("Out");

        // 플레이어를 초기 위치로 이동
        player.transform.position = playerPos.position;

        // 다음 스테이지로 이동
        stage++;
        if (stage > 2) // 마지막 스테이지를 넘었을 경우 게임 오버 처리
            Invoke("GameOver", 6);
        else
            Invoke("StageStart", 5); // 다음 스테이지 시작
    }

    // 적 스폰 데이터를 읽어오는 함수
    void ReadSpawnFile()
    {
        // 스폰 데이터 초기화
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // 현재 스테이지에 해당하는 텍스트 파일 읽기
        TextAsset textFile = Resources.Load("Stage " + stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        // 텍스트 파일의 각 줄을 읽어와 스폰 데이터로 변환
        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            Debug.Log(line); // 읽은 데이터를 로그에 출력

            if (line == null)
                break;

            // 스폰 데이터 생성 및 설정
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]); // 스폰 딜레이
            spawnData.type = line.Split(',')[1]; // 적의 타입
            spawnData.point = int.Parse(line.Split(',')[2]); // 스폰 포인트 인덱스
            spawnList.Add(spawnData);
        }

        // 텍스트 파일 닫기
        stringReader.Close();

        // 첫 번째 스폰 딜레이 설정
        nextSpawnDelay = spawnList[0].delay;
    }

    // 매 프레임 호출되는 함수
    void Update()
    {
        // 현재 스폰 딜레이를 증가
        curSpawnDelay += Time.deltaTime;

        // 스폰 딜레이가 초과되었고 스폰이 끝나지 않았을 경우
        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy(); // 적 스폰
            curSpawnDelay = 0; // 딜레이 초기화
        }

        // 점수 UI 업데이트
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score); // 천 단위 콤마 추가
    }

    // 적을 스폰하는 함수
    void SpawnEnemy()
    {
        int enemyIndex = 0;
        // 스폰 데이터에 따라 적의 인덱스 설정
        switch (spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0; // 작은 적
                break;
            case "M":
                enemyIndex = 1; // 중간 적
                break;
            case "L":
                enemyIndex = 2; // 큰 적
                break;
            case "B":
                enemyIndex = 3; // 보스
                break;
        }

        // 스폰 포인트 설정
        int enemyPoint = spawnList[spawnIndex].point;
        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;

        // 적의 움직임 및 속도 설정
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.gameManager = this;
        enemyLogic.objectManager = objectManager;

        if (enemyPoint == 5 || enemyPoint == 6) // 오른쪽에서 등장
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if (enemyPoint == 7 || enemyPoint == 8) // 왼쪽에서 등장
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else // 정면에서 등장
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }

        // 다음 스폰 데이터로 전환
        spawnIndex++;
        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true; // 스폰 종료
            return;
        }

        // 다음 스폰 딜레이 갱신
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }

    // 플레이어를 리스폰하는 함수
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerEXE", 2f); // 2초 후 리스폰 실행
    }
    public void RespawnPlayerEXE()
    {
        player.transform.position = Vector3.down * 3.5f; // 초기 위치로 이동
        player.SetActive(true); // 활성화
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isHit = false; // 무적 상태 해제
    }

    // 폭발 효과를 호출하는 함수
    public void CallExplosion(Vector3 pos, string type)
    {
        GameObject explosion = objectManager.MakeObj("Explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();
        explosion.transform.position = pos;
        explosionLogic.StartExplosion(type); // 폭발 애니메이션 시작
    }

    // 라이프 아이콘 UI를 업데이트하는 함수
    public void UpdateLifeIcon(int life)
    {
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0); // 기본 비활성화
        }
        for (int index = 0; index < life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1); // 활성화
        }
    }

    // 폭탄 아이콘 UI를 업데이트하는 함수
    public void UpdateBoomIcon(int boom)
    {
        for (int index = 0; index < 3; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 0); // 기본 비활성화
        }
        for (int index = 0; index < boom; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 1); // 활성화
        }
    }

    // 게임 오버 처리를 위한 함수
    public void GameOver()
    {
        gameOverSet.SetActive(true); // 게임 오버 UI 활성화
    }

    // 게임 재시작을 위한 함수
    public void GameRetry()
    {
        SceneManager.LoadScene(0); // 첫 번째 씬 로드
    }
}
