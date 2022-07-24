using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtValue : MonoBehaviour
{
    private TextMesh thisText;
    private Color color;
    private float x;
    private float y;

    [Header("不透明度")]
    private float alpha;
    public float alphaSet;
    public float alphaMultiplier;

    private void OnEnable()
    {
        x = GetComponent<Transform>().position.x + Random.Range(-0.5f,0.5f);
        y= GetComponent<Transform>().position.y + Random.Range(-0.5f, 0.5f);
        GetComponent<TextMesh>().color = new Color(1, 1, 1, alphaSet);
        alpha = alphaSet;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().position= Vector3.Lerp(GetComponent<Transform>().position, new Vector3(x,y,1),0.1f);
        alpha *= alphaMultiplier;
        color = new Color(1, 1, 1, alpha);
        GetComponent<TextMesh>().color = color;
        if (GetComponent<TextMesh>().color.a<=0.1)
        {
            //返回对象池
            HurtValuePool.Instance.ReturnPool(this.gameObject);
        }
    }
}
