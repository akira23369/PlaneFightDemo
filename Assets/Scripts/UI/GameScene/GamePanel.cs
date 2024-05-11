using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel<GamePanel>
{
    public UIButton btnBack;
    public UILabel lbTime;
    public List<GameObject> hpList;
    public float nowTime = 0;
    public override void Init()
    {
        // ¹ØÁªhpList
        for (int i = 1; i <= 10; i++)
        {
            GameObject fobj = GameObject.Find("Sp" + i.ToString());
            Transform s = fobj?.transform.GetChild(0);
            hpList.Add(s?.gameObject);
        }

        btnBack.onClick.Add(new EventDelegate(() =>
        {
            QuitTipPanel.Instance.ShowMe();
            //Time.timeScale = 0;
        }));


    }

    public void ChangeHp(int hp)
    {
        for (int i = 0; i < hpList.Count; i++)
        {
            hpList[i].SetActive(i < hp);
        }
    }

    private void Update()
    {
        nowTime += Time.deltaTime;

        string str = "";
        int h = (int)nowTime / 3600;
        int m = (int)(nowTime % 3600) / 60;
        int s = (int)nowTime % 60;

        if (h > 0) str += $"{h}h";
        if (m > 0 || str != "") str += $"{m}m";
        str += $"{s}s";
        lbTime.text = str;
    }
}
