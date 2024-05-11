using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic _instance;
    public static BKMusic Instance => _instance;
    public AudioSource bkAudio;

    void Awake()
    {
        print(Application.persistentDataPath);
        _instance = this;
        bkAudio = GetComponent<AudioSource>();

        // 用存储的数据, 第一次初始化是否播放
        SetBKMusicIsOpen(GameDataMgr.Instance.musicData.isOpenMusic);
        SetBKMusicValue(GameDataMgr.Instance.musicData.musicValue);
    }

    public void SetBKMusicIsOpen(bool isopen) => bkAudio.mute = !isopen;

    public void SetBKMusicValue(float value) => bkAudio.volume = value;

}
