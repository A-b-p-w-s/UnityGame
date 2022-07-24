using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public int aggressivity;
    public Parameter parameter;
    public float speed;
    private Transform direction;
    public bool ishit;
    private AnimatorStateInfo info;
    private Animator anim;
    private Rigidbody2D rigdbody;

    private bool Invincible_bool;
    private float Hurt_time;


    private static Enemy instance;
    public static Enemy Instance
    {
        get
        {
            if (instance == null)
                instance = Transform.FindObjectOfType<Enemy>();
            return instance;
        }
    }
    private void OnEnable()
    {
       
        transform.position=new Vector3(playerController.Instance.Enemy_x,playerController.Instance.Enemy_y,0);
        GetComponent<Collider2D>().enabled = (true);
        health = 50;
    }
    void Start()
    {
        anim = transform.GetComponent<Animator>();
        rigdbody = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Hurt_time + 0.1 <= Time.time)
        {
            Invincible_bool = false;
        }

        if (ishit && !Invincible_bool)
        {
            Hurt_time = Time.time;
            Invincible_bool = true;
            health -= aggressivity;
            if (health <= 0)
            {
                GetComponent<Animator>().Play("die");
                GetComponent<Collider2D>().enabled = (false);

            }
            else 
            {
                if (direction.position.x > transform.position.x)
                {
                    rigdbody.velocity = new Vector2(-1 * Random.Range(0.8f, 1f) * speed, rigdbody.velocity.y * Random.Range(0f, 1f));
                }
                if (direction.position.x < transform.position.x)
                {
                    rigdbody.velocity = new Vector2(1 * Random.Range(0.8f, 1f) * speed, rigdbody.velocity.y * Random.Range(0f, 1f));
                }
                if (direction.position.y > transform.position.y)
                {
                    rigdbody.velocity = new Vector2(rigdbody.velocity.x * Random.Range(0f, 1f), -1 * Random.Range(0.8f, 1f) * speed);
                }
                if (direction.position.y < transform.position.y)
                {
                    rigdbody.velocity = new Vector2(rigdbody.velocity.x * Random.Range(0f, 1f), 1 * Random.Range(0.8f, 1f) * speed);
                }
                ishit = false;
            }
        }
    }

    public void die()
    {
        EnemyPool.Instance.ReturnPool(this.gameObject);
    }
    public void GetSpeed(float speed)
    {
        this.speed = speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="swort")
        {
            ishit = true;
            direction = collision.transform;
            aggressivity = collision.GetComponent<attack>().aggressivity;
            speed = collision.GetComponent<attack>().repel;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "swort")
        {
            ishit = false;
        }
    }
}
