using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FireData
{
    public List<FireInfo> fireInfos = new List<FireInfo>();
}

/// <summary>
/// 开火点的类型, 顺序还是散弹
/// </summary>
public enum E_FireType
{
    shunxv,
    sandan,
}

public class FireInfo
{
    [XmlAttribute] public int id;
    [XmlAttribute] public E_FireType type;
    [XmlAttribute] public int num;
    [XmlAttribute] public float cd;        // 每个子弹的间隔时间
    [XmlAttribute] public string ids;      // 1, 10 代表1~10id中随机
    [XmlAttribute] public float delay;
}
