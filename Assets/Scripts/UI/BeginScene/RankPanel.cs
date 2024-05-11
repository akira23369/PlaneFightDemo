using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    public UIButton btnClose;
    public UIScrollView scrollView;
    private List<RankItem> lbItemList = new List<RankItem>();

    public override void Init()
    {
        for (int i = 0; i < 10; i++)
        {
            GameDataMgr.Instance.AddRankItem("hello", 123);
        }
        HideMe();
        btnClose.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
        }));

    }

    public override void ShowMe()
    {

        Vector3 tmp = new Vector3(0, -64, 0);
        List<RankInfo> rankData = GameDataMgr.Instance.rankData.rankList;
        for (int i = 0; i < rankData.Count; i++)
        {
            if (i < lbItemList.Count)
            {
                lbItemList[i].InitInfo(i + 1, rankData[i].name, rankData[i].time);
            }
            else
            {
                
                GameObject obj = Instantiate(Resources.Load<GameObject>("UI/SpRankItem"));
                obj.transform.SetParent(scrollView.transform, false);
                obj.transform.localPosition += i * tmp;
                //obj.GetComponent<UISprite>().SetAnchor(gameObject);

                RankItem it = obj.GetComponent<RankItem>();
                it.InitInfo(i + 1, rankData[i].name, rankData[i].time);
                lbItemList.Add(it);
            }
        }
        base.ShowMe();
    }
}
