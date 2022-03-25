using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill : MonoBehaviour
{
    public GameObject Bullet_Prefab; // ������ �� �����Ǵ� ������

    void Start()
    {
        Invoke("Circle_Skill", 2f);

    }

    void Update()
    {
    }

    private IEnumerator Skill_01()
    {
        //yield return new WaitForSeconds(3);
        float Attack_Rate = 2f; // ���� �ֱ�
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

    private void Circle_Skill()
    {
        StartCoroutine("Skill_01");
    }
}
