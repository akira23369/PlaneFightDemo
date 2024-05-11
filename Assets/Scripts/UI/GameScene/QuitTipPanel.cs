using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitTipPanel : BasePanel<QuitTipPanel>
{
    public UIButton btnSure;
    public UIButton btnNo;
    public override void Init()
    {
        btnSure.onClick.Add(new EventDelegate(() =>
        {
            SceneManager.LoadScene("BeginScene");
        }));
        btnNo.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
        }));
        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
    }

    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }
}
