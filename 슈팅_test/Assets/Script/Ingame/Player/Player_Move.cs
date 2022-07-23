using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Move : MonoBehaviour
{
    public static Player_Move instance;

    [Header("이동 속도")]
    public float Speed = 5;

    [Header("점프")]
    public float Gravity = -9.81f;
    public float Jump_Power = 10;
    private float Y_Velocity;

    [Header("체력 슬라이더")]
    [SerializeField]
    private Slider HP_Hader;

    [Header("체력 UI")]
    public Text Hp_Text;
    private float Max_Hp = 100;
    public float Cur_Hp = 100;

    // 공격 업그레이드 아이템을 먹었을 시 값이 올라간다.
    private float Attack_Upgrade = 0;
    // 무적 아이템을 관리한 변수를 만들어준다.
    private bool Enemy_Invincibility;
    // 플레이어 이동속도 2배 아이템을 관리할 변수를 만들어준다.
    private bool MY_Speed = true;

    // 보스움직임 시네머신이 돌아가고 있을 때 공격을 맞지 않게 하기 위하여 변수를 만들어준다.
    public bool Player_Boss = true;

    public bool Player_Load = true;

    CharacterController CC;

    void Start()
    {
        Player_Load = true;
        Player_Boss = true;
        CC = GetComponent<CharacterController>();

        Handle_Hp();

    }


    void Update()
    {
        Speed = 50f;

        if (SceneManager.GetActiveScene().name == "Stage_02")
        {
            if (Player_Load)
            {
                HP_Hader = GameObject.Find("HP_Slider").GetComponent<Slider>();
                Hp_Text = GameObject.Find("HP_Text").GetComponent<Text>();
                Cur_Hp = 100;
                this.gameObject.transform.position = new Vector3(557f, 59f, 67.54f);
                Player_Load = false;
            }
        }
        if (Player_Boss)
        {
            // 보스의 시네마신이 시작하기 전
            this.gameObject.layer = 0;
        }
        else if (!Player_Boss)
        {
            // 보스의 시네마신이 진행되고 있을 때
            this.gameObject.layer = 6;
        }

        if (Boss.instance.Cur_Hp <= 0)
        {
            // 보스의 HP가 0 이하 일 때 플레이어와 모든 충돌을 꺼준다.
            this.gameObject.layer = 6;
            Speed = 0f;
        }

        // 점프
        // Y 속도에 중력을 계속 더한다.
        Y_Velocity += Gravity * Time.deltaTime;
        // 만약 사용자가 바닥에 붙어있다면
        if (CC.isGrounded)
        {
            // 만약 사용자가 스페이스키를 누른다면, Y속도에 뛰는 힘을 대입한다.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Y_Velocity = Jump_Power;
            }
        }


        // 이동
        // 1. 사용자의 입력에 따라
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. 앞뒤 좌우로 방향을 만든다.
        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        // 카메라가 보고있는 방향을 앞 방향으로 변경한다.
        dir = Camera.main.transform.TransformDirection(dir);
        // 대각선 이동으로 하면서 루트2로 길이가 늘어나기에 1로 만들어준다. ( 정규화 : Normalize )
        dir.Normalize();
        // Y 속도를 최종 dir의 Y에 대입한다.
        dir.y = Y_Velocity;

        // Move 움직이전에 충돌 체크를 해준다. 만약 충돌하면 멈춘다.
        CC.Move(dir * Speed * Time.deltaTime);
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Handle_Hp()
    {
        HP_Hader.value = (float)Cur_Hp / (float)Max_Hp;
    }


    private void OnTriggerEnter(Collider other)
    {
        // Enemy_01 (박테리아) 와 닿았을 경우
        // Enemy_02 (세균) 과 닿았을 경우
        // Enemy_03 (바이러스) 와 닿았을 경우
        // Enemy_04 (암세포) 와 닿았을 경우
        Enemy_Move EM = other.GetComponent<Enemy_Move>();
        if (other.CompareTag("Enemy_01") && !Enemy_Invincibility || other.CompareTag("Enemy_02") && !Enemy_Invincibility || other.CompareTag("Enemy_03") && !Enemy_Invincibility || other.CompareTag("Enemy_04") && !Enemy_Invincibility)
        {
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= EM.Damage / 2;
            Hp_Text.text = "HP : " + Cur_Hp + "/ 100";
            StartCoroutine(Invincibility()); // 무적
        }

        // 몬스터 총알 과 닿았을 경우
        if (other.CompareTag("Enemy_Bullet") && !Enemy_Invincibility)
        {
            Enemy_Bullet EB = other.GetComponent<Enemy_Bullet>();
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= EB.Attack;
            Hp_Text.text = "HP : " + Cur_Hp + "/ 100";
            StartCoroutine(Invincibility()); // 무적
        }

        // 보스의 총알 과 닿았을 경우
        if (other.CompareTag("Boss_Bullet") && !Enemy_Invincibility)
        {
            MoveMent MM = other.GetComponent<MoveMent>();
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= MM.Attack;
            Hp_Text.text = "HP : " + Cur_Hp + "/ 100";
            StartCoroutine(Invincibility()); // 무적
        }

        if (other.CompareTag("유도탄") && !Enemy_Invincibility)
        {
            Guided_Missile GM = other.GetComponent<Guided_Missile>();
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= GM.Attack;
            Hp_Text.text = "HP : " + Cur_Hp + "/ 100";
            StartCoroutine(Invincibility()); // 무적
            Destroy(other.gameObject);
        }

        // 보스의 스킬 과 닿았을 경우
        if (other.CompareTag("Boss_Skill") && !Enemy_Invincibility)
        {
            Boss_Skill BS = other.GetComponent<Boss_Skill>();
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= BS.Attack;
            Hp_Text.text = "HP : " + Cur_Hp + "/ 100";
            StartCoroutine(Invincibility()); // 무적
        }

        // 아이템 01 ( 공격 업그레이드  아이템 ) 과 닿았을 경우
        if (other.CompareTag("Item_01"))
        {
            UI_Manager.instance.Item(1);
            StartCoroutine("Attack_UpGrade");
            Destroy(other.gameObject);
        }

        // 아이템 02 ( 무적 아이템 ) 과  닿았을 경우
        if (other.CompareTag("Item_02"))
        {
            UI_Manager.instance.Item(1);
            StartCoroutine(Invincibility_Item());
            Destroy(other.gameObject);
        }

        // 아이템 03 ( HP 회복 아이템 ) 과  닿았을 경우
        if (other.CompareTag("Item_03"))
        {
            UI_Manager.instance.Item(1);
            if (Cur_Hp == 100)
            {
                Destroy(other.gameObject);
            }

            else if (Cur_Hp <= 99 && Cur_Hp >= 90)
            {
                Invoke("Handle_Hp", 0.01f);
                Cur_Hp = 100;
                Hp_Text.text = "HP : " + Cur_Hp + "/ 100";
            }

            else
            {
                Invoke("Handle_Hp", 0.01f);
                Cur_Hp += 10;
                Hp_Text.text = "HP : " + Cur_Hp + "/ 100";
            }
        }

        // 아이템 04 ( 고통데미지 감소 아이템 ) 과  닿았을 경우
        if (other.CompareTag("Item_04"))
        {
            UI_Manager.instance.Item(1);
            other.gameObject.tag = "Item_04(false)";
            GameObject.Find("PainDown_Item").SetActive(false);
        }

        // 아이템 05 ( 몬스터들의 이동속도 감소 아이템 ) 과  닿았을 경우
        if (other.CompareTag("Item_05"))
        {
            UI_Manager.instance.Item(1);
            StartCoroutine("SpeedDown_Itme");
        }

        // 아이템 06 ( 이동속도 2배 아이템 ) 과  닿았을 경우
        if (other.CompareTag("Item_06"))
        {
            UI_Manager.instance.Item(1);
            MY_Speed = true;
            StartCoroutine("MySpeedUP_Item");
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        // Enemy_04 ( 4번째 몬스터인 파티클 공격 ) 에 닿았을 때
        if (other.CompareTag("Smoke_Particle") && !Enemy_Invincibility)
        {
            Enemy_Particle EP = other.GetComponent<Enemy_Particle>();
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= EP.Damage;
            Hp_Text.text = "HP : " + Cur_Hp + "/ 100";
            StartCoroutine(Invincibility()); // 무적
        }
    }

    IEnumerator Invincibility()
    {
        // 몬스터와 충돌 될 경우 1.5초 정도의 무적시간을 가지고 있다.
        Enemy_Invincibility = true;
        Debug.Log("무적");
        yield return new WaitForSeconds(1.5f);
        Debug.Log("끝");
        Enemy_Invincibility = false;
    }

    IEnumerator Attack_UpGrade()
    {
        yield return
        Attack_Upgrade += 1;
        if (Attack_Upgrade == 1)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (Attack_Upgrade == 2)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);
        }
        else if (Attack_Upgrade == 3)
        {
            transform.GetChild(3).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(true);
        }
        else if (Attack_Upgrade >= 4)
        {
            transform.GetChild(4).gameObject.SetActive(false);
            transform.GetChild(5).gameObject.SetActive(true);
        }
    }

    IEnumerator Invincibility_Item()
    {
        // 3초 동안 무적시간을 갖는다.
        Enemy_Invincibility = true;
        Debug.Log("무적");
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("끝");
        Enemy_Invincibility = false;
    }

    IEnumerator SpeedDown_Itme()
    {
        // 인게임에 소환되어 있는 몬스터들만 속도 제한 된다.
        Enemy_Move.instance.Move_Speed -= 50f;
        yield return new WaitForSeconds(5f);
        Enemy_Move.instance.Move_Speed += 50f;

    }

    IEnumerator MySpeedUP_Item()
    {
        if (MY_Speed)
        {
            Speed = 150f;
            Jump_Power = 3f;
            yield return new WaitForSeconds(10f);
            Jump_Power = 5f;

            Speed = 50f;
            MY_Speed = false;
        }
    }
}
