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
    // ���ڼ�¼ɢ����һ�ŵķ���
    private float changeAngle;

    private Vector3 nowDir;

    private void Update()
    {
        UpdatePos();
        // ÿ֡����Ƿ���Ҫ���ÿ����
        ResetFireInfo();
        // �����ӵ�
        UpdateFire();
    }

    // ����Ļ����ת��������
    private void UpdatePos()
    {
        // �����ͬһƽ��
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
        // ������ֵķ��仹û��
        if (nowCd != 0 && nowNum != 0) return;

        // ����ϻصķ�������ô����
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

        // �����ɢ����Ҫ����Ƕ�, �ĸ��ս���90, ����180
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

        // ÿ��Ҫ����ǰ��cd���
        nowCd -= Time.deltaTime;
        if (nowCd > 0) return;

        GameObject obj;
        BulletObject bulletObj;

        switch (fireInfo.type)
        {
            case E_FireType.shunxv:
                // �����ӵ��������ƶ�
                obj = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resPath));
                bulletObj = obj.AddComponent<BulletObject>();
                bulletObj.InitInfo(nowBulletInfo);

                // �����ӵ�λ�úͳ���
                obj.transform.position = transform.position;
                obj.transform.rotation = Quaternion.LookRotation(PlayerObject.Instance.transform.position - transform.position);
                nowNum--;
                // ÿ���ӵ�֮���cd
                nowCd = nowNum == 0 ? 0 : fireInfo.cd;
                break;
            case E_FireType.sandan:
                if (nowCd == 0)
                {
                    for (int i = 0; i < nowNum; i++)
                    {
                        // �����ӵ��������ƶ�
                        obj = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resPath));
                        bulletObj = obj.AddComponent<BulletObject>();
                        bulletObj.InitInfo(nowBulletInfo);

                        // �����ӵ�λ�úͳ���
                        obj.transform.position = transform.position;
                        //obj.transform.rotation = 
                        nowDir = Quaternion.AngleAxis(changeAngle * i, Vector3.up) * initDir;
                    }
                    // �ǵ�����cd������
                    nowCd = nowNum = 0;
                }
                else
                {
                    // �����ӵ��������ƶ�
                    obj = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resPath));
                    bulletObj = obj.AddComponent<BulletObject>();
                    bulletObj.InitInfo(nowBulletInfo);

                    // �����ӵ�λ�úͳ���
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