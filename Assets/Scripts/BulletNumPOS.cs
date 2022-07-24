using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNumPOS : MonoBehaviour
{
    private Vector2 mouselePos;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMesh>().text = playerController.Instance.player_Info.BulletsNum + "";
        mouselePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetComponent<Transform>().position = new Vector2(mouselePos.x, mouselePos.y);
    }

    // Update is called once per frame
    /*void Update()
    {
        GetComponent<TextMesh>().text = playerController.Instance.player_Info.BulletsNum+"";
        mouselePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetComponent<Transform>().position = new Vector2(mouselePos.x,mouselePos.y);
    }*/
    private void FixedUpdate()
    {
        if (playerController.Instance.player_Info.BulletsNum <= 0)
        {
            GetComponent<TextMesh>().color = new Color(1, 0, 0, 1);
        }
        else
        {
            GetComponent<TextMesh>().color = new Color(1, 1, 1, 1);
        }
        playerController.Instance.BulletsNumText.text= playerController.Instance.player_Info.BulletsNumMAX + "/" + playerController.Instance.player_Info.BulletsNum;
        GetComponent<TextMesh>().text = playerController.Instance.player_Info.BulletsNum + "";
        mouselePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetComponent<Transform>().position = new Vector2(mouselePos.x, mouselePos.y);
    }
}
