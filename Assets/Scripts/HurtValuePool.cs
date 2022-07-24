using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HurtValuePool : MonoBehaviour
{
    private static HurtValuePool instance;
    public static HurtValuePool Instance
    {
        get
        {
            if (instance == null)
                instance = Transform.FindObjectOfType<HurtValuePool>();
            return instance;
        }
    }
    public GameObject Prefab;

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
            var newShadow = Instantiate(Prefab);
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

    public GameObject GetFromPooll(float x,float y,int hurtvalue)
    {
        if (availableObjects.Count == 0)
        {
            FillPool();
        }
        var outShadow = availableObjects.Dequeue();
        outShadow.transform.position = new Vector3(x, y, 1);
        outShadow.GetComponent<TextMesh>().text = hurtvalue+"";
        outShadow.SetActive(true);
        return outShadow;
    }
}
