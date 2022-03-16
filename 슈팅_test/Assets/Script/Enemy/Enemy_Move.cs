using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour
{
    [SerializeField]
    public float Move_Speed = 0.0f;
    [SerializeField]
    public Vector3 Move_Direction = Vector3.zero;

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
        // �Ѿ����� ����� ���
        if (other.CompareTag("Bullet") || other.CompareTag("Player") || other.CompareTag("Destroy_Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
