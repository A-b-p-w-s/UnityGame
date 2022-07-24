using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public float interval;
    public Transform muzzlePos;
    public Transform shellPos;
    private Vector2 mouselePos;
    public Vector2 direction;
    public int tag;
    public float repel;
    public int aggressivity;
    public GameObject muzzle;
    public GameObject bulletshell;

    private static Pistol instance;
    public static Pistol Instance
    {
        get
        {
            if (instance == null)
                instance = Transform.FindObjectOfType<Pistol>();
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        muzzlePos = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        mouselePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mouselePos.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, -1, 1);
            transform.parent.GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
        }
        if (mouselePos.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.parent.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        Shoot();
    }
    public void Shoot()
    {
        direction = (mouselePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right = direction;
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
        {
            if (playerController.Instance.player_Info.BulletsNum > 0)
            {
                if (playerController.Instance.player_Info.MT)
                {
                    if (Input.GetMouseButtonDown(0) && !playerController.Instance.isReload)
                    {
                        GetComponent<Animator>().SetTrigger("Shoot");
                        GetComponent<Animator>().speed = playerController.Instance.player_Info.attack_speed_1;
                        tag = 1;
                    }
                    else
                        playerController.Instance.isattack = false;
                }
                else
                {
                    if (Input.GetMouseButton(0) && !playerController.Instance.isReload)
                    {
                        GetComponent<Animator>().SetTrigger("Shoot");
                        GetComponent<Animator>().speed = playerController.Instance.player_Info.attack_speed_1;                     
                        tag = 1;
                    }
                    else
                        playerController.Instance.isattack = false;
                }
                
                /*else if (Input.GetMouseButton(1))
                {
                    GetComponent<Animator>().SetTrigger("Shoot");
                    GetComponent<Animator>().speed = playerController.Instance.player_Info.attack_speed_2;
                    repel = (playerController.Instance.player_Info.repel_2);
                    aggressivity = playerController.Instance.player_Info.aggressivity_2;
                    tag = 2;
                }*/
            }

        }
    }

    public float angel=15;

    public void Shooting()
    {
        for (int i =1; i <= playerController.Instance.player_Info.ShootNum && playerController.Instance.player_Info.BulletsNum>0; i++)
        {
            playerController.Instance.isattack = true;
            angel *= -1;
            playerController.Instance.player_Info.BulletsNum--;
            BullePool.Instance.BulleFromPool(muzzle.transform, Quaternion.AngleAxis(angel*(i/2),Vector3.forward)*direction, playerController.Instance.player_Info.BulletsSpeed);
            BullePool.Instance.BulleShellFromPool(bulletshell.transform);
        }
    }
            
}

