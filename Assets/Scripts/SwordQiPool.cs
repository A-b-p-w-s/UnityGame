using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordQiPool : MonoBehaviour
{
    private static SwordQiPool instance;
    public static SwordQiPool Instance
    {
        get
        {
            if (instance == null)
                instance = Transform.FindObjectOfType<SwordQiPool>();
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

    public GameObject GetFromPooll(float x, float y, float speed,Color swordC,float swordqi_attack,float repel)
    {
        if (availableObjects.Count == 0)
        {
            FillPool();
        }
        var outShadow = availableObjects.Dequeue();
        outShadow.GetComponent<Transform>().right = attack.Instance.swordPos;
        float z = outShadow.GetComponent<Transform>().localEulerAngles.z;
        outShadow.GetComponent<swordqi>().puncture = playerController.Instance.player_Info.puncture;
        outShadow.GetComponent<swordqi>().swordqiattack = swordqi_attack;
        outShadow.GetComponent<swordqi>().repel = repel;
        outShadow.GetComponent<Transform>().localEulerAngles = new Vector3(0, 0,z);
        outShadow.GetComponent<swordqi>().speed = speed;
        outShadow.transform.position = new Vector3(x, y, -0.5f);
        outShadow.GetComponent<SpriteRenderer>().color = swordC;
        outShadow.GetComponent<Transform>().localScale=  new Vector2(Mathf.Clamp(Mathf.RoundToInt(outShadow.transform.localScale.x),-1,1)* attack.Instance.flipY, Mathf.Clamp(Mathf.RoundToInt(outShadow.transform.localScale.y), -1, 1) * attack.Instance.flipY);
        outShadow.SetActive(true);
        return outShadow;
    }
}
