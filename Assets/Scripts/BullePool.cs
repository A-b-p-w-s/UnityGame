using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullePool : MonoBehaviour
{

    private static BullePool instance;
    public static BullePool Instance
    {
        get
        {
            if (instance == null)
                instance = Transform.FindObjectOfType<BullePool>();
            return instance;
        }
    }
    public GameObject shadowPrefab;
    public GameObject BulletsShell;

    public int shadowCount;

    private Queue<GameObject> availableObjects1 = new Queue<GameObject>();
    private Queue<GameObject> availableObjects2 = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;
        //初始对象池
        FillPool1();
        FillPool2();
    }
    public void FillPool1()
    {
        for (int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);
            //取消启用,返回对象池
            ReturnPool1(newShadow);
        }

    }
    public void FillPool2()
    {
        for (int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(BulletsShell);
            newShadow.transform.SetParent(transform);
            //取消启用,返回对象池
            ReturnPool2(newShadow);
        }

    }

    public void ReturnPool1(GameObject gameObject)
    {
        gameObject.SetActive(false);

        availableObjects1.Enqueue(gameObject);
    }
    public void ReturnPool2(GameObject gameObject)
    {
        gameObject.SetActive(false);

        availableObjects2.Enqueue(gameObject);
    }

    public GameObject BulleFromPool(Transform tf,Vector2 direction,float speed)
    {
        if (availableObjects1.Count == 0)
        {
            FillPool1();
        }
        var outShadow = availableObjects1.Dequeue();
        outShadow.GetComponent < Transform >().position= new Vector3(tf.position.x,tf.position.y,-0.5f);
        outShadow.GetComponent<Bullets>().direction = direction;
        outShadow.GetComponent<Bullets>().speed = speed;
        outShadow.SetActive(true);
        return outShadow;
    }
    public GameObject BulleShellFromPool(Transform tf)
    {
        if (availableObjects2.Count == 0)
        {
            FillPool2();
        }
        var outShadow = availableObjects2.Dequeue();
        outShadow.GetComponent<Transform>().position = tf.position;
        outShadow.SetActive(true);
        return outShadow;
    }
}
