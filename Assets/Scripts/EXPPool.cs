using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPPool : MonoBehaviour
{
    private static EXPPool instance;
    public static EXPPool Instance
    {
        get
        {
            if (instance == null)
                instance = Transform.FindObjectOfType<EXPPool>();
            return instance;
        }
    }
    public GameObject shadowPrefab;

    public int shadowCount;

    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;
        //初始对象池
        FillPool();
    }
    public void FillPool()
    {
        for (int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);
            //取消启用,返回对象池
            ReturnPool(newShadow);
        }

    }

    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);

        availableObjects.Enqueue(gameObject);
    }

    public GameObject GetFromPooll(float x,float y,float exvalue)
    {
        if (availableObjects.Count == 0)
        {
            FillPool();
        }
        var outShadow = availableObjects.Dequeue();
        outShadow.GetComponent<Transform>().localEulerAngles = new Vector3(0,0,exvalue);
        outShadow.transform.position = new Vector3(x, y, 0);
        outShadow.SetActive(true);
        return outShadow;
    }
}
