using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerObject : MonoBehaviour
{
    private static PlayerObject _instance;
    public static PlayerObject Instance => _instance;

    public int nowhp;
    public int maxhp;
    public int moveSpeed;
    public int roundSpeed;
    public bool isDead;

    private Quaternion targetQ;     // 按住ad左右旋转

    // 世界坐标转屏幕坐标的点
    private Vector3 nowPos;
    private Vector3 lastPos;        // 用于边界判断

    private void Awake()
    {
        _instance = this;
    }

    public void Wound()
    {
        if (isDead) return;
        nowhp -= 1;
        GamePanel.Instance.ChangeHp(nowhp);
        if (nowhp <= 0) Dead();
    }

    public void Dead()
    {
        isDead = true;
        GameOverPanel.Instance.ShowMe();
    }


    private float hValue;
    private float vValue;
    private void Update()
    {
        if (isDead) return;
        hValue = Input.GetAxisRaw("Horizontal");
        vValue = Input.GetAxisRaw("Vertical");

        if (hValue == 0) targetQ = Quaternion.identity;
        else targetQ = Quaternion.AngleAxis(-20 * hValue, Vector3.forward);

        // 按住ad慢慢转动
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQ, roundSpeed * Time.deltaTime);
        lastPos = transform.position;
        transform.Translate(Vector3.forward * vValue * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * hValue * moveSpeed * Time.deltaTime, Space.World);      // 不加World会慢慢下沉

        // 极限判断
        nowPos = Camera.main.WorldToScreenPoint(transform.position);
        if (nowPos.x <= 0 || nowPos.x >= Screen.width)
        {
            lastPos.y = transform.position.y;
            lastPos.z = transform.position.z;
            transform.position = lastPos;
        }
        if (nowPos.y <= 0 || nowPos.y >= Screen.height)
        {
            // 只拉z
            lastPos.x = transform.position.x;
            lastPos.y = transform.position.y;
            transform.position = lastPos;
        }

        // 射线检测
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000, 1 << LayerMask.NameToLayer("Bullet")))
            {
                BulletObject obj = hitInfo.transform.GetComponent<BulletObject>();
                obj.Dead();
            }
        }
    }
}
