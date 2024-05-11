using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : BasePanel<SettingPanel>
{
    public UIButton btnClose;
    public UIToggle togMusic;
    public UIToggle togSound;

    public UISlider sliderMusic;
    public UISlider sliderSound;
    public override void Init()
    {
        btnClose.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
        }));
        sliderMusic.onChange.Add(new EventDelegate(() =>
        {
            // �ı����ִ�С
            GameDataMgr.Instance.SetMusicValue(sliderMusic.value);
        }));
        sliderSound.onChange.Add(new EventDelegate(() =>
        {
            // �ı���Ч��С
            GameDataMgr.Instance.SetSoundValue(sliderSound.value);

        }));
        togMusic.onChange.Add(new EventDelegate(() =>
        {
            // ���ֿ���
            GameDataMgr.Instance.SetIsOpenMusic(togMusic.value);
        }));
        togSound.onChange.Add(new EventDelegate(() =>
        {
            // ��Ч����
            GameDataMgr.Instance.SetIsOpenSound(togSound.value);
        }));
        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        // ÿ����ʾʱ, ���ϴε�����������
        MusicData data = GameDataMgr.Instance.musicData;
        togMusic.value = data.isOpenMusic;
        togSound.value = data.isOpenSound;
        sliderMusic.value = data.musicValue;
        sliderSound.value = data.soundValue;
    }

    public override void HideMe()
    {
        base.HideMe();
        GameDataMgr.Instance.SaveMusicData();
    }
}
