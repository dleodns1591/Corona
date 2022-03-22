using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField]
    public Transform Target; // ���� ���ӿ��� �ΰ��� ����� ������ Ÿ��
    [SerializeField]
    public float Scroll_Range = 0f;
    [SerializeField]
    public float Move_Speed = 0f;
    [SerializeField]
    public Vector3 Move_Direction = Vector3.back;

    void Start()
    {
        
    }

    void Update()
    {
        //����� Move_Direction �������� Move_Speed�� �ӵ��� �̵�
        transform.position += Move_Direction * Move_Speed * Time.deltaTime;

        //����� ������ ������ ����� ��ġ �缳��
        if (transform.position.z <= -Scroll_Range)
        {
            transform.position = Target.position + Vector3.forward * 1000;
        }
    }
}
