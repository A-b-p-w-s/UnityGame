using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordqi : MonoBehaviour
{
    [Header("不透明度")]
    private float alpha;
    public float alphaSet;
    public float alphaMultiplier;

    public float ReduceSpeed;
    private Vector2 direction;
    public float speed;

    public float swordqiattack;
    public float repel;
    private float z;
    public float puncture;

    private void OnEnable()
    {
        //swordqiattack = GetComponent<Transform>().localEulerAngles.x;
        //repel = GetComponent<Transform>().localEulerAngles.y;
        GetComponent<CapsuleCollider2D>().enabled = true;
        alpha = alphaSet;
        direction = attack.Instance.swordPos;
        //GetComponent<Transform>().right = direction;
        //z = GetComponent<Transform>().localEulerAngles.z;
        //GetComponent<Transform>().localEulerAngles = new Vector3(swordqiattack, repel,z);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * speed, direction.y * speed);
        speed -= ReduceSpeed;
        alpha *= alphaMultiplier;
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, alpha);
        if (GetComponent<SpriteRenderer>().color.a <= 0.1)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            //返回对象池
            SwordQiPool.Instance.ReturnPool(this.gameObject);
        }
    }

    //OnTriggerStay2D OnTriggerEnter2D

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy") && UnityEngine.Random.Range(0f, 1f) >= puncture)
        {
                GetComponent<CapsuleCollider2D>().enabled = false;
                SwordQiPool.Instance.ReturnPool(this.gameObject);
        }
    }
}
