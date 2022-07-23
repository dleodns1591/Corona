using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Guided_Missile : MonoBehaviour
{
    [Header("�Ѿ��� Ÿ��")]
    public Transform Bullet_Target;

    [Header("�Ѿ��� Rigidbody")]
    public Rigidbody Bullet_Rigidbody;

    [Header("�Ѿ��� ȸ�� �ӵ�")]
    public float Turn_Speed = 1f;

    [Header("�Ѿ��� ���ư��� �ӵ�")]
    public float FlySpeed = 10f;

    [Header("���ݷ�")]
    public float Attack = 7f;

    private Transform Bullet_LocalTrans;

    private void Start()
    {
        Destroy(this.gameObject, 5f);
        Bullet_LocalTrans = GetComponent<Transform>();
    }

    private void Update()
    {
        Bullet_Target = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (!Bullet_Rigidbody)
        {
            return;
        }

        //Bullet_Rigidbody.velocity = Bullet_LocalTrans.forward * FlySpeed;
        //var Bullet_TargetRot = Quaternion.LookRotation(Bullet_Target.position - Bullet_LocalTrans.position);
        //Bullet_Rigidbody.MoveRotation(Quaternion.RotateTowards(Bullet_LocalTrans.rotation, Bullet_TargetRot, Turn_Speed));

        Bullet_Rigidbody.velocity = Bullet_LocalTrans.forward * FlySpeed;
        var Bullet_TargetRot = Quaternion.LookRotation(Bullet_Target.position - Bullet_LocalTrans.position);
        Bullet_Rigidbody.MoveRotation(Quaternion.RotateTowards(Bullet_LocalTrans.rotation, Bullet_TargetRot, Turn_Speed));
    }
}
