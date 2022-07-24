using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP : MonoBehaviour
{
    public float value;
    // Start is called before the first frame update
    private void OnEnable()
    {
        value = GetComponent<Transform>().localEulerAngles.z;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("pickrange"))
        {
            if (Vector3.Distance(GetComponent<Transform>().position, collision.transform.position)<=0.5f)
            {
                playerController.Instance.Exp += value;
                EXPPool.Instance.ReturnPool(this.gameObject);
            }
            else
                GetComponent<Transform>().position = Vector3.Lerp(GetComponent<Transform>().position, collision.transform.position, 0.1f);
        }
    }
}
