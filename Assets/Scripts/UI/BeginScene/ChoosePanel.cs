using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePanel : BasePanel<ChoosePanel>
{
    public UIButton btnClose;
    public UIButton btnStart;
    public UIButton btnLeft;
    public UIButton btnRight;

    public Transform heroPos;

    public List<GameObject> hpList;
    public List<GameObject> speedList;
    public List<GameObject> volumeList;

    private GameObject airPlaneObj;

    public Camera uiCamera;
    public override void Init()
    {
        btnStart.onClick.Add(new EventDelegate(() =>
        {
            SceneManager.LoadScene("GameScene");
        }));
        btnLeft.onClick.Add(new EventDelegate(() =>
        {
            int x = GameDataMgr.Instance.nowSelHeroIndex;
            int ct = GameDataMgr.Instance.roleData.roleList.Count;
            x = (x - 1 + ct) % ct;
            GameDataMgr.Instance.nowSelHeroIndex = x;
            UpdateSelHero();
        }));
        btnRight.onClick.Add(new EventDelegate(() =>
        {
            int x = GameDataMgr.Instance.nowSelHeroIndex;
            x = (x + 1) % GameDataMgr.Instance.roleData.roleList.Count;
            GameDataMgr.Instance.nowSelHeroIndex = x;
            UpdateSelHero();
        }));
        btnClose.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
            BeginPanel.Instance.ShowMe();
        }));
        HideMe();
    }

    public override void HideMe()
    {
        DestroyObj();
        base.HideMe();
    }

    public override void ShowMe()
    {
        GameDataMgr.Instance.nowSelHeroIndex = 0;
        UpdateSelHero();
        base.ShowMe();
    }

    // 更新当前的英雄
    public void UpdateSelHero()
    {
        RoleInfo info = GameDataMgr.Instance.GetNowSelHeroInfo();
        DestroyObj();
        airPlaneObj = Instantiate(Resources.Load<GameObject>(info.resPath));
        airPlaneObj.transform.SetParent(heroPos, false);
        airPlaneObj.transform.localPosition = Vector3.zero;
        airPlaneObj.transform.localRotation = Quaternion.identity;
        airPlaneObj.transform.localScale = Vector3.one * info.scale;

        for (int i = 0; i < 10; i++)
        {
            hpList[i].SetActive(i < info.hp);
            speedList[i].SetActive(i < info.speed);
            volumeList[i].SetActive(i < info.volume);
        }
    }

    public void DestroyObj()
    {
        if (airPlaneObj != null)
        {
            Destroy(airPlaneObj);
            airPlaneObj = null;
        }
    }

    private float time = 0;
    private bool isSel = false;
    void Update()
    {
        time += Time.deltaTime;
        airPlaneObj.transform.Translate(Vector3.up * Mathf.Sin(time) * 0.0003f, Space.World);

        // 如果鼠标点击了
        if (Input.GetMouseButtonDown(0))
        {
            // 并且点到了场上UI的碰撞器
            if (Physics.Raycast(uiCamera.ScreenPointToRay(Input.mousePosition),
                1000, 1 << LayerMask.NameToLayer("UI")))
            {
                isSel = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
            isSel = false;

        if (Input.GetMouseButton(0) && isSel)
        {
            // 处理输入旋转
            heroPos.rotation *= Quaternion.AngleAxis(10 * Input.GetAxis("Mouse X"), Vector3.up);
        }
    }
}
