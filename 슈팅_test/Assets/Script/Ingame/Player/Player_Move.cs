using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : MonoBehaviour
{
    public static Player_Move instance;

    [Header("�̵� �ӵ�")]
    public float Speed = 5;

    [Header("����")]
    public float Gravity = -9.81f;
    public float Jump_Power = 10;
    private float Y_Velocity;

    [Header("ü�� �����̴�")]
    [SerializeField]
    private Slider HP_Hader;

    [Header("ü�� UI")]
    public Text Hp_Text;
    private float Max_Hp = 100;
    private float Cur_Hp = 100;

    private float Attack_Upgrade = 0;
    private bool MY_Speed = true;

    public bool Player_Boss = true;

    private bool Enemy_Invincibility;
    CharacterController CC;

    void Start()
    {
        Player_Boss = true;
        CC = GetComponent<CharacterController>();

        HP_Hader.value = (float)Cur_Hp / (float)Max_Hp;

    }


    void Update()
    {
        if (Player_Boss)
        {
            this.gameObject.layer = 0;
        }
        else if(!Player_Boss)
        {
            this.gameObject.layer = 6;
        }

        // ����
        // Y �ӵ��� �߷��� ��� ���Ѵ�.
        Y_Velocity += Gravity * Time.deltaTime;
        // ���� ����ڰ� �ٴڿ� �پ��ִٸ�
        if (CC.isGrounded)
        {
            // ���� ����ڰ� �����̽�Ű�� �����ٸ�, Y�ӵ��� �ٴ� ���� �����Ѵ�.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Y_Velocity = Jump_Power;
            }
        }


        // �̵�
        // 1. ������� �Է¿� ����
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. �յ� �¿�� ������ �����.
        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        // ī�޶� �����ִ� ������ �� �������� �����Ѵ�.
        dir = Camera.main.transform.TransformDirection(dir);
        // �밢�� �̵����� �ϸ鼭 ��Ʈ2�� ���̰� �þ�⿡ 1�� ������ش�. ( ����ȭ : Normalize )
        dir.Normalize();
        // Y �ӵ��� ���� dir�� Y�� �����Ѵ�.
        dir.y = Y_Velocity;

        // Move ���������� �浹 üũ�� ���ش�. ���� �浹�ϸ� �����.
        CC.Move(dir * Speed * Time.deltaTime);
    }

    public void Handle_Hp()
    {
        HP_Hader.value = (float)Cur_Hp / (float)Max_Hp;
    }


    private void OnTriggerEnter(Collider other)
    {
        // Enemy_01 (���׸���) �� ����� ���
        // Enemy_02 (����) �� ����� ���
        // Enemy_03 (���̷���) �� ����� ���
        // Enemy_04 (�ϼ���) �� ����� ���
        Enemy_Move EM = other.GetComponent<Enemy_Move>();
        if (other.CompareTag("Enemy_01") || other.CompareTag("Enemy_02") || other.CompareTag("Enemy_03") || other.CompareTag("Enemy_04"))
        {
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= EM.Damage / 2;
            Hp_Text.text = "HP : " + Cur_Hp + "/ 100";
            StartCoroutine(Invincibility()); // ����
        }

        // ���� �Ѿ� �� ����� ���
        if (other.CompareTag("Enemy_Bullet"))
        {
            Enemy_Bullet EB = other.GetComponent<Enemy_Bullet>();
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= EB.Attack;
            Hp_Text.text = "HP : " + Cur_Hp + "/ 100";
            StartCoroutine(Invincibility()); // ����
        }

        // ������ �Ѿ� �� ����� ���
        if (other.CompareTag("Boss_Bullet"))
        {
            MoveMent MM = other.GetComponent<MoveMent>();
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= MM.Attack;
            Hp_Text.text = "HP : " + Cur_Hp + "/ 100";
            StartCoroutine(Invincibility()); // ����
        }

        if (other.CompareTag("����ź"))
        {
            Guided_Missile GM = other.GetComponent<Guided_Missile>();
            Invoke("Handle_Hp", 0.01f);
            //Cur_Hp -= GM.Attack;
            Hp_Text.text = "HP : " + Cur_Hp + "/ 100";
            StartCoroutine(Invincibility()); // ����
            Destroy(other.gameObject);
        }

        // ������ ��ų �� ����� ���
        if (other.CompareTag("Boss_Skill"))
        {
            Boss_Skill BS = other.GetComponent<Boss_Skill>();
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= BS.Attack;
            Hp_Text.text = "HP : " + Cur_Hp + "/ 100";
            StartCoroutine(Invincibility()); // ����
        }

        // ������ 01 ( ���� ���׷��̵�  ������ ) �� ����� ���
        if (other.CompareTag("Item_01"))
        {
            StartCoroutine("Attack_UpGrade");
            Destroy(other.gameObject);
        }

        // ������ 02 ( ���� ������ ) ��  ����� ���
        if (other.CompareTag("Item_02"))
        {
            StartCoroutine("Invincibility_Item");
            Destroy(other.gameObject);
        }

        // ������ 03 ( HP ȸ�� ������ ) ��  ����� ���
        if (other.CompareTag("Item_03"))
        {
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

        // ������ 04 ( ���뵥���� ���� ������ ) ��  ����� ���
        if (other.CompareTag("Item_04"))
        {
            other.gameObject.tag = "Item_04(false)";
            GameObject.Find("PainDown_Item").SetActive(false);
        }

        // ������ 05 ( ���͵��� �̵��ӵ� ���� ������ ) ��  ����� ���
        if (other.CompareTag("Item_05"))
        {
            StartCoroutine("SpeedDown_Itme");
        }

        // ������ 06 ( �̵��ӵ� 2�� ������ ) ��  ����� ���
        if (other.CompareTag("Item_06"))
        {
            StartCoroutine("MySpeedUP_Item");
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        // Enemy_04 ( 4��° ������ ��ƼŬ ���� ) �� ����� ��
        if (other.CompareTag("Smoke_Particle") && !Enemy_Invincibility)
        {
            Enemy_Particle EP = other.GetComponent<Enemy_Particle>();
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= EP.Damage;
            Hp_Text.text = "HP : " + Cur_Hp + "/ 100";
            StartCoroutine(Invincibility()); // ����
        }
    }

    IEnumerator Invincibility()
    {
        // ���Ϳ� �浹 �� ��� 1.5�� ������ �����ð��� ������ �ִ�.
        this.gameObject.layer = 6;
        Enemy_Invincibility = true;
        yield return new WaitForSeconds(1.5f);
        this.gameObject.layer = 0;
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
        // 3�� ���� �����ð��� ���´�.
        this.gameObject.layer = 6;
        Enemy_Invincibility = true;
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        this.gameObject.layer = 0;
        Enemy_Invincibility = false;
    }

    IEnumerator SpeedDown_Itme()
    {
        // �ΰ��ӿ� ��ȯ�Ǿ� �ִ� ���͵鸸 �ӵ� ���� �ȴ�.
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
