using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsShell : MonoBehaviour
{
    public float speed;
    public float stoptim=.5f;
    public float fadeSppe=.3f;

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0.75f;
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-0.5f,0.5f),Random.Range(.5f,1f))*speed;
        StartCoroutine(Stop());
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(stoptim);
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        while (GetComponentInChildren<SpriteRenderer>().color.a > 0)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, GetComponentInChildren<SpriteRenderer>().color.a - fadeSppe);
            yield return new WaitForFixedUpdate();
        }     
        BullePool.Instance.ReturnPool2(this.gameObject);
    }
}
