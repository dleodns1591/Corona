using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header("�̵� �ӵ�")]
    public float Speed = 0f;

    [Header("�̵� ����")]
    public Vector3 Move_Direction = Vector3.zero;

    [Header("ü�� �����̴�")]
    public Slider HP_Slider;

    [Header("ü�� UI")]
    public Text Hp_Text;
    private float Max_Hp = 500;
    private float Cur_Hp = 500;

    [Header("�Ѿ�")]
    public GameObject Bullet_Prefab;


    void Start()
    {
        Invoke("Circle_Skill", 1f);
        HP_Slider.value = (float)Cur_Hp / (float)Max_Hp;
    }

    void Update()
    {
        transform.position += Move_Direction * Speed * Time.deltaTime;
    }

    public void Handle_Hp()
    {
        HP_Slider.value = (float)Cur_Hp / (float)Max_Hp;
    }

    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        Hp_Text.text = "Boss HP : " + Cur_Hp + "/ 500";

        if (other.CompareTag("Boss_Wall"))
        {
            Debug.Log("���� ����");
            Speed = 0f;
            transform.GetChild(5).gameObject.SetActive(true);
            
        }

        // �÷��̾� �Ѿ� �� ����� ��
        if (other.CompareTag("Bullet"))
        {
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= bullet.Attack;
        }
    }

    private IEnumerator Skill_01()
    {
        //yield return new WaitForSeconds(3);
        float Attack_Rate = 1.5f; // ���� �ֱ�
        int Count = 30; // �߻�ü ���� ����
        float Interval_Angle = 360 / Count; // �߻�ü ������ ����
        float Weight_Angle = 0; // ���ߵǴ� ���� (�׻� ���� ��ġ�� �߻����� �ʵ��� ����)

        // �� ���·� ����ϴ� �߻�ü ���� (Count ������ŭ)
        while (true)
        {
            for (int i = 0; i < Count; i++)
            {
                // �߻�ü ����
                GameObject Clone = Instantiate(Bullet_Prefab, transform.position, Quaternion.identity);
                // �߻�ü �̵� ���� (����)
                float Angle = Weight_Angle + Interval_Angle * i;
                // �߻�ü �̵� ���� (����)
                float x = Mathf.Cos(Angle * Mathf.PI / 180.0f);
                float z = Mathf.Sin(Angle * Mathf.PI / 180.0f);
                // �߻�ü �̵� ���� ����
                Clone.GetComponent<MoveMent>().MoveTo(new Vector3(x, 0, z));
            }

            // �߻�ü�� �����Ǵ� ���� ���� ������ ���� ����;
            Weight_Angle += 1;

            // Attack_Rate �ð���ŭ ���
            yield return new WaitForSeconds(Attack_Rate);
        }

    }

    public void Circle_Skill()
    {
        if (Cur_Hp >= 350)
        {
            Debug.Log("350 �̻�");
            StartCoroutine("Skill_01");
        }

        else if (Cur_Hp >= 200 && Cur_Hp < 350)
        {
            Debug.Log("350 �̸�");
            StopCoroutine("Skill_01");
        }
    }
}
