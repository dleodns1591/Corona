using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    [Header("이동 속도")]
    public float Speed = 1f;

    [Header("공격력")]
    public float Attack = 6f;

    private void Start()
    {
        // 현재 오브젝트는 5초후 삭제된다.
        Destroy(this.gameObject, 5f);
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
