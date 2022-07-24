using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StateType
{
    Idle,Patrol,Chase,React,Attack,Hurt
}
[Serializable]
public class Parameter{
    public int health;
    public float moveSpeed;
    public float chaseSpeed;
    public float idleTime;
    public Transform[] patrolPoints;
    public Transform[] chasePoints;
    public Animator animator;
}
public class FSM : MonoBehaviour
{
    public Parameter parameter;
    public float speed;
    public Vector2 direction;
    public bool ishit;
    public int aggressivity;
    private int i=0;
    public float expvalue;

    // Start is called before the first frame update
    private IState currentState;
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();
    private void OnEnable()
    {
       transform.GetComponent<CapsuleCollider2D>().enabled = (true);
        parameter.health = (int)playerController.Instance.enemy_health;
        expvalue = (int)playerController.Instance.enemy_health / 2;
        transform.position = new Vector3(playerController.Instance.Enemy_x, playerController.Instance.Enemy_y, 0);
        if (i == 1)
            TransitionState(StateType.Idle);
    }
    void Start()
    {
        parameter.animator = GetComponent<Animator>();

        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Patrol, new PatrolState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.React, new ReactState(this));
        states.Add(StateType.Attack, new AttackState(this));
        states.Add(StateType.Hurt, new HurtState(this));

        TransitionState(StateType.Idle);
        i++;
        
    }
    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }

    public void TransitionState(StateType type)
    {
        if (currentState! != null)
            currentState.OnExit();
        currentState = states[type];
        currentState.OnEnter();
    }

    public void FlipTo(Transform target)
    {
        if(target != null)
        {
            if (transform.position.x > target.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (transform.position.x < target.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    public bool isattack;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player"&&isattack)
        {
            TransitionState(StateType.Idle);
            isattack = false;
            playerController.Instance.isattacked = true;
        }
    }


    //OnTriggerStay2D OnTriggerEnter2D
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("explode"))
        {
            ishit = true;
            direction = (new Vector2(transform.position.x, transform.position.y) - new Vector2(collision.transform.position.x, collision.transform.position.y)).normalized;
            aggressivity = (int)collision.GetComponent<explode>().explode_value;
            speed = collision.GetComponent<explode>().explode_repel;
            TransitionState(StateType.Attack);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("swordqi"))
        {
            /*if (playerController.Instance.player_Info.qiisexploed)
            {
                ExplodePool.Instance.GetFromPooll(transform.position.x, transform.position.y);
            }*/
            ishit = true;
            direction = (new Vector2(transform.position.x, transform.position.y) - new Vector2(collision.transform.position.x, collision.transform.position.y)).normalized;
            aggressivity = (int)playerController.Instance.player_Info.aggressivity_1;
            speed = playerController.Instance.player_Info.repel_1;
                parameter.health -= aggressivity;
                HurtValuePool.Instance.GetFromPooll(transform.position.x, transform.position.y, aggressivity);
                if (parameter.health <= 0)
                {
                    TransitionState(StateType.Hurt);
                }
                else
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * UnityEngine.Random.Range(0.8f, 1f) * speed, direction.y * UnityEngine.Random.Range(0.8f, 1f) * speed);
                    //rigdbody.velocity = new Vector2(attack.Instance.swordPos.x * Random.Range(0.8f, 1f) * manager.speed, attack.Instance.swordPos.y * Random.Range(0.8f, 1f) * manager.speed);
                    TransitionState(StateType.Idle);
                }
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("swort"))
        {
            if (playerController.Instance.player_Info.isexplode)
            {
                ExplodePool.Instance.GetFromPooll(transform.position.x,transform.position.y);
            }
            if ((UnityEngine.Random.Range(0f, 1f) < playerController.Instance.player_Info.bloodsuck && attack.Instance.tag == 2) && playerController.Instance.Hurt_value < playerController.Instance.Health_max)
            {
                playerController.Instance.Hurt_value++;
                playerController.Instance.h_num.text = playerController.Instance.Hurt_value + "";
                playerController.Instance.h_image.GetComponent<Animator>().Play("Health_change");

            }
            ishit = true;
            direction = (new Vector2(transform.position.x, transform.position.y) - new Vector2(collision.transform.position.x, collision.transform.position.y)).normalized;
            aggressivity = attack.Instance.aggressivity;
            speed = attack.Instance.repel;
            TransitionState(StateType.Attack);
        }
       

    }
    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("swort"))
        {
            ishit = false;
        }
    }*/

    public void die()
    {
        EXPPool.Instance.GetFromPooll(transform.position.x, transform.position.y, expvalue);
        EnemyPool.Instance.ReturnPool(this.gameObject);
    }

}
