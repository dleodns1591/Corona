using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill : MonoBehaviour
{
    public GameObject Bullet_Prefab; // 공격할 때 생성되는 프리팹

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
        float Attack_Rate = 2f; // 공격 주기
        int Count = 30; // 발사체 생성 개수
        float Interval_Angle = 360 / Count; // 발사체 사이의 각도
        float Weight_Angle = 0; // 가중되는 각도 (항상 같은 위치로 발사하지 않도록 설정)

        // 원 형태로 방사하는 발사체 생성 (Count 개수만큼)
        while (true)
        {
            for (int i = 0; i < Count; i++)
            {
                // 발사체 생성
                GameObject Clone = Instantiate(Bullet_Prefab, transform.position, Quaternion.identity);
                // 발사체 이동 방향 (각도)
                float Angle = Weight_Angle + Interval_Angle * i;
                // 발사체 이동 방향 (백터)
                float x = Mathf.Cos(Angle * Mathf.PI / 180.0f);
                float z = Mathf.Sin(Angle * Mathf.PI / 180.0f);
                // 발사체 이동 방향 설정
                Clone.GetComponent<MoveMent>().MoveTo(new Vector3(x, 0, z));
            }

            // 발사체가 생성되는 시작 각도 설정을 위한 변수;
            Weight_Angle += 1;

            // Attack_Rate 시간만큼 대기
            yield return new WaitForSeconds(Attack_Rate);
        }

    }

    private void Circle_Skill()
    {
        StartCoroutine("Skill_01");
    }
}
