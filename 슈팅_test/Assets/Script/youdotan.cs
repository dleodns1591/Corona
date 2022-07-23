using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class youdotan : MonoBehaviour
{
    [Header("이동 속도")]
    public float Speed;

    [Header("총알 회전속도")]
    public float Bullet_Speed;

    [Header("목표물")]
    public Transform Target;

    [Header("총알 rigidbody")]
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
