using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : BasePanel<GameOverPanel>
{
    public UILabel lbScore;
    public UIButton btnYes;
    public UIInput inputName;

    public int endTime;
    public override void Init()
    {
        btnYes.onClick.Add(new EventDelegate(() =>
        {
            // ����ҳɼ����浽���а�
            GameDataMgr.Instance.AddRankItem(inputName.value, endTime);
            SceneManager.LoadScene("BeginScene");
        }));
        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        endTime = (int)GamePanel.Instance.nowTime;
        lbScore.text = GamePanel.Instance.lbTime.text;

    }
}
