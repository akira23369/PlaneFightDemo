using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Awake()
    {
        // 根据开始选择的飞机来动态创建
        RoleInfo info = GameDataMgr.Instance.GetNowSelHeroInfo();
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.resPath));
        PlayerObject playerObj = obj.AddComponent<PlayerObject>();
        playerObj.moveSpeed = info.speed * 20;
        playerObj.maxhp = 10;
        playerObj.nowhp = info.hp;
        playerObj.roundSpeed = 20;

        obj.layer = 0;
        obj.tag = "Player";
        GamePanel.Instance.ChangeHp(info.hp);
    }
}
