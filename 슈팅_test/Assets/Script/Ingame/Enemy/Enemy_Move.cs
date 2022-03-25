using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Move : MonoBehaviour
{
    [Header("이동 속도")]
    [SerializeField]
    public float Move_Speed = 0.0f;

    [Header("이동 방향")]
    [SerializeField]
    public Vector3 Move_Direction = Vector3.zero;

    [Header("공격력")]
    public float Damage = 0f;
    public float Particle_Damage = 0f;

    [Header("체력")]
    public float Hp = 0f;

    [Header("스코어")]
    public float Score = 0f;

    public Text Score_Text;

    void Start()
    {
        Score_Text = GameObject.Find("Score_Text").GetComponent<Text>();
    }

    void Update()
    {
        transform.position += Move_Direction * Move_Speed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        Move_Direction = direction;
    }

    public void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();

        // 총알 / 플레이어 / 벽 에 닿았을 경우 오브젝트가 삭제된다.
        if (other.CompareTag("Destroy_Wall") || other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Bullet"))
        {
            Hp -= bullet.Attack;
            if (Hp == 0)
            {
                Destroy(this.gameObject);
                Score_Text.text = "Score : " + Score;
            }
        }

    }
}
