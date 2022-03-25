using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : MonoBehaviour
{
    public float Speed = 5;
    public float Gravity = -9.81f;
    public float Jump_Power = 10;
    private float Y_Velocity;

    [SerializeField]
    private Slider HP_Hader;

    private float Max_Hp = 100;
    private float Cur_Hp = 100;
    public Text Hp_Text;

    private bool Enemy_Invincibility;
    CharacterController CC;

    void Start()
    {
        CC = GetComponent<CharacterController>();

        HP_Hader.value = (float)Cur_Hp / (float)Max_Hp;
    }

    void Update()
    {
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

    private void Handle_Hp()
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
        if (other.CompareTag("Enemy_01") || other.CompareTag("Enemy_02") || other.CompareTag("Enemy_03") || other.CompareTag("Enemy_04"))
        {
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= EM.Damage / 2;
            Hp_Text.text = "HP : " + Cur_Hp + "/ 100";
            StartCoroutine(Invincibility()); // 무적
        }

        // 몬스터 총알 과 닿았을 경우
        if (other.CompareTag("Enemy_Bullet"))
        {
            Enemy_Bullet EB = other.GetComponent<Enemy_Bullet>();
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= EB.Attack;
            Hp_Text.text = "HP : " + Cur_Hp + "/ 100";
            StartCoroutine(Invincibility()); // 무적
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("충돌");
        if(other.CompareTag("Smoke_Particle") && !Enemy_Invincibility)
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
        this.gameObject.layer = 6;
        Enemy_Invincibility = true;
        yield return new WaitForSeconds(1.5f);
        this.gameObject.layer = 0;
        Enemy_Invincibility = false;
    }
}
