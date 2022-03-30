using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public static Boss instance;

    [Header("�̵� �ӵ�")]
    public float Speed = 0f;

    [Header("ü�� �����̴�")]
    public Slider HP_Slider;

    [Header("ü�� UI")]
    public Text Hp_Text;
    private float Max_Hp = 500;
    public float Cur_Hp = 500;

    [Header("�Ѿ�")]
    public GameObject Bullet_Prefab;

    [Header("���� ��ų (�볪��)")]
    public GameObject Long_Circle;
    public Transform Logn_Circle_Direction;

    [Header("���� �̵����")]
    Vector3 Target = new Vector3(557f, 37f, 553f);

    [Header("���� �浹 üũ")]
    public bool BossHP_Check = true; // �÷��̾ ���� ���� �� ������ �ȸµ��� �ϱ� ���� bool ������ ������ش�.
    public bool BossMove_Check = true;
    public bool All_Enemy_Check = true; // ���� ���� �������� bool ������ ������ش�.

    [Header("���� ������ �ó׸���")]
    public PlayableDirector Boss_Check;

    public GameObject UI_Canvas;

    void Start()
    {
        Boss_Check.Stop();

        // �ʱ�ȭ ���ش�.
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

            Debug.Log("���� ����");
            Speed = 0f;
            //BossHP_Check = false;
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
        #region ���� ��ų(���� �߻�)
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
        #endregion
    }

    private IEnumerator Skill_02()
    {
        #region ���� ��ų(�볪�� ������)
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
        #region ���� ��ų(���� �߻�_02)
        while (true)
        {
            yield return null;
            yield return new WaitForSeconds(2f);
            float Attack_Rate = 0.5f; // ���� �ֱ�
            int Count = 5; // �߻�ü ���� ����
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
        #endregion
    }

    private void Skill_Stop()
    {
        // ������ HP�� 0���� �� ��
        if (Cur_Hp <= 0)
        {
            // Skill_01, Skill_02, Skill_03 �ڷ�ƾ�� ������Ų��.
            StopCoroutine("Skill_01");
            StopCoroutine("Skill_02");
            StopCoroutine("Skill_03");

            // ���� ������Ʈ�� ���� ��Ų��.
            Destroy(this.gameObject);
        }
    }

    private void Skill_Start()
    {
        if (SceneManager.GetActiveScene().name == "Stage_01")
        {
            // ���� ���� ���� ������ 30���� �� �� 
            if (UI_Manager.instance.EnemyDie_Point >= 1)
            {
                // Skill_01, Skill_02, Skill_03 �ڷ�ƾ�� �����Ų��.
                StartCoroutine("Skill_01");
                StartCoroutine("Skill_02");
                StartCoroutine("Skill_03");
            }
        }
    }

    private void Boss_Move()
    {
        // ���� ������ ���� ������ 30���� �� ��
        if (UI_Manager.instance.EnemyDie_Point >= 1)
        {
            // ������ ������ ��ġ�� �̵���Ų��.
            Vector3 velo = Vector3.zero;
            transform.position = Vector3.Lerp(transform.position, Target, 0.005f);

            // ���� �����̵� Ȯ���� true �� ��
            if (BossMove_Check)
            {
                // ������ ������ �ó׸����� �����Ų��.
                Boss_Check.Play();
            }
        }
    }

    private void Boss_Move_02()
    {
        // ���� �����ǿ����� Ȯ���� false �� �� 
        if (!BossMove_Check)
        {
            Boss_Check.Stop(); // ���� ������ �ó׸����� �����.
            transform.GetChild(6).gameObject.SetActive(true); // ���� HP�� ������Ʈ�� SetActive true �Ѵ�.
            UI_Canvas.SetActive(true); // UI(���ھ�, �������� Ƚ��, �÷��̾� HP �� ��) ĵ������ SetActive true �Ѵ�,
        }
    }
}
