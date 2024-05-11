using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_PosType
{
    TopLeft,
    Top,
    TopRight,
    Left,
    Right,
    ButtonLeft,
    Button,
    ButtonRight,
}

public class FireObject : MonoBehaviour
{
    public E_PosType type;

    private Vector3 screenPos;

    private Vector3 initDir;

    private FireInfo fireInfo;

    private int nowNum;
    private float nowCd;
    private float nowDelay;
    private BulletInfo nowBulletInfo;
    // 用于记录散弹上一颗的方向
    private float changeAngle;

    private Vector3 nowDir;

    private void Update()
    {
        UpdatePos();
        // 每帧检测是否需要重置开火点
        ResetFireInfo();
        // 发射子弹
        UpdateFire();
    }

    // 将屏幕坐标转世界坐标
    private void UpdatePos()
    {
        // 和玩家同一平面
        screenPos.z = 147.73f;
        switch (type)
        {
            case E_PosType.TopLeft:
                screenPos.x = 0;
                screenPos.y = Screen.height;

                initDir = Vector3.right;
                break;
            case E_PosType.Top:
                screenPos.x = Screen.width / 2;
                screenPos.y = Screen.height;

                initDir = Vector3.right;
                break;
            case E_PosType.TopRight:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height;

                initDir = Vector3.left;
                break;
            case E_PosType.Left:
                screenPos.x = 0;
                screenPos.y = Screen.height / 2;

                initDir = Vector3.right;
                break;
            case E_PosType.Right:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height / 2;

                initDir = Vector3.left;
                break;
            case E_PosType.ButtonLeft:
                screenPos.x = 0;
                screenPos.y = 0;

                initDir = Vector3.right;
                break;
            case E_PosType.Button:
                screenPos.x = Screen.width / 2;
                screenPos.y = 0;

                initDir = Vector3.right;
                break;
            case E_PosType.ButtonRight:
                screenPos.x = Screen.width;
                screenPos.y = 0;

                initDir = Vector3.left;
                break;
        }

        transform.position = Camera.main.ScreenToWorldPoint(screenPos);
    }

    private void ResetFireInfo()
    {
        // 如果本轮的发射还没完
        if (nowCd != 0 && nowNum != 0) return;

        // 如果上回的发射间隔还么结束
        if (fireInfo != null)
        {
            nowDelay -= Time.deltaTime;
            if (nowDelay > 0)
                return;
        }

        List<FireInfo> list = GameDataMgr.Instance.fireData.fireInfos;
        fireInfo = list[Random.Range(0, list.Count)];
        nowNum = fireInfo.num;
        nowCd = fireInfo.cd;
        nowDelay = fireInfo.delay;

        string[] strs = fireInfo.ids.Split(',');
        int beginId = int.Parse(strs[0]);
        int endId = int.Parse(strs[1]);

        int randomBulletId = Random.Range(beginId, endId + 1);
        nowBulletInfo = GameDataMgr.Instance.bulletData.bulletInfos[randomBulletId - 1];

        // 如果是散弹就要计算角度, 四个拐角是90, 其余180
        if (fireInfo.type == E_FireType.sandan)
        {
            switch (type)
            {
                case E_PosType.TopLeft:
                case E_PosType.TopRight:
                case E_PosType.ButtonLeft:
                case E_PosType.ButtonRight:
                    changeAngle = 90 / (nowNum + 1);
                    break;
                case E_PosType.Top:
                case E_PosType.Left:
                case E_PosType.Right:
                case E_PosType.Button:
                    changeAngle = 180f / (nowNum + 1);
                    break;
            }
        }
    }

    private void UpdateFire()
    {
        if (nowNum == 0) return;

        // 每次要发射前的cd检测
        nowCd -= Time.deltaTime;
        if (nowCd > 0) return;

        GameObject obj;
        BulletObject bulletObj;

        switch (fireInfo.type)
        {
            case E_FireType.shunxv:
                // 创建子弹并让其移动
                obj = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resPath));
                bulletObj = obj.AddComponent<BulletObject>();
                bulletObj.InitInfo(nowBulletInfo);

                // 设置子弹位置和朝向
                obj.transform.position = transform.position;
                obj.transform.rotation = Quaternion.LookRotation(PlayerObject.Instance.transform.position - transform.position);
                nowNum--;
                // 每颗子弹之间的cd
                nowCd = nowNum == 0 ? 0 : fireInfo.cd;
                break;
            case E_FireType.sandan:
                if (nowCd == 0)
                {
                    for (int i = 0; i < nowNum; i++)
                    {
                        // 创建子弹并让其移动
                        obj = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resPath));
                        bulletObj = obj.AddComponent<BulletObject>();
                        bulletObj.InitInfo(nowBulletInfo);

                        // 设置子弹位置和朝向
                        obj.transform.position = transform.position;
                        //obj.transform.rotation = 
                        nowDir = Quaternion.AngleAxis(changeAngle * i, Vector3.up) * initDir;
                    }
                    // 记得重置cd和数量
                    nowCd = nowNum = 0;
                }
                else
                {
                    // 创建子弹并让其移动
                    obj = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resPath));
                    bulletObj = obj.AddComponent<BulletObject>();
                    bulletObj.InitInfo(nowBulletInfo);

                    // 设置子弹位置和朝向
                    obj.transform.position = transform.position;
                    //obj.transform.rotation = 
                    nowDir = Quaternion.AngleAxis(changeAngle * (fireInfo.num - nowNum), Vector3.up) * initDir;
                    nowNum--;
                    nowCd = nowNum == 0 ? 0 : fireInfo.cd;
                }
                break;
        }
    }
}