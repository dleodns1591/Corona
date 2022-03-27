using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Smooth : MonoBehaviour
{
    [SerializeField]
    public float Move_Speed = 0.0f;
    [SerializeField]
    public Vector3 Move_Direction = Vector3.zero;

    public float Damage = 2f;
    public float Hp = 3f;

    void Start()
    {

    }

    void Update()
    {
        transform.position += Move_Direction * Move_Speed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        Move_Direction = direction;
    }

    public void OnTriggerEnter(Collider other)
    {
        // �Ѿ� / �÷��̾� / �� �� ����� ��� ������Ʈ�� �����ȴ�.
        if (other.CompareTag("Destroy_Wall"))
        {
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Bullet") || other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

    }
}
