using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Cell : MonoBehaviour
{
    public static Red_Cell instance;

    [Header("이동 속도")]
    [SerializeField]
    public float Move_Speed = 0.0f;

    [Header("이동 방향")]
    [SerializeField]
    public Vector3 Move_Direction = Vector3.zero;

    [Header("체력")]
    [SerializeField]
    public float HP = 0.0f;

    void Start()
    {

    }

    void Update()
    {
        transform.position += Move_Direction * Move_Speed * Time.deltaTime;
    }

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy_01") || other.CompareTag("Enemy_02") || other.CompareTag("Enemy_03") || other.CompareTag("Enemy_04") || other.CompareTag("Bullet") || other.CompareTag("Player"))
        {
            HP -= 10;
            if (HP <= 0)
            {
                tag = "Red_Cell(false)";
                transform.GetChild(0).gameObject.SetActive(false);
                Move_Speed = 1000;
            }
        }
    }

}
