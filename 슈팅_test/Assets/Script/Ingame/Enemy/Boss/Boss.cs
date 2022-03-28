using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header("�̵� �ӵ�")]
    public float Speed = 0f;

    [Header("ü�� �����̴�")]
    public Slider HP_Slider;

    [Header("ü�� UI")]
    public Text Hp_Text;
    private float Max_Hp = 500;
    private float Cur_Hp = 500;

    [Header("�Ѿ�")]
    public GameObject Bullet_Prefab;

    [Header("���� ��ų (�볪��)")]
    public GameObject Long_Circle;
    public Transform Logn_Circle_Direction;

    [Header("���� �̵����")]
    Vector3 Target = new Vector3(557f, 37f, 553f);

    [Header("���� �浹 üũ")]
    public bool BossHP_Check = true;

    void Start()
    {
        BossHP_Check = true;
        Speed = 0f;
        HP_Slider.value = (float)Cur_Hp / (float)Max_Hp;
    }

    void Update()
    {

        Skill_Stop();
        if (UI_Manager.instance.EnemyDie_Point >= 0)
        {
            Vector3 velo = Vector3.zero;
            transform.position = Vector3.Lerp(transform.position, Target, 0.005f);
        }
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
            transform.GetChild(6).gameObject.SetActive(true);
            BossHP_Check = false;
            Skill_Start();
        }

        // �÷��̾� �Ѿ� �� ����� ��
        if (other.CompareTag("Bullet") && BossHP_Check == false)
        {
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= bullet.Attack;
        }
    }

    private IEnumerator Skill_01()
    {
        yield return new WaitForSeconds(3);
        float Attack_Rate = 1.8f; // ���� �ֱ�
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

    private IEnumerator Skill_02()
    {
        if (Cur_Hp >= 350)
        {
            while (true)
            {
                yield return null;
                yield return new WaitForSeconds(10f);
                transform.GetChild(0).gameObject.SetActive(true);
                yield return new WaitForSeconds(2.5f);
                transform.GetChild(0).gameObject.SetActive(false);
                Instantiate(Long_Circle, Logn_Circle_Direction.position, Logn_Circle_Direction.transform.rotation);
            }
        }
    }

    private IEnumerator Skill_03()
    {
        while (true)
        {
            yield return null;
            yield return new WaitForSeconds(10f);
            float Attack_Rate = 1; // ���� �ֱ�
            int Count = 15; // �߻�ü ���� ����
            float Interval_Angle = 360 / Count; // �߻�ü ������ ����
            float Weight_Angle = 180; // ���ߵǴ� ���� (�׻� ���� ��ġ�� �߻����� �ʵ��� ����)

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
                    float x = Mathf.Sin(Angle * Mathf.PI / 180.0f);
                    //float z = Mathf.Cos(Angle * Mathf.PI / 180.0f);
                    // �߻�ü �̵� ���� ����
                    Clone.GetComponent<MoveMent>().MoveTo(new Vector3(x, 0, -1));
                }

                // �߻�ü�� �����Ǵ� ���� ���� ������ ���� ����;
                Weight_Angle += 1;

                // Attack_Rate �ð���ŭ ���
                yield return new WaitForSeconds(Attack_Rate);
            }
        }
    }

    private void Skill_Stop()
    {
        if (Cur_Hp <= 0)
        {
            StopCoroutine("Skill_01");
            StopCoroutine("Skill_02");
            StopCoroutine("Skill_03");

            Destroy(this.gameObject);
        }
    }

    private void Skill_Start()
    {
        if (UI_Manager.instance.EnemyDie_Point >= 30)
        {
            StartCoroutine("Skill_01");
            StartCoroutine("Skill_02");
            StartCoroutine("Skill_03");
        }
    }
}
