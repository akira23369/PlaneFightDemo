using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    private BulletInfo info;
    private float time = 0;     // 曲线计时的变量
    public void InitInfo(BulletInfo info)
    {
        this.info = info;
        Invoke("DealyDestroy", info.lifeTime);
    }

    private void Start()
    {
        //InitInfo(GameDataMgr.Instance.bulletData.bulletInfos[16]);
    }

    private void DealyDestroy() => Dead();
    public void Dead()
    {
        GameObject deadEff = Instantiate(Resources.Load<GameObject>(info.deadEff));
        deadEff.transform.position = transform.position;
        Destroy(deadEff, 1);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerObject player = other.GetComponent<PlayerObject>();
            player?.Wound();
            Dead();
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * info.forwardSpeed * 0.5f * Time.deltaTime);
        time += Time.deltaTime;
        switch (info.type)
        {
            case E_moveType.Straight:
                break;
            case E_moveType.Curve:
                transform.Translate(Vector3.right * info.rightSpeed * Mathf.Sin(time * info.roundSpeed) * Time.deltaTime);
                break;
            case E_moveType.LeftPara:
                transform.rotation *= Quaternion.AngleAxis(info.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case E_moveType.RightPara:
                transform.rotation *= Quaternion.AngleAxis(-info.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case E_moveType.Auto:
                transform.rotation = Quaternion.Slerp(transform.rotation, 
                    Quaternion.LookRotation(PlayerObject.Instance.transform.position - transform.position), Time.deltaTime);
                break;
        }
    }
}