using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    [Header("�̵� �ӵ�")]
    public float Speed = 1f;

    [Header("���ݷ�")]
    public float Attack = 6f;

    private void Start()
    {
        // ���� ������Ʈ�� 5���� �����ȴ�.
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
