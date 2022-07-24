using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class player_info
{
    public bool MT;
    public float ReloadMoveSpeed;
    public int ShootNum;
    public float Reload;
    public int BulletsNumMAX;
    public int BulletsNum;
    public float blive_time;
    public float BulletsSpeed;
    public bool isexplode;
    public bool qiisexploed;
    public float PickingRange;
    public float SwordQi_aggressivity_1;
    public float SwordQi_repel_1;
    public float SwordQiSpeed1;
    public float aggressivity_1;
    public float repel_1;
    public float attack_speed_1;
    public float bloodsuck;
    public float SwordQi_aggressivity_2;
    public float SwordQi_repel_2;
    public float SwordQiSpeed2;
    public int aggressivity_2;
    public float repel_2;
    public float attack_speed_2;
    public float Invincible_time;
    public float speed = 5f;
    public int LV;
    public float Health_add_cd;
    public float puncture;
}

public class playerController : MonoBehaviour
{

    private static playerController instance;
    public static playerController Instance
    {
        get
        {
            if (instance == null)
                instance = Transform.FindObjectOfType<playerController>();
            return instance;
        }
    }

    [Header("枪械")]
    public GameObject[] guns;
    public Text BulletsNumText;
    public Image BulletsNumimage;



    // Start is called before the first frame update
    public player_info player_Info;
    float rolltime = 0f;
    public Rigidbody2D rb;
    public Animator anim;
    public Animator trapanim;
    public float shakeTime;
    public float shakeStrenght;
    private float moveSpeed;
    public Collider2D play_wall;
    public float Exp;
    private float needexp;
    public CircleCollider2D PickingRange;
    public GameObject shield;


    [Header("游戏UI")]
    public float shield_value;
    public float shield_max;
    public float shield_CD;
    private float shield_time;
    public int Hurt_value=6;
    public int Health_max;
    public Text h_num;
    public Text h_full;
    public Image h_image;
    public GameObject GAMEOVER;
    public Image shield_image;

    private bool isHurt;
    private bool Invincible_bool;
    private float Hurt_time;

    [Header("鼠标指针")]
    public Texture2D aim;

    [Header("等级参数")]
    public Image ExpImage;
    public Text ExpText;
    public float n;
    public float starexp;

    [Header("游戏时间组件")]
    public Text GameTime;

    [Header("CD的UI组件")]
    public Image CD_image;


    [Header("Dash参数")]
    public float dasgTime;
    private float dashTimeLeft;
    private float lastDash=-10f;
    public float dashCoolDowm;
    public float dashSpeed;
    public bool isDashing;
    private float dash_x;
    private float dash_y;


    [Header("敌人生成时间")]
    public float product_enemy_time;
    private float product_time;
    public float enemy_health_add;
    public float enemy_health;
    private float enmey_health_value;

    [Header("敌人生成坐标")]
    public Transform Enemy_transform;
    public float Enemy_x;
    public float Enemy_y;

    public GameObject StatusUI;
    private float health_cd;
    private float gametime;
    public GameObject StatrGame;
    public GameObject win;

    public bool isattack;
    private float reloadtime;
    void Start()
    {
        Cursor.SetCursor(aim,new Vector2(16,16),CursorMode.Auto);

        StatrGame.SetActive(true);
        Time.timeScale = 0f;
        transform.position = new Vector3(0, 0, -0.5f);
        Hurt_value = Health_max;
        h_full.text = Hurt_value + "";
        h_num.text = "X" + Hurt_value;
        GameTime.text = Time.time + "";
        product_time = product_enemy_time;
        product_enemy();
        ExpText.text = "LV: " + player_Info.LV;
        Exp = 0;
        needexp = starexp * MathF.Pow(n, player_Info.LV);
        shield_value = shield_max;
        shield_time = shield_CD;
        enmey_health_value = enemy_health;
        health_cd = player_Info.Health_add_cd;
    }
    public void GAMESTART()
    {
        reloadtime = player_Info.Reload;
        BulletsNumText.text = player_Info.BulletsNumMAX + "/" + player_Info.BulletsNum;
        player_Info.BulletsNum = player_Info.BulletsNumMAX;
        GameObject.FindGameObjectWithTag("pool").gameObject.SetActive(true);
        gametime = 0;
        transform.position = new Vector3(0, 0, -0.5f);
        Hurt_value = Health_max;
        h_full.text = Hurt_value + "";
        h_num.text = "X" + Hurt_value;
        GameTime.text = Time.time + "";
        product_time = product_enemy_time;
        product_enemy();
        ExpText.text = "LV: " + player_Info.LV;
        Exp = 0;
        needexp = starexp * MathF.Pow(n, player_Info.LV);
        shield_value = shield_max;
        shield_time = shield_CD;
        enmey_health_value = enemy_health;
        health_cd = player_Info.Health_add_cd;
    }


