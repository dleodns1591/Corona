using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool isFire;
    Vector3 Direction;
    [SerializeField]
    float Speed = 1f;

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
