using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_rotation : MonoBehaviour
{
    [Header("ȸ�� �ӵ�")]
    public float Rotate_Speed = 50;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * Rotate_Speed);
    }
}
