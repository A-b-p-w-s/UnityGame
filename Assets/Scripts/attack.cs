using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isAttack;
    private Vector2 mouselePos;
    public Vector2 swordPos;
    public int aggressivity;
    public float repel;
    public float flipY;
    public int tag;

    private static attack instance;
    public static attack Instance
    {
        get
        {
            if (instance == null)
                instance = Transform.FindObjectOfType<attack>();
            return instance;
        }
    }

    public void setattack(bool isAttack)
    {
        this.isAttack = isAttack;
    }
    public bool getisAttack()
    {
        return isAttack;
    }

    void Start()
    {
        flipY = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        mouselePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(mouselePos.x < transform.position.x)
        {
            transform.localScale = new Vector3(-flipY, -flipY, 1);
            transform.parent.GetComponent<Transform>().localScale = new Vector3(-1, 1,1);
        }
        if (mouselePos.x > transform.position.x)
        {
            transform.localScale = new Vector3(flipY, flipY, 1);
            transform.parent.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
            cut();
    }

    public bool isattack;
    void cut()
    {
        if (!isattack)
        {
            swordPos = (mouselePos - new Vector2(transform.position.x, transform.position.y)).normalized;
            transform.right = swordPos;
        }
        //Debug.Log(swordPos);
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
        {
            if (Input.GetMouseButton(0) && !isAttack)
            {
                isAttack = true;
                GetComponent<Animator>().SetInteger("Combosteo", 1);
                GetComponent<Animator>().speed = playerController.Instance.player_Info.attack_speed_1;
                repel =(playerController.Instance.player_Info.repel_1);
                //aggressivity = playerController.Instance.player_Info.aggressivity_1;
                tag = 1;
                
            }
            else if (Input.GetMouseButton(1) && !isAttack)
            {
                isAttack = true;
                GetComponent<Animator>().SetInteger("Combosteo", 2);
                GetComponent<Animator>().speed = playerController.Instance.player_Info.attack_speed_2;
                repel = (playerController.Instance.player_Info.repel_2);
                aggressivity = playerController.Instance.player_Info.aggressivity_2;
                tag = 2;
            }
        }
        else
        {
            isAttack = false;
            GetComponent<Animator>().SetInteger("Combosteo", 0);
        }
        
    }

    public void startattack()
    {
        isattack = true;
    }
    public void endattack()
    {
        isattack = false;
    }

    public void swordqi1()
    {
        if (playerController.Instance.player_Info.SwordQiSpeed1 > 0)
        {
            SwordQiPool.Instance.GetFromPooll(transform.position.x, transform.position.y, playerController.Instance.player_Info.SwordQiSpeed1, new Color(1, 1, 1, 1), playerController.Instance.player_Info.aggressivity_1 * playerController.Instance.player_Info.SwordQi_aggressivity_1, playerController.Instance.player_Info.SwordQi_repel_1*playerController.Instance.player_Info.repel_1);
        }
    }
    public void swordqi2()
    {
        if (playerController.Instance.player_Info.SwordQiSpeed2 > 0)
        {
            SwordQiPool.Instance.GetFromPooll(transform.position.x, transform.position.y, playerController.Instance.player_Info.SwordQiSpeed2, new Color(1, 0, 0, 1), playerController.Instance.player_Info.aggressivity_2 * playerController.Instance.player_Info.SwordQi_aggressivity_2, playerController.Instance.player_Info.SwordQi_repel_2* playerController.Instance.player_Info.repel_2);
        }
    }

    public void AttackOver()
    {
        isAttack = false;
    }

    //private void OnTriggerStay2D(Collider2D collision) OnTriggerEnter2D
   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
                collision.GetComponent<Enemy>().Gethit(transform);
        }
    }
   */
}
