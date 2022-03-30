using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public static Boss instance;

    [Header("이동 속도")]
    public float Speed = 0f;

    [Header("체력 슬라이더")]
    public Slider HP_Slider;

    [Header("체력 UI")]
    public Text Hp_Text;
    private float Max_Hp = 500;
    public float Cur_Hp = 500;

    [Header("총알")]
    public GameObject Bullet_Prefab;

    [Header("몬스터 스킬 (통나무)")]
    public GameObject Long_Circle;
    public Transform Logn_Circle_Direction;

    [Header("보스 이동경로")]
    Vector3 Target = new Vector3(557f, 37f, 553f);

    [Header("보스 충돌 체크")]
    public bool BossHP_Check = true; // 플레이어가 총을 쐈을 때 보스가 안맞도록 하기 위해 bool 변수를 만들어준다.
    public bool BossMove_Check = true;
    public bool All_Enemy_Check = true; // 스폰 적을 관리해줄 bool 변수를 만들어준다.

    [Header("보스 움직임 시네마신")]
    public PlayableDirector Boss_Check;

    public GameObject UI_Canvas;

    void Start()
    {
        Boss_Check.Stop();

        // 초기화 해준다.
        BossMove_Check = true;
        BossHP_Check = true;
        All_Enemy_Check = true;
        Speed = 0f;

        HP_Slider.value = (float)Cur_Hp / (float)Max_Hp;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Stage_01")
        {
            Skill_Stop();
            Boss_Move();
            Boss_Move_02();
        }
    }

    private void Awake()
    {
        Boss.instance = this;
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
            //BossHP_Check = false;
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
        #region 보스 스킬(원형 발사)
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
        #endregion
    }

    private IEnumerator Skill_02()
    {
        #region 보스 스킬(통나무 굴리기)
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
        #endregion
    }

    private IEnumerator Skill_03()
    {
        #region 보스 스킬(원형 발사_02)
        while (true)
        {
            yield return null;
            yield return new WaitForSeconds(2f);
            float Attack_Rate = 0.5f; // 공격 주기
            int Count = 5; // 발사체 생성 개수
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
        #endregion
    }

    private void Skill_Stop()
    {
        // 보스의 HP가 0이하 일 때
        if (Cur_Hp <= 0)
        {
            // Skill_01, Skill_02, Skill_03 코루틴을 중지시킨다.
            StopCoroutine("Skill_01");
            StopCoroutine("Skill_02");
            StopCoroutine("Skill_03");

            // 보스 오브젝트를 삭제 시킨다.
            Destroy(this.gameObject);
        }
    }

    private void Skill_Start()
    {
        if (SceneManager.GetActiveScene().name == "Stage_01")
        {
            // 만약 몬스터 죽은 점수가 30이하 일 때 
            if (UI_Manager.instance.EnemyDie_Point >= 1)
            {
                // Skill_01, Skill_02, Skill_03 코루틴을 실행시킨다.
                StartCoroutine("Skill_01");
                StartCoroutine("Skill_02");
                StartCoroutine("Skill_03");
            }
        }
    }

    private void Boss_Move()
    {
        // 만약 몬스터의 죽은 점수가 30이하 일 때
        if (UI_Manager.instance.EnemyDie_Point >= 1)
        {
            // 보스를 지정한 위치로 이동시킨다.
            Vector3 velo = Vector3.zero;
            transform.position = Vector3.Lerp(transform.position, Target, 0.005f);

            // 만약 보스이동 확인이 true 일 때
            if (BossMove_Check)
            {
                // 보스의 움직임 시네마신을 실행시킨다.
                Boss_Check.Play();
            }
        }
    }

    private void Boss_Move_02()
    {
        // 만약 보스의움직임 확인이 false 일 때 
        if (!BossMove_Check)
        {
            Boss_Check.Stop(); // 보스 움직임 시네마신을 멈춘다.
            transform.GetChild(6).gameObject.SetActive(true); // 보스 HP바 오브젝트를 SetActive true 한다.
            UI_Canvas.SetActive(true); // UI(스코어, 몬스터죽은 횟수, 플레이어 HP 등 등) 캔버스를 SetActive true 한다,
        }
    }
}
