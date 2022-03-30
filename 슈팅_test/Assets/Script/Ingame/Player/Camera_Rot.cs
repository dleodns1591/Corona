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

        // 1. ���콺 �Է� ���� �̿��Ѵ�.
        float mx = Input.GetAxis("Mouse X"); // ����â���� ���콺�� ���� ���������� �̵��Ҷ� ���� ( �� -���� : ���� +��� )
        float my = Input.GetAxis("Mouse Y"); // ����â���� ���콺�� ���� ���������� �̵��Ҷ� ���� ( �Ʒ� -���� : �� +��� )
        rx += Rot_Speed * my * Time.deltaTime;
        ry += Rot_Speed * mx * Time.deltaTime;

        // rx ȸ�� ���� ���� ( ȭ�� ������ ���콺�� �������� X�� ȸ�� ���� �ϵ� ��� ���� ���� ���� )
        rx = Mathf.Clamp(rx, -80, 80);
        ry = Mathf.Clamp(ry, -80, 80);
        // X���� ������ ���� X���� �̵��� �ƴ϶� X���� ȸ�� �ؼ� ���Ʒ� ���� ������ X���̿��� �Ѵ�.
        // Y���� ������ ���� Y���� �̵��� �ƴ϶� Y���� ȸ�� �ؼ� ���ʿ����� ���� ������ Y���̿��� �Ѵ�.

        // 2. ȸ���� �Ѵ�.
        transform.eulerAngles = new Vector3(-rx, ry, 0);
        // X���� ȸ���� ����� �����Ǹ�, �Ʒ�, ������ �����Ǹ� ���� ���ư���. ( �׷��� X�࿡ -�� �־���. )

    }

    private void Boss_Die()
    {
        if (Boss.instance.Cur_Hp <= 0)
        {
            Rot_Speed = 0f;
        }
    }

}
