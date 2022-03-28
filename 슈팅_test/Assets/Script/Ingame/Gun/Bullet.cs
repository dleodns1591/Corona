using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("이동 속도")]
    public float Speed = 1f;

    [Header("공격력")]
    public float Attack = 1f;
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
        if (other.CompareTag("Enemy_01") || other.CompareTag("Enemy_02") || other.CompareTag("Enemy_03") || other.CompareTag("Enemy_04") || other.CompareTag("Background"))
        {
            Destroy(this.gameObject);
        }
    }
}
