using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public enum E_moveType
{
    Straight,   // ֱ��
    Curve,      // ����
    LeftPara,   // ������l
    RightPara,  // ������r
    Auto,       // ����
}

public class BulletData
{
    public List<BulletInfo> bulletInfos = new List<BulletInfo>();
}

public class BulletInfo
{
    [XmlAttribute] public int id;                  // �ӵ����ݵ�id
    [XmlAttribute] public E_moveType type;         // �ӵ��ƶ�����
    [XmlAttribute] public float forwardSpeed;      // �������ٶ�
    [XmlAttribute] public float rightSpeed;        // �����ƶ��ٶ�
    [XmlAttribute] public float roundSpeed;        // ��ת�ٶ�
    [XmlAttribute] public string resPath;          // ��Դ·��
    [XmlAttribute] public string deadEff;          // ������Ч·��
    [XmlAttribute] public float lifeTime;      
}
