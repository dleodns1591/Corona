using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header("이동 속도")]
    public float Speed = 0f;

    [Header("이동 방향")]
    public Vector3 Move_Direction = Vector3.zero;

    [Header("체력 슬라이더")]
    public Slider HP_Slider;

    [Header("체력 UI")]
    public Text Hp_Text;
    private float Max_Hp = 500;
    private float Cur_Hp = 500;

    [Header("총알")]
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
            Debug.Log("벽과 닿음");
            Speed = 0f;
            transform.GetChild(5).gameObject.SetActive(true);
            
        }

        // 플레이어 총알 과 닿았을 때
        if (other.CompareTag("Bullet"))
        {
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= bullet.Attack;
        }
    }

    private IEnumerator Skill_01()
    {
        //yield return new WaitForSeconds(3);
        float Attack_Rate = 1.5f; // 공격 주기
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

    public void Circle_Skill()
    {
        if (Cur_Hp >= 350)
        {
            Debug.Log("350 이상");
            StartCoroutine("Skill_01");
        }

        else if (Cur_Hp >= 200 && Cur_Hp < 350)
        {
            Debug.Log("350 미만");
            StopCoroutine("Skill_01");
        }
    }
}
