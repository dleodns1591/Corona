using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Move : MonoBehaviour
{
    public static Enemy_Move instance;

    [Header("�̵� �ӵ�")]
    [SerializeField]
    public float Move_Speed = 0.0f;

    [Header("�̵� ����")]
    [SerializeField]
    public Vector3 Move_Direction = Vector3.zero;

    [Header("���ݷ�")]
    public float Damage = 0f;

    [Header("ü��")]
    public float Hp = 0f;

    void Start()
    {

    }

    void Update()
    {
        transform.position += Move_Direction * Move_Speed * Time.deltaTime;
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();

        // �Ѿ� / �÷��̾� / �� �� ����� ��� ������Ʈ�� �����ȴ�.
        if (other.CompareTag("Destroy_Wall") || other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Bullet"))
        {
            Hp -= bullet.Attack;
            if (Hp <= 0 && CompareTag("Enemy_01"))
            {
                UI_Manager.instance.Score(100);
                Destroy(this.gameObject);
            }

            if (Hp <= 0 && CompareTag("Enemy_02"))
            {
                UI_Manager.instance.Score(200);
                Destroy(this.gameObject);
            }

            if (Hp <= 0 && CompareTag("Enemy_03"))
            {
                UI_Manager.instance.Score(300);
                Destroy(this.gameObject);
            }

            if (Hp <= 0 && CompareTag("Enemy_04"))
            {
                UI_Manager.instance.Score(400);
                Destroy(this.gameObject);
            }

            if (Hp <= 0)
            {
                UI_Manager.instance.EnemyDie(1);
            }

        }

    }
}
