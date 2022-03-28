using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill : MonoBehaviour
{
    [Header("이동 속도")]
    public float Speed = 0f;

    [Header("회전 속도")]
    public float Rotate_Speed = 0f;

    [Header("이동 방향")]
    public Vector3 Move_Direction = Vector3.zero;

    [Header("공격력")]
    public float Attack = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * Rotate_Speed);
        transform.position += Move_Direction * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroy_Wall"))
        {
            Destroy(this.gameObject);
        }
    }

}
