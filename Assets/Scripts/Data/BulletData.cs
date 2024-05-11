using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public enum E_moveType
{
    Straight,   // 直线
    Curve,      // 曲线
    LeftPara,   // 抛物线l
    RightPara,  // 抛物线r
    Auto,       // 跟踪
}

public class BulletData
{
    public List<BulletInfo> bulletInfos = new List<BulletInfo>();
}

public class BulletInfo
{
    [XmlAttribute] public int id;                  // 子弹数据的id
    [XmlAttribute] public E_moveType type;         // 子弹移动类型
    [XmlAttribute] public float forwardSpeed;      // 正朝向速度
    [XmlAttribute] public float rightSpeed;        // 横向移动速度
    [XmlAttribute] public float roundSpeed;        // 旋转速度
    [XmlAttribute] public string resPath;          // 资源路径
    [XmlAttribute] public string deadEff;          // 死亡特效路径
    [XmlAttribute] public float lifeTime;      
}
