using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select : MonoBehaviour
{
    // Start is called before the first frame update
     public void select1()
    {
        playerController.Instance.player_Info.ReloadMoveSpeed = 2;
        playerController.Instance.player_Info.MT = true;
        playerController.Instance.guns[0].SetActive(true);
        playerController.Instance.Health_max = 4;
        playerController.Instance.player_Info.speed = 6;
        playerController.Instance.player_Info.aggressivity_1 = 10;
        playerController.Instance.player_Info.attack_speed_1 = 1f;
        playerController.Instance.player_Info.repel_1 = 5;
        playerController.Instance.player_Info.BulletsSpeed = 10;
        playerController.Instance.player_Info.BulletsNumMAX = 8;
        playerController.Instance.player_Info.blive_time = 1;
        playerController.Instance.player_Info.ShootNum = 1;
        playerController.Instance.player_Info.Reload = 1;
        playerController.Instance.player_Info.LV = 0;
        transform.gameObject.SetActive(false);
        playerController.Instance.Play_Start();
        playerController.Instance.GAMESTART();
    }
    public void select2()
    {
        playerController.Instance.player_Info.ReloadMoveSpeed = 2f;
        playerController.Instance.player_Info.MT = false;
        playerController.Instance.guns[1].SetActive(true);
        playerController.Instance.Health_max = 6;
        playerController.Instance.player_Info.speed = 5;
        playerController.Instance.player_Info.aggressivity_1 = 7;
        playerController.Instance.player_Info.attack_speed_1 = 1.25f;
        playerController.Instance.player_Info.repel_1 = 3;
        playerController.Instance.player_Info.BulletsSpeed = 12;
        playerController.Instance.player_Info.BulletsNumMAX = 30;
        playerController.Instance.player_Info.blive_time = 1;
        playerController.Instance.player_Info.ShootNum = 1;
        playerController.Instance.player_Info.Reload = 2;
        playerController.Instance.player_Info.LV = 0;
        transform.gameObject.SetActive(false);
        playerController.Instance.Play_Start();
        playerController.Instance.GAMESTART();
    }
    public void select3()
    {
        playerController.Instance.player_Info.ReloadMoveSpeed = 2f;
        playerController.Instance.player_Info.MT = true;
        playerController.Instance.guns[2].SetActive(true);
        playerController.Instance.Health_max = 8;
        playerController.Instance.player_Info.speed = 5;
        playerController.Instance.player_Info.aggressivity_1 = 10;
        playerController.Instance.player_Info.attack_speed_1 = 1f;
        playerController.Instance.player_Info.repel_1 = 7;
        playerController.Instance.player_Info.BulletsSpeed = 8;
        playerController.Instance.player_Info.BulletsNumMAX = 9;
        playerController.Instance.player_Info.blive_time = 1;
        playerController.Instance.player_Info.ShootNum = 3;
        playerController.Instance.player_Info.Reload = 2;
        playerController.Instance.player_Info.LV = 0;
        transform.gameObject.SetActive(false);
        playerController.Instance.Play_Start();
        playerController.Instance.GAMESTART();
    }
}
