using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header("이동 속도")]
    public float Speed = 0f;

    [Header("체력 슬라이더")]
    public Slider HP_Slider;

    [Header("체력 UI")]
    public Text Hp_Text;
    private float Max_Hp = 500;
    private float Cur_Hp = 500;

    [Header("총알")]
    public GameObject Bullet_Prefab;

    [Header("몬스터 스킬 (통나무)")]
    public GameObject Long_Circle;
    public Transform Logn_Circle_Direction;

    [Header("보스 이동경로")]
    Vector3 Target = new Vector3(557f, 37f, 553f);

    [Header("보스 충돌 체크")]
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
            Debug.Log("벽과 닿음");
            Speed = 0f;
            transform.GetChild(6).gameObject.SetActive(true);
            BossHP_Check = false;
            Skill_Start();
        }

        // 플레이어 총알 과 닿았을 때
        if (other.CompareTag("Bullet") && BossHP_Check == false)
        {
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= bullet.Attack;
        }
    }

    private IEnumerator Skill_01()
    {
        yield return new WaitForSeconds(3);
        float Attack_Rate = 1.8f; // 공격 주기
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
            float Attack_Rate = 1; // 공격 주기
            int Count = 15; // 발사체 생성 개수
            float Interval_Angle = 360 / Count; // 발사체 사이의 각도
            float Weight_Angle = 180; // 가중되는 각도 (항상 같은 위치로 발사하지 않도록 설정)

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
                    float x = Mathf.Sin(Angle * Mathf.PI / 180.0f);
                    //float z = Mathf.Cos(Angle * Mathf.PI / 180.0f);
                    // 발사체 이동 방향 설정
                    Clone.GetComponent<MoveMent>().MoveTo(new Vector3(x, 0, -1));
                }

                // 발사체가 생성되는 시작 각도 설정을 위한 변수;
                Weight_Angle += 1;

                // Attack_Rate 시간만큼 대기
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
