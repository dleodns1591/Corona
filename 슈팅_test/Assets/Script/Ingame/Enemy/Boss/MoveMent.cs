using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMent : MonoBehaviour
{
    [Header("�̵� �ӵ�")]
    public float MoveSpeed = 0.0f;

    [Header("�̵� ����")]
    public Vector3 Move_Direction = Vector3.zero;

    [Header("���ݷ�")]
    public float Attack = 0.0f;

    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    void Update()
    {
        transform.position += Move_Direction * MoveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        Move_Direction = direction;
    }
}
