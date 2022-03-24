using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("�̵� �ӵ�")]
    public float Speed = 1f;

    [Header("���ݷ�")]
    public float Attack = 3f;
    bool isFire;
    Vector3 Direction;

    void Start()
    {
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        if (isFire)
        {
            transform.Translate(Direction * Time.deltaTime * Speed);
        }
    }

    public void Fire(Vector3 dir)
    {
        Direction = dir;
        isFire = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
