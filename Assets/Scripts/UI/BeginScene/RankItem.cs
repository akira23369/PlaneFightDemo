using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankItem : MonoBehaviour
{
    public UILabel labRank;
    public UILabel labName;
    public UILabel labTime;

    public void InitInfo(int rank, string name, int time)
    {
        labRank.text = rank.ToString();
        labName.text = name;

        string str = "";
        int h = time / 3600;
        int m = (time % 3600) / 60;
        int s = time % 60;

        if (h > 0) str += $"{h}h";
        if (m > 0 || str != "") str += $"{m}m";
        str += $"{s}s";
        labTime.text = str;
    }
}
