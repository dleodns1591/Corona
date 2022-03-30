using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Rot : MonoBehaviour
{
    private float rx;
    private float ry;
    public float Rot_Speed = 200;

    void Start()
    {
        
    }

    void Update()
    {
        Boss_Die();

        // 1. 마우스 입력 값을 이용한다.
        float mx = Input.GetAxis("Mouse X"); // 게임창에서 마우스를 왼쪽 오른쪽으로 이동할때 마다 ( 왼 -음수 : 오른 +양수 )
        float my = Input.GetAxis("Mouse Y"); // 게임창에서 마우스를 왼쪽 오른쪽으로 이동할때 마다 ( 아래 -음수 : 위 +양수 )
        rx += Rot_Speed * my * Time.deltaTime;
        ry += Rot_Speed * mx * Time.deltaTime;

        // rx 회전 각을 제한 ( 화면 밖으로 마우스가 나갔을때 X축 회전 덤블링 하듯 계속 도는 것을 방지 )
        rx = Mathf.Clamp(rx, -80, 80);
        ry = Mathf.Clamp(ry, -80, 80);
        // X축을 돌리는 이유 X축이 이동이 아니라 X축을 회전 해서 위아래 보는 방향은 X축이여야 한다.
        // Y축을 돌리는 이유 Y축이 이동이 아니라 Y축을 회전 해서 왼쪽오른쪽 보는 방향은 Y축이여야 한다.

        // 2. 회전을 한다.
        transform.eulerAngles = new Vector3(-rx, ry, 0);
        // X축의 회전은 양수가 증가되면, 아래, 음수가 증가되면 위로 돌아간다. ( 그래서 X축에 -를 넣었다. )

    }

    private void Boss_Die()
    {
        if (Boss.instance.Cur_Hp <= 0)
        {
            Rot_Speed = 0f;
        }
    }

}
