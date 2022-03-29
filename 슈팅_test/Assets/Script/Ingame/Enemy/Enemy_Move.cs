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

    private bool Enemy_Die = true;

    void Start()
    {
        Enemy_Die = true;
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
            if (Hp <= 0 && CompareTag("Enemy_01") && Enemy_Die)
            {
                Enemy_Die = false;
                Destroy(this.gameObject);
                UI_Manager.instance.Score(100);
                Debug.Log("Enemy_01 ���� Ƚ��");
                UI_Manager.instance.EnemyDie(1);
            }

            else if (Hp <= 0 && CompareTag("Enemy_02") && Enemy_Die)
            {
                Destroy(this.gameObject);
                UI_Manager.instance.Score(200);
                Debug.Log("Enemy_02 ���� Ƚ��");
                UI_Manager.instance.EnemyDie(1);
            }

            else if (Hp <= 0 && CompareTag("Enemy_03") && Enemy_Die)
            {
                Destroy(this.gameObject);
                UI_Manager.instance.Score(300);
                Debug.Log("Enemy_03 ���� Ƚ��");
                UI_Manager.instance.EnemyDie(1);
            }

            else if (Hp <= 0 && CompareTag("Enemy_04") && Enemy_Die)
            {
                Destroy(this.gameObject);
                UI_Manager.instance.Score(400);
                Debug.Log("Enemy_04 ���� Ƚ��");
                UI_Manager.instance.EnemyDie(1);
            }

        }

    }
}
