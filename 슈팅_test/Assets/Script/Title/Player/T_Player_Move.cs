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
        // ����
        // Y �ӵ��� �߷��� ��� ���Ѵ�.
        Y_Velocity += Gravity * Time.deltaTime;
        // ���� ����ڰ� �ٴڿ� �پ��ִٸ�
        //if (CC.isGrounded)
        //{
            // ���� ����ڰ� �����̽�Ű�� �����ٸ�, Y�ӵ��� �ٴ� ���� �����Ѵ�.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("����");
                Y_Velocity = Jump_Power;
            }
        //}


        // �̵�
        // 1. ������� �Է¿� ����
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. �յ� �¿�� ������ �����.
        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        // ī�޶� �����ִ� ������ �� �������� �����Ѵ�.
        dir = Camera.main.transform.TransformDirection(dir);
        // �밢�� �̵����� �ϸ鼭 ��Ʈ2�� ���̰� �þ�⿡ 1�� ������ش�. ( ����ȭ : Normalize )
        dir.Normalize();
        // Y �ӵ��� ���� dir�� Y�� �����Ѵ�.
        dir.y = Y_Velocity;

        // Move ���������� �浹 üũ�� ���ش�. ���� �浹�ϸ� �����.
        CC.Move(dir * Speed * Time.deltaTime);
    }

}
