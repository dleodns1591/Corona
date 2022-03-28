using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMent : MonoBehaviour
{
    [Header("이동 속도")]
    public float MoveSpeed = 0.0f;

    [Header("이동 방향")]
    public Vector3 Move_Direction = Vector3.zero;

    [Header("공격력")]
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
