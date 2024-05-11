using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPanel : BasePanel<BeginPanel>
{
    public UIButton btnBegin;
    public UIButton btnRank;
    public UIButton btnSetting;
    public UIButton btnQuit;
    public override void Init()
    {
        btnBegin.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
            ChoosePanel.Instance.ShowMe();
        }));
        btnRank.onClick.Add(new EventDelegate(() =>
        {
            RankPanel.Instance.ShowMe();
        }));
        btnSetting.onClick.Add(new EventDelegate(() =>
        {
            SettingPanel.Instance.ShowMe();
        }));
        btnQuit.onClick.Add(new EventDelegate(() =>
        {
            Application.Quit();
        }));
    }

}
