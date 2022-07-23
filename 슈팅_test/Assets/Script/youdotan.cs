using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class youdotan : MonoBehaviour
{
    [Header("�̵� �ӵ�")]
    public float Speed;

    [Header("�Ѿ� ȸ���ӵ�")]
    public float Bullet_Speed;

    [Header("��ǥ��")]
    public Transform Target;

    [Header("�Ѿ� rigidbody")]
    public Rigidbody Bullet_Rigidbody;

    private Transform Bullet_LocalTrans;

    private void Start()
    {
        Bullet_LocalTrans = GetComponent<Transform>();
    }

    private void Update()
    {
        Target = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Bullet_Rigidbody.velocity = Bullet_LocalTrans.forward * Speed;
        var Bullet_Rotation = Quaternion.LookRotation(Target.position - Bullet_LocalTrans.position);
        Bullet_Rigidbody.MoveRotation(Quaternion.RotateTowards(Bullet_LocalTrans.rotation, Bullet_Rotation, Bullet_Speed));
    }

}
