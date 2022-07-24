using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public Vector2 direction;
    public float speed;
    public float live_time;
    // Start is called before the first frame update
    private void OnEnable()
    {
        live_time = playerController.Instance.player_Info.blive_time;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        live_time -= Time.deltaTime;
        if (live_time <= 0)
        {
            BullePool.Instance.ReturnPool1(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy") && UnityEngine.Random.Range(0f, 1f) >= 0)
        {
            BullePool.Instance.ReturnPool1(this.gameObject);
        }
    }
}
