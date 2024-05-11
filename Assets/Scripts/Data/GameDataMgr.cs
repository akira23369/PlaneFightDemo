using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    private static GameDataMgr _instance = new GameDataMgr();
    public static GameDataMgr Instance => _instance;
    private GameDataMgr()
    {
        musicData = XmlDataMgr.Instance.LoadData(typeof(MusicData), "MusicData") as MusicData;
        rankData = XmlDataMgr.Instance.LoadData(typeof(RankData), "RankData") as RankData;
        roleData = XmlDataMgr.Instance.LoadData(typeof(RoleData), "RoleData") as RoleData;
        bulletData = XmlDataMgr.Instance.LoadData(typeof(BulletData), "BulletData") as BulletData;
        fireData = XmlDataMgr.Instance.LoadData(typeof(FireData), "FireData") as FireData;
    }
    public MusicData musicData;

    public RankData rankData;
    // 角色数据
    public RoleData roleData;

    public BulletData bulletData;

    // 开火点数据
    public FireData fireData;

    public int nowSelHeroIndex = 0;

    public RoleInfo GetNowSelHeroInfo() => roleData.roleList[nowSelHeroIndex];
    public void SaveMusicData() => XmlDataMgr.Instance.SaveData(musicData, "MusicData");
    public void SetIsOpenMusic(bool isopen)
    {
        musicData.isOpenMusic = isopen;
        BKMusic.Instance.SetBKMusicIsOpen(isopen);
    }
    public void SetIsOpenSound(bool isopen)
    {
        musicData.isOpenSound = isopen;
    }

    public void SetSoundValue(float value)
    {
        musicData.soundValue = value;
    }

    public void SetMusicValue(float value)
    {
        musicData.musicValue = value;
        BKMusic.Instance.SetBKMusicValue(value);
    }

    public void AddRankItem(string name, int time)
    {
        RankInfo t = new RankInfo();
        t.name = name;
        t.time = time;
        rankData.rankList.Add(t);

        rankData.rankList.Sort((a, b) => a.time - b.time);
        
        // 移除后20
        if (rankData.rankList.Count > 20)
        {
            rankData.rankList.RemoveAt(20);
        }

        XmlDataMgr.Instance.SaveData(rankData, "RankData");
    }
}
