using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FireData
{
    public List<FireInfo> fireInfos = new List<FireInfo>();
}

/// <summary>
/// ����������, ˳����ɢ��
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
    [XmlAttribute] public float cd;        // ÿ���ӵ��ļ��ʱ��
    [XmlAttribute] public string ids;      // 1, 10 ����1~10id�����
    [XmlAttribute] public float delay;
}
