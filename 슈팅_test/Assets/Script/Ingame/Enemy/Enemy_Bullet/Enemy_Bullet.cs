using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    [Header("이동 속도")]
    public float Speed = 0f;

    [Header("공격력")]
    public float Attack = 0f;

    [Header("지워지는 속도")]
    public float Time_Speed = 0f;

    private void Start()
    {
        Destroy(this.gameObject, Time_Speed);
    }

    private void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Destroy_Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
