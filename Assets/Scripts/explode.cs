using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour
{
    public int explode_value;
    public float explode_repel;
    // Start is called before the first frame update
    void Start()
    {
        explode_value = ((int)playerController.Instance.player_Info.aggressivity_1 + playerController.Instance.player_Info.aggressivity_2) / 2;
        explode_repel = (playerController.Instance.player_Info.repel_1 + playerController.Instance.player_Info.repel_2) / 2;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void RePool()
    {
        ExplodePool.Instance.ReturnPool(this.gameObject);
    }
}