    // Update is called once per frame
    void Update()
    {
        if (player_Info.BulletsNum <= 0)
        {
            isReload = true;
            BulletsNumText.color = new Color(1, 0, 0, 1);
        }
            
        gametime += Time.deltaTime;
        GameTime.text = (19 - (int)gametime / 60) + ":" + (59 - (int)gametime % 60);
        if (gametime >= 1199)
        {
            Time.timeScale = 0f;
            win.SetActive(true);
        }
        if (Health_max > Hurt_value && player_Info.Health_add_cd>0)
        {
            health_cd -= Time.deltaTime;
            if (health_cd <= 0)
            {
                Hurt_value++;
                h_image.GetComponent<Animator>().Play("Health_change");
                health_cd = player_Info.Health_add_cd;
                h_num.text = Hurt_value + "";
            }
        }
        PickingRange.radius = player_Info.PickingRange;
        ExpImage.fillAmount = Exp / needexp;
        shield_image.fillAmount = shield_value / shield_max;
        if(Exp>= needexp){
            player_Info.LV += 1;
            ExpText.text = "LV: " + player_Info.LV;
            Exp -= needexp;
            needexp = starexp * MathF.Pow(n, player_Info.LV);
            enemy_health = enmey_health_value+ enemy_health_add * MathF.Pow(1.1f,player_Info.LV);
            Time.timeScale = 0f;
            StatusUI.SetActive(true);

        }
        if (shield_value > 0)
        {
            shield_image.GetComponentInChildren<Text>().text = shield_value + "";
        }
        else
            shield_image.GetComponentInChildren<Text>().text ="";
        if (shield_value<shield_max)
        {
            shield_time -= Time.deltaTime;
            if (shield_time <= 0)
            {
                shield_value++;
                shield_time = shield_CD;
            }
        }

        if (shield_value<=0)
        {
            shield.SetActive(false);
        }
        else
        {
            shield.SetActive(true);
        }
        if (Hurt_value <= 0)
        {
            transform.GetComponent<Animator>().Play("die");
        }
        CD_image.fillAmount -= 1.0f / dashCoolDowm * Time.deltaTime;
        product_time -= Time.deltaTime;
        if (product_time <= 0)
        {
            for(int i=1+(int)gametime/ 30; i > 0; i--)
            {
                product_enemy();
            }
            product_time = product_enemy_time;
        }
        if (Hurt_time+ player_Info.Invincible_time <= Time.time)
        {
            Invincible_bool = false;
            anim.SetBool("hurting", false);
        }
        if (Input.GetButtonDown("Jump"))
        {
            dash_x = Pistol.Instance.direction.x;
            dash_y = Pistol.Instance.direction.y;
            if (Time.time >= (lastDash + dashCoolDowm))
            {
                ReadyToDash();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            isReload = true;
        }

        if (!isHurt && !isDashing)
        {
            Movement();
        }
        if (Mathf.Abs(rb.velocity.x)<0.1f)
        {
            isHurt = false;
        }
    }

    public void Reloading()
    {
        if (reloadtime>0)
        {
            reloadtime -= Time.deltaTime;
            BulletsNumimage.fillAmount += 1.0f / player_Info.Reload * Time.deltaTime;
        }
        else
        {
            reloadtime = player_Info.Reload;
            player_Info.BulletsNum = player_Info.BulletsNumMAX;
            BulletsNumimage.fillAmount =0;
            BulletsNumText.color = new Color(1, 1, 1, 1);
            isReload = false;
        }
    }
    public void Play_Start()
    {
        Time.timeScale = 1f;
    }
    public void Play_die()
    {
        transform.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("pool").gameObject.SetActive(false);
        GAMEOVER.SetActive(true);
        Time.timeScale = 0f;
    }

    public bool isReload;
    private void FixedUpdate()
    {
        Dash();
        if (isReload)
        {
            Reloading();
        }
    }

    void ReadyToDash()
    {
        isDashing = true;
        dashTimeLeft = dasgTime;
        lastDash = Time.time;
        CD_image.fillAmount = 1;
    }

    void Dash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                rb.velocity = new Vector2(dash_x * dashSpeed, dash_y * dashSpeed);
                dashTimeLeft -= Time.deltaTime;
                Pool.Instance.GetFromPooll();
                Invincible_bool = true;
                GetComponent<Collider2D>().enabled=(false);
                play_wall.enabled = (true);
            }
            else
            {
                rb.velocity = new Vector2(0,0);
                Invincible_bool = false;
                isDashing = false;
                GetComponent<Collider2D>().enabled = (true);
                play_wall.enabled = (false);

            }
        }
    }
    void Movement()
    {
        float horizontalmove=Input.GetAxis("Horizontal");
        float verticalomove = Input.GetAxis("Vertical");
        float facedircetion = Input.GetAxisRaw("Horizontal");
        //角色移动
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * moveSpeed, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(horizontalmove));
        }
        if (verticalomove != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, verticalomove * moveSpeed);
            anim.SetFloat("running", Mathf.Abs(verticalomove));
        }
        if (isReload||isattack)
        {
            if (player_Info.speed - player_Info.ReloadMoveSpeed >0)
                moveSpeed = player_Info.speed - player_Info.ReloadMoveSpeed;
            else
                moveSpeed = 0;
        }
        else
        {
            moveSpeed = player_Info.speed;
        }
        /*if (facedircetion != 0 )
        {
            transform.localScale = new Vector3(facedircetion, 1, 1);
        }*/

    }

    void product_enemy()
    {/*
        if (UnityEngine.Random.value>=0.5)
        {
            if(UnityEngine.Random.value >= 0.5)
            {
                if (-59.5f < transform.position.x - 10)
                {
                   Enemy_x = UnityEngine.Random.Range(-59.5f, transform.position.x - 10);
                }
                else
                {
                    Enemy_x = UnityEngine.Random.Range(transform.position.x + 10, 19f);
                }
            }
            else
            {
                if (59.5f > transform.position.x + 10)
                {

                    Enemy_x = UnityEngine.Random.Range(transform.position.x + 10, 19f);
                }
                else
                {
                    Enemy_x = UnityEngine.Random.Range(-19f, transform.position.x - 10);
                }
            }

            Enemy_y = UnityEngine.Random.Range(-10f, 10f);
        }
        else
        {
            if (UnityEngine.Random.value >= 0.5)
            {
                if(-10f < transform.position.y - 5)
                {
                    Enemy_y= UnityEngine.Random.Range(-10f, transform.position.y - 5);
                }
                else
                {
                    Enemy_y = UnityEngine.Random.Range(transform.position.y + 5 ,10f);
                }
            }
            else
            {
                if (10f > transform.position.y + 5)
                {
                    Enemy_y = UnityEngine.Random.Range(transform.position.y + 5, 10f);
                }
                else
                {
                    Enemy_y = UnityEngine.Random.Range(-10f, transform.position.y - 5);
                }
            }
            Enemy_x = UnityEngine.Random.Range(-19f, 19f);
        }*/
        if(UnityEngine.Random.Range(0f, 1f) > 0.5)
        {
            if (UnityEngine.Random.Range(0f, 1f) > 0.5)
            {
                Enemy_x = UnityEngine.Random.Range(transform.position.x - 20, transform.position.x - 15);
            }
            else
                Enemy_x = UnityEngine.Random.Range(transform.position.x + 15, transform.position.x + 20);
            Enemy_y = UnityEngine.Random.Range(transform.position.y-15,transform.position.y+15);
        }
        else
        {
            if (UnityEngine.Random.Range(0f, 1f) > 0.5)
            {
                Enemy_y = UnityEngine.Random.Range(transform.position.y - 15, transform.position.y - 10);
            }
            else
                Enemy_y = UnityEngine.Random.Range(transform.position.y + 10, transform.position.y + 15);
            Enemy_x = UnityEngine.Random.Range(transform.position.x - 20, transform.position.x + 20);
        }
            
        EnemyPool.Instance.GetFromPooll();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "trap" && !Invincible_bool)
        {
            Hurt_time = Time.time;
            Invincible_bool = true;
            trapanim = collision.GetComponent<Animator>();
            trapanim.SetBool("trap", true);
            isHurt = true;
            if (shield_value > 0)
            {
                shield_value--;
                shield.GetComponent<Animator>().Play("ShieldHurt");
            }
            else
            {
                h_image.GetComponent<Animator>().Play("Health_change");
                Hurt_value -= 1;
                h_num.text = "X" + Hurt_value;  
                anim.SetBool("hurting", true);
                anim.SetFloat("running", 0);
            }
            if (transform.position.x < collision.transform.position.x)
            {
                rb.velocity = new Vector2(-8, rb.velocity.y);
            }
            if (transform.position.x > collision.transform.position.x)
            {
                rb.velocity = new Vector2(8, rb.velocity.y);
            }
            if (transform.position.y < collision.transform.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, -8);
            }
            if (transform.position.y > collision.transform.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, 8);
            }
            hurtingScense.Instance.CameraShake(shakeTime, shakeStrenght);
        } 
    }
    
    private float xx;
    private float yy;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "trap")
        {
            trapanim.SetBool("trap", false);
        }
        if (collision.tag == "back")
        {
            if (transform.position.x > 0)
            {
                xx = 1;
            }
            else
            {
                xx = -1;
            }
            if(transform.position.y>0)
            {
                yy = 1;
            }
            else
            {
                yy = -1;
            }
            if (Mathf.Abs(transform.position.x) > 59.5f && Mathf.Abs(transform.position.y) > 32.5f)
                transform.localPosition = new Vector3(xx * 59.5f, yy * 32.5f, -0.5f);
            else
            {
                if(Mathf.Abs(transform.position.x) > 59.5f)
                {
                    transform.localPosition = new Vector3(xx * 59.5f, transform.position.y, -0.5f);
                }
                if (Mathf.Abs(transform.position.y) > 32.5f)
                {
                    transform.localPosition = new Vector3(transform.position.x, yy * 32.5f, -0.5f);
                }
            }
        }
    }
    public bool isattacked;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isattacked && !Invincible_bool)
        {
            isattacked = false;
            Hurt_time = Time.time;
            Invincible_bool = true;
            if (shield_value > 0)
            {
                shield_value--;
                shield.GetComponent<Animator>().Play("ShieldHurt");
            }
            else
            {
                h_image.GetComponent<Animator>().Play("Health_change");
                Hurt_value -= 1;
                h_num.text = "X" + Hurt_value;
                isHurt = true;
                anim.SetBool("hurting", true);
                anim.SetFloat("running", 0);
                
            }

            if (transform.position.x < collision.transform.position.x)
            {
                rb.velocity = new Vector2(-8, rb.velocity.y);
            }
            if (transform.position.x > collision.transform.position.x)
            {
                rb.velocity = new Vector2(8, rb.velocity.y);
            }
            if (transform.position.y < collision.transform.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, -8);
            }
            if (transform.position.y > collision.transform.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, 8);
            }
            hurtingScense.Instance.CameraShake(shakeTime, shakeStrenght);
        }
    }
}
