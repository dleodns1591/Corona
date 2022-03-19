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

    CharacterController CC;

    void Start()
    {
        CC = GetComponent<CharacterController>();

        HP_Hader.value = (float)Cur_Hp / (float)Max_Hp;
    }

    void Update()
    {
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

    private void Handle_Hp()
    {
        HP_Hader.value = (float)Cur_Hp / (float)Max_Hp;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy_Move EM = other.GetComponent<Enemy_Move>();
        if (other.CompareTag("Enemy"))
        {
            Invoke("Handle_Hp", 0.01f);
            Cur_Hp -= EM.Damage / 2;
            Hp_Text.text = "HP : " + Cur_Hp + "/ 100";
            StartCoroutine(Invincibility()); // ����
        }
    }

    IEnumerator Invincibility()
    {
        this.gameObject.layer = 6;
        yield return new WaitForSeconds(1.5f);
        this.gameObject.layer = 0;
    }

}
