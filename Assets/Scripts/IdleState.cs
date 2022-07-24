using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;

    private float timer;

    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("slid");
        manager.isattack = false;
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= parameter.idleTime)
        {
            manager.TransitionState(StateType.Patrol);
        }
    }

    public void OnExit()
    {
        timer = 0;
    }
}

public class PatrolState : IState
{
    private FSM manager;
    private Parameter parameter;
    private int patrolPosition;

    public PatrolState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("run");
        manager.isattack = true;
    }

    public void OnUpdate()
    {
       
        manager.FlipTo(GameObject.FindGameObjectWithTag("Player").transform);
        manager.transform.position = Vector2.MoveTowards(manager.transform.position,
        GameObject.FindGameObjectWithTag("Player").transform.position, parameter.moveSpeed * Time.deltaTime);
    }

    public void OnExit()
    {
        patrolPosition++;
        if (patrolPosition >= parameter.patrolPoints.Length)
        {
            patrolPosition = 0;
        }
    }
}


public class ChaseState : IState
{
    private FSM manager;
    private Parameter parameter;

    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {

    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}

public class ReactState : IState
{
    private FSM manager;
    private Parameter parameter;

    public ReactState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {

    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}

public class AttackState : IState
{
    private FSM manager;
    private Parameter parameter;
    private Rigidbody2D rigdbody;
    private bool Invincible_bool;
    private float Hurt_time;

    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {

    }

    public void OnUpdate()
    {
        rigdbody = manager.transform.GetComponent<Rigidbody2D>();
            /*if (Hurt_time + 0.1 <= Time.time)
            {
                Invincible_bool = false;
            }*/

            if (manager.ishit )
            {
                //Hurt_time = Time.time;
                //Invincible_bool = true;
                manager.parameter.health -= manager.aggressivity;
                HurtValuePool.Instance.GetFromPooll(manager.transform.position.x, manager.transform.position.y, manager.aggressivity);
                if (manager.parameter.health <= 0)
                {
                    manager.TransitionState(StateType.Hurt);
                }
                else
                {
                    rigdbody.velocity = new Vector2(manager.direction.x * Random.Range(0.8f, 1f) * manager.speed, manager.direction.y * Random.Range(0.8f, 1f) * manager.speed);
                //rigdbody.velocity = new Vector2(attack.Instance.swordPos.x * Random.Range(0.8f, 1f) * manager.speed, attack.Instance.swordPos.y * Random.Range(0.8f, 1f) * manager.speed);
                manager.TransitionState(StateType.Idle);
                }
            manager.ishit = false;
            }
            else
            {
                 manager.TransitionState(StateType.Patrol);
            }
    }

    public void OnExit()
    {

    }
}
public class HurtState : IState
{
    private FSM manager;
    private Parameter parameter;
    public HurtState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        manager.GetComponent<CapsuleCollider2D>().enabled = (false);
        //EXPPool.Instance.GetFromPooll(manager.transform.position.x ,manager.transform.position.y,manager.expvalue);
        parameter.animator.Play("die");
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}