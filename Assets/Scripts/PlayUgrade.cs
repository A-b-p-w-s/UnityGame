using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUgrade : MonoBehaviour
{

    private int attack1lv = 0;
    private int attack2lv = 0;
    public float repel1lv = 0;
    public float repel2lv = 0;
    public float attackspeed1lv = 0;
    public float attackspeed2lv = 0;
    public int Healthlv = 0;
    public float shield_maxlv=0;
    public float move_speedlv = 0;
    public float dash_speedlv = 0;
    public float dash_cdlv = 0;
    public float health_add_cdlv = 0;
    public float shield_cdlv = 0;
    public float pickrangelv = 0;
    public float sword_sizelv = 0;
    public float bloodsucklv = 0;
    public float swordqispeed1lv = 0;
    public float swordqispeed2lv = 0;
    public float swortpuncturelv = 0;
    public int clock=0;

    private int k=-1;
    private List<float> list = new List<float>();
    private List<float> new_list = new List<float>();
    private float HPlv=0f;

    private void OnEnable()
    {
        list.Sort();
        if(list[list.Count - 1] > 0)
        {
            transform.GetChild(1).GetChild(new_list.IndexOf(list[list.Count - 1])).gameObject.SetActive(true);
            transform.GetChild(1).GetChild(new_list.IndexOf(list[list.Count - 1])).GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 150);
        }
        if(list[list.Count - 2] > 0)
        {
            transform.GetChild(1).GetChild(new_list.IndexOf(list[list.Count - 2])).gameObject.SetActive(true);
            transform.GetChild(1).GetChild(new_list.IndexOf(list[list.Count - 2])).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 150);
        }
        if (list[list.Count - 3] > 0)
        {
            transform.GetChild(1).GetChild(new_list.IndexOf(list[list.Count - 3])).gameObject.SetActive(true);
            transform.GetChild(1).GetChild(new_list.IndexOf(list[list.Count - 3])).GetComponent<RectTransform>().anchoredPosition = new Vector2(100, 150);
        }
    }
    private void Awake()
    {
        float r = UnityEngine.Random.RandomRange(0.7f, 1f);
        list.Add(r - 1 * HPlv);
        new_list.Add(r - 1 * HPlv);
        list.Add(r - 0.1f - (1 * Mathf.Abs(1 - HPlv)));
        new_list.Add(r - 0.1f - (1 * Mathf.Abs(1 - HPlv)));
        list.Add(r - 0.2f - (1 * Mathf.Abs(2 - HPlv)));
        new_list.Add(r - 0.2f - (1 * Mathf.Abs(2 - HPlv)));
    }

    public void select()
    {
        switch (k)
        {           
            case 0:
                HPlv++;
                break;
            default:
                transform.GetChild(0).GetComponent<Text>().text = "<size=45>请选择一个选项升级</size>";
                break;
        }

        if (k >= 0)
        {
            transform.GetChild(1).GetChild(new_list.IndexOf(list[list.Count - 1])).gameObject.SetActive(false);
            transform.GetChild(1).GetChild(new_list.IndexOf(list[list.Count - 2])).gameObject.SetActive(false);
            transform.GetChild(1).GetChild(new_list.IndexOf(list[list.Count - 3])).gameObject.SetActive(false);
            list.Clear();
            new_list.Clear();
            float r = UnityEngine.Random.RandomRange(0.7f, 1f);
            list.Add(r - 1 * HPlv);
            new_list.Add(r - 1 * HPlv);
            list.Add(r - 0.1f - (1 * Mathf.Abs(1 - HPlv)));
            new_list.Add(r - 0.1f - (1 * Mathf.Abs(1 - HPlv)));
            list.Add(r - 0.2f - (1 * Mathf.Abs(2 - HPlv)));
            new_list.Add(r - 0.2f - (1 * Mathf.Abs(2 - HPlv)));
             gameObject.SetActive(false);
            playerController.Instance.Play_Start();
            k = -1;
            transform.GetChild(0).GetComponent<Text>().text = "<size=45>请选择一个选项升级</size>";
        }
        
    }

    public void HP_MAX()
    {
        transform.GetChild(0).GetComponent<Text>().text = "<size=45>最大生命+1</size>";
        if (HPlv == 0)
        {
            k = 0;
        }
    }

    private void Update()
    {
        if (playerController.Instance.player_Info.LV >= 5 )
        {
            transform.GetChild(6).gameObject.SetActive(true);
            transform.GetChild(7).gameObject.SetActive(true);
            transform.GetChild(6).GetChild(1).GetComponent<Text>().text = "最大生命 + " + (Healthlv + 1) + " \nLV：" + Healthlv+"\n 升级需角色等级>=LV"+( 5 * (Healthlv + 1));
        }
        if (shield_maxlv>=1)
        {
            transform.GetChild(12).gameObject.SetActive(true);
            transform.GetChild(7).GetChild(1).GetComponent<Text>().text = "最大护盾 + " + 1 + " \nLV：" + shield_maxlv + "\n 升级需角色等级>=LV"+(5 * (shield_maxlv + 1));
        }
        else if (playerController.Instance.player_Info.LV >= 5)
        {
            transform.GetChild(7).GetChild(1).GetComponent<Text>().text = "最大护盾 + " + 3 + " \nLV：" + shield_maxlv + "\n 升级需角色等级>=LV" + (5 * (shield_maxlv + 1));
        }
        if (move_speedlv>=5 && dash_speedlv==0)
        {
            transform.GetChild(9).gameObject.SetActive(true);
            transform.GetChild(9).GetChild(1).GetComponent<Text>().text = "冲刺距离+ 10% \nLV：" + dash_speedlv;
        }
        if (move_speedlv >= 5 && dash_cdlv==0)
        {
            transform.GetChild(10).gameObject.SetActive(true);
            transform.GetChild(10).GetChild(1).GetComponent<Text>().text = "冲刺CD- 10% \nLV：" + dash_cdlv;
        }
        if(playerController.Instance.player_Info.SwordQiSpeed1 > 0)
        {
            transform.GetChild(18).gameObject.SetActive(true);
            transform.GetChild(16).GetChild(1).GetComponent<Text>().text = "已有剑气";
        }
        if (playerController.Instance.player_Info.SwordQiSpeed2 > 0)
        {

            transform.GetChild(17).GetChild(1).GetComponent<Text>().text = "已有剑气";
            transform.GetChild(19).gameObject.SetActive(true);
        }
        if((playerController.Instance.player_Info.SwordQiSpeed2 > 0 || playerController.Instance.player_Info.SwordQiSpeed1 > 0) && playerController.Instance.player_Info.LV >= 20)
        {
            transform.GetChild(20).gameObject.SetActive(true);

        }
        if (clock==0 &&playerController.Instance.player_Info.LV >= 25 )
        {
            if(!playerController.Instance.player_Info.isexplode)
                transform.GetChild(21).gameObject.SetActive(true);
            if(!playerController.Instance.player_Info.qiisexploed)
                transform.GetChild(22).gameObject.SetActive(true);
        }
    }
    public void up_attack1()
    {
        float a = playerController.Instance.player_Info.aggressivity_1 *1.15f;
        playerController.Instance.player_Info.aggressivity_1 = (int) a;
        attack1lv++;
        transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "左键伤害 + 15% \nLV：" + attack1lv;
        playerController.Instance.Play_Start();
    }
    public void up_attack2()
    {
        float a = playerController.Instance.player_Info.aggressivity_2 * 1.15f;
        playerController.Instance.player_Info.aggressivity_2 = (int)a;
        attack2lv++;
        transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "右键伤害 + 15% \nLV：" + attack2lv;
        playerController.Instance.Play_Start();
    }
    public void up_repel1()
    {
        playerController.Instance.player_Info.repel_1 *= 1.05f;
        repel1lv ++;
        transform.GetChild(2).GetChild(1).GetComponent<Text>().text = "左键击退 + 5% \nLV：" + repel1lv;
        playerController.Instance.Play_Start();
    }
    public void up_repel2()
    {
        playerController.Instance.player_Info.repel_2 *= 1.05f;
        repel2lv++;
        transform.GetChild(3).GetChild(1).GetComponent<Text>().text = "右键击退 + 5% \nLV：" + repel1lv;
        playerController.Instance.Play_Start();
    }
    public void attack_speed1()
    {
        playerController.Instance.player_Info.attack_speed_1 *= 1.1f;
        attackspeed1lv++;
        transform.GetChild(4).GetChild(1).GetComponent<Text>().text = "左键攻速 + 10% \nLV：" + attackspeed1lv;
        playerController.Instance.Play_Start();
    }
    public void attack_speed2()
    {
        playerController.Instance.player_Info.attack_speed_2 *= 1.1f;
        attackspeed2lv++;
        transform.GetChild(5).GetChild(1).GetComponent<Text>().text = "右键攻速 + 10% \nLV：" + attackspeed2lv;
        playerController.Instance.Play_Start();
    }

    public void Health_max()
    {
        if (playerController.Instance.player_Info.LV >=5*(Healthlv+1))
        {
            playerController.Instance.Hurt_value+= (Healthlv + 1);
            playerController.Instance.Health_max+= (Healthlv + 1);
            playerController.Instance.h_full.text = playerController.Instance.Health_max + "";
            playerController.Instance.h_num.text = playerController.Instance.Hurt_value + "";
            Healthlv++;
            gameObject.SetActive(false);
            playerController.Instance.Play_Start();
        } 
    }
    public void shield_max()
    {
        if(playerController.Instance.player_Info.LV >= 5 * (shield_maxlv + 1) && shield_maxlv>=1)
        {
            playerController.Instance.shield_max +=1 ;
            playerController.Instance.shield_value += 1;
            shield_maxlv++;
            gameObject.SetActive(false);
            playerController.Instance.Play_Start();
        }
        else if (playerController.Instance.player_Info.LV >= 5  && shield_maxlv==0)
        {
            playerController.Instance.shield_max =3;
            playerController.Instance.shield_value = 3;
            shield_maxlv++;
            gameObject.SetActive(false);
            playerController.Instance.Play_Start();
        }
    }
    public void move_speed()
    {
        move_speedlv++;
        playerController.Instance.player_Info.speed *= 1.1f;
        transform.GetChild(8).GetChild(1).GetComponent<Text>().text = "角色移速 + 10% \nLV：" + move_speedlv;
        playerController.Instance.Play_Start();
    }

    public void dash_speed()
    {
        if (move_speedlv >= 5)
        {
            dash_speedlv++;
            playerController.Instance.dashSpeed *= 1.1f;
            transform.GetChild(9).GetChild(1).GetComponent<Text>().text = "冲刺距离+ 10% \nLV：" + dash_speedlv;
            gameObject.SetActive(false);
            playerController.Instance.Play_Start();
        }
    }
    public void dash_cd()
    {
        if (move_speedlv >= 5)
        {
            dash_cdlv++;
            playerController.Instance.dashCoolDowm *= 0.9f;
            transform.GetChild(10).GetChild(1).GetComponent<Text>().text = "冲刺CD- 10% \nLV：" + dash_cdlv;
            gameObject.SetActive(false);
            playerController.Instance.Play_Start();
        }
    }
    public void health_add_cd()
    {
        if (health_add_cdlv >0)
        {
            health_add_cdlv++;
            transform.GetChild(11).GetChild(1).GetComponent<Text>().text = "生命恢复速度+ 10% \nLV：" + health_add_cdlv;
            playerController.Instance.player_Info.Health_add_cd *= 0.9f;
            gameObject.SetActive(false);
            playerController.Instance.Play_Start();
        }
        else
        {
            health_add_cdlv++;
            transform.GetChild(11).GetChild(1).GetComponent<Text>().text = "生命恢复速度+ 10% \nLV：" + health_add_cdlv;
            playerController.Instance.player_Info.Health_add_cd = 30;
            gameObject.SetActive(false);
            playerController.Instance.Play_Start();
        }
    }
    public void shield_cd()
    {
        if (shield_maxlv > 0)
        {
            shield_cdlv++;
            transform.GetChild(12).GetChild(1).GetComponent<Text>().text = "护盾CD- 10% \nLV：" + shield_cdlv;
            playerController.Instance.shield_CD *= 0.9f;
            gameObject.SetActive(false);
            playerController.Instance.Play_Start();
        }
    }
    public void pickrange()
    {
        pickrangelv++;
        playerController.Instance.player_Info.PickingRange *= 1.25f;
        transform.GetChild(13).GetChild(1).GetComponent<Text>().text = "拾取范围+25% \nLV：" + pickrangelv;
        gameObject.SetActive(false);
        playerController.Instance.Play_Start();
    }
    public void swordsize()
    {
        sword_sizelv++;
        attack.Instance.flipY *= 1.05f;
        float a = playerController.Instance.player_Info.aggressivity_1 * 1.1f;
        playerController.Instance.player_Info.aggressivity_1 = (int)a;
        a = playerController.Instance.player_Info.aggressivity_2 * 1.1f;
        playerController.Instance.player_Info.aggressivity_2 = (int)a;
        playerController.Instance.player_Info.attack_speed_1 *= 0.95f;
        playerController.Instance.player_Info.attack_speed_2 *= 0.95f;
        playerController.Instance.player_Info.speed *= 0.95f;
        transform.GetChild(14).GetChild(1).GetComponent<Text>().text = "武器巨大化\n伤害+10%&大小+5%\n移速&攻速-5% \nLV：" + sword_sizelv;
        gameObject.SetActive(false);
        playerController.Instance.Play_Start();
    }
    public void bloodsuck()
    {
        bloodsucklv++;
        playerController.Instance.player_Info.bloodsuck *= 1.1f;
        transform.GetChild(15).GetChild(1).GetComponent<Text>().text = "右键吸血概率+10%\nLV：" + bloodsucklv;
        gameObject.SetActive(false);
        playerController.Instance.Play_Start();
    }
    public void swordqi1()
    {
        if (playerController.Instance.player_Info.SwordQiSpeed1<=0)
        {
            playerController.Instance.player_Info.SwordQiSpeed1= 4;
            playerController.Instance.player_Info.SwordQi_aggressivity_1 =0.8f;
            playerController.Instance.player_Info.SwordQi_repel_1 =0.5f;
            gameObject.SetActive(false);
            playerController.Instance.Play_Start();
            playerController.Instance.player_Info.puncture = 0.1f;
        }
    }
    public void swordqi2()
    {
        if (playerController.Instance.player_Info.SwordQiSpeed2 <= 0)
        {
            playerController.Instance.player_Info.SwordQiSpeed2 = 3;
            playerController.Instance.player_Info.SwordQi_aggressivity_2 = 0.8f;
            playerController.Instance.player_Info.SwordQi_repel_2 =0.5f;
            gameObject.SetActive(false);
            playerController.Instance.Play_Start();
            playerController.Instance.player_Info.puncture = 0.1f;
        }
    }
    public void swordqispeed1()
    {
        swordqispeed1lv++;
        playerController.Instance.player_Info.SwordQiSpeed1 *= 1.05f;
        transform.GetChild(18).GetChild(1).GetComponent<Text>().text = "左剑气飞行距离+5%\nLV：" + swordqispeed1lv;
        gameObject.SetActive(false);
        playerController.Instance.Play_Start();
    }
    public void swordqispeed2()
    {
        swordqispeed2lv++;
        playerController.Instance.player_Info.SwordQiSpeed2 *= 1.05f;
        transform.GetChild(19).GetChild(1).GetComponent<Text>().text = "右剑气飞行距离+5%\nLV：" + swordqispeed2lv;
        gameObject.SetActive(false);
        playerController.Instance.Play_Start();
    }

    public void swortpuncture()
    {
        swortpuncturelv++;
        playerController.Instance.player_Info.puncture *= 1.2f;
        transform.GetChild(20).GetChild(1).GetComponent<Text>().text = "剑气穿概率+20%\nLV：" + swortpuncturelv;
        gameObject.SetActive(false);
        playerController.Instance.Play_Start();
    }

    public void sword_ex()
    {
        if (!playerController.Instance.player_Info.isexplode)
        {
            playerController.Instance.player_Info.isexplode = true;
            transform.GetChild(21).GetChild(1).GetComponent<Text>().text = "已近拥有近战产生爆炸!";
            transform.GetChild(22).gameObject.SetActive(false);
            gameObject.SetActive(false);
            playerController.Instance.Play_Start();
            clock++;
        }
    }

    public void qi_ex()
    {
        if (!playerController.Instance.player_Info.qiisexploed)
        {
            playerController.Instance.player_Info.qiisexploed = true;
            transform.GetChild(22).GetChild(1).GetComponent<Text>().text = "已拥有剑气产生爆炸!";
            transform.GetChild(21).gameObject.SetActive(false);
            gameObject.SetActive(false);
            playerController.Instance.Play_Start();
            clock++;
        }
    }
}
