using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Player_Move : MonoBehaviour
{
    public float Speed = 5;
    public float Gravity = -9.81f;
    public float Jump_Power = 10;
    private float Y_Velocity;

    CharacterController CC;

    void Start()
    {
        CC = GetComponent<CharacterController>();

    }

    void Update()
    {
        // 점프
        // Y 속도에 중력을 계속 더한다.
        Y_Velocity += Gravity * Time.deltaTime;
        // 만약 사용자가 바닥에 붙어있다면
        //if (CC.isGrounded)
        //{
            // 만약 사용자가 스페이스키를 누른다면, Y속도에 뛰는 힘을 대입한다.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("점프");
                Y_Velocity = Jump_Power;
            }
        //}


        // 이동
        // 1. 사용자의 입력에 따라
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. 앞뒤 좌우로 방향을 만든다.
        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        // 카메라가 보고있는 방향을 앞 방향으로 변경한다.
        dir = Camera.main.transform.TransformDirection(dir);
        // 대각선 이동으로 하면서 루트2로 길이가 늘어나기에 1로 만들어준다. ( 정규화 : Normalize )
        dir.Normalize();
        // Y 속도를 최종 dir의 Y에 대입한다.
        dir.y = Y_Velocity;

        // Move 움직이전에 충돌 체크를 해준다. 만약 충돌하면 멈춘다.
        CC.Move(dir * Speed * Time.deltaTime);
    }

}
