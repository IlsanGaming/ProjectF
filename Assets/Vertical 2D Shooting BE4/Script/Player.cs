using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Moving and Attck 변수 할당
    public float speed;
    public float MaxShotDelay;
    public float CurShotDelay;
    public int Power;
    public int maxPower;
    public int boom;
    public int maxboom;

    //life and score 변수 할당
    public int life;
    public int score;

    //플레이어가 BorderLine에 닿았을시
    //조작키가 비활성화 되도록 하는 변수
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;

    //플레이어가 발사하는 총알 오브젝트
    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject boomEffect;


    //Game 매니저 연결
    public GameManager gameManager;
    public ObjectManager objectManager;
    public bool isHit;
    public bool isBoomTime;

    public GameObject[] followers;
    public bool isRespawnTime;

    //좌우키를 누를시 애니메이션 활용
    Animator animator;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        //애니메이터 할당
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        isRespawnTime = true;
        Invoke("Unbeatable", 3);
    }
    void Unbeatable(bool active)
    {
        isRespawnTime = !isRespawnTime;
        if (active)
        {
            isRespawnTime = true;
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            for(int index=0;index<followers.Length;index++)
            {//무적타임 이펙트(투명)
                followers[index].GetComponent<SpriteRenderer>().color= new Color(1, 1, 1, 0.5f);
            }
        }
        else
        {
            isRespawnTime = false;
            spriteRenderer.color = new Color(1, 1, 1, 1);
            for (int index = 0; index < followers.Length; index++)
            {
                //무적타임 종료(원래대로)
                followers[index].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }
    void Update()
    {
        //이동. 발사. 재장전으로 간소화
        Move();
        Fire();
        Boom();
        Reload();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == -1)||(isTouchLeft && h ==1))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v==1)||(isTouchBottom && v==-1))
            v = 0;

        Vector3 curPos =transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        if(Input.GetButtonDown("Horizontal")||(Input.GetButtonUp("Horizontal")))
        {
            animator.SetInteger("Input", (int)h);
        }
        
    }
    void Boom()
    {
        if (!Input.GetButton("Fire2"))
            return;
        if (isBoomTime)
            return;
        if(boom ==0)
            return;

        boom--;
        isBoomTime = true;
        gameManager.UpdateBoomIcon(boom);
        //Effect Visible
        boomEffect.SetActive(true);
        Invoke("OffBomeEffect", 4f);

        //Remove Enemy
        GameObject[] enemiesL = objectManager.GetPool("EnemyL");
        GameObject[] enemiesM = objectManager.GetPool("EnemyM");
        GameObject[] enemiesS = objectManager.GetPool("EnemyS");
        for (int index = 0; index < enemiesL.Length; index++)
        {
            if (enemiesL[index].activeSelf)
            {
                Enemy enemyLogic = enemiesL[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }
        for (int index = 0; index < enemiesM.Length; index++)
        {
            if (enemiesM[index].activeSelf)
            {
                Enemy enemyLogic = enemiesM[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }
        for (int index = 0; index < enemiesS.Length; index++)
        {
            if (enemiesS[index].activeSelf)
            {
                Enemy enemyLogic = enemiesS[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }
        //Remove Enemy Bullet
        GameObject[] bulletsA = objectManager.GetPool("BulletEnemyA");
        GameObject[] bulletsB = objectManager.GetPool("BulletEnemyB");
        for (int index = 0; index < bulletsA.Length; index++)
        {
            if (bulletsA[index].activeSelf)
            {
                bulletsA[index].SetActive(false);
            }
        }
        for (int index = 0;index<bulletsB.Length; index++)
        {
            if (bulletsB[index].activeSelf)
            {
                bulletsB[index].SetActive(false);
            }
        }

    }
    void Fire()
    {
        if(!Input.GetButton("Fire1"))
            return;

        if (CurShotDelay < MaxShotDelay)
            return;

        switch(Power)
        {
            case 1:
                //Power One
                GameObject bullet =objectManager.MakeObj("BulletPlayerA");
                bullet.transform.position= transform.position;
                Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
                rigidbody.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletR = objectManager.MakeObj("BulletPlayerA");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;
                GameObject bulletL = objectManager.MakeObj("BulletPlayerA");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;

                Rigidbody2D rigidbodyR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidbodyL = bulletL.GetComponent<Rigidbody2D>();
                rigidbodyR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidbodyL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            default:
                GameObject bulletRR = objectManager.MakeObj("BulletPlayerA");
                bulletRR.transform.position = transform.position + Vector3.right * 0.35f;

                GameObject bulletCC = objectManager.MakeObj("BulletPlayerB");
                bulletCC.transform.position = transform.position;

                GameObject bulletLL = objectManager.MakeObj("BulletPlayerA");
                bulletLL.transform.position = transform.position + Vector3.left * 0.35f;

                Rigidbody2D rigidbodyRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidbodyCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidbodyLL = bulletLL.GetComponent<Rigidbody2D>();
                rigidbodyRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidbodyCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidbodyLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break ;
        }
        CurShotDelay = 0;
    }
    void Reload()
    {
        CurShotDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true; break;
                case "Bottom":
                    isTouchBottom = true; break;
                case "Right":
                    isTouchRight = true; break;
                case "Left":
                    isTouchLeft = true; break;
            }
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            if (isRespawnTime)
                return;
            if (isHit)
                return;

            isHit = true;
            life--;
            gameManager.UpdateLifeIcon(life);
            gameManager.CallExplosion(transform.position, "P");

            if (life == 0)
            {
                gameManager.GameOver();
            }
            else
            {
                gameManager.RespawnPlayer();
            }
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            if (collision.gameObject.tag == "Enemy")
            {
                GameObject bossGo = collision.gameObject;
                Enemy enemyBoss = bossGo.GetComponent<Enemy>();
                if (enemyBoss.enemyname == "B")
                {
                    return;
                }
                else
                {
                    collision.gameObject.SetActive(false);
                }
            }
        }
        else if (collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Coin":
                    score += 1000;
                    break;
                case "Power":
                    if (Power == maxPower)
                        score += 500;
                    else
                    {
                        Power++;
                        AddFollower();
                    }


                    break;
                case "Boom":
                    if (boom == maxboom)
                        score += 500;
                    else
                    {
                        boom++;
                        gameManager.UpdateBoomIcon(boom);
                    }

                    break;
            }
            Destroy(collision.gameObject);
        }
    }
    void AddFollower()
    {
        if(Power==4)
            followers[0].SetActive(true);
        else if(Power==5)
            followers[1].SetActive(true);
        else if(Power==6)
            followers[2].SetActive(true);
    }

    void OffBomeEffect()
    {
        boomEffect.SetActive(false);
        isBoomTime= false;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false; break;
                case "Bottom":
                    isTouchBottom = false; break;
                case "Right":
                    isTouchRight = false; break;
                case "Left":
                    isTouchLeft = false; break;
            }
        }
    }
}
