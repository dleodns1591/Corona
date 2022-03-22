using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Move : MonoBehaviour
{
    [SerializeField]
    public float Move_Speed = 0.0f;
    [SerializeField]
    public Vector3 Move_Direction = Vector3.zero;

    public float Damage = 2f;
    public float Hp = 3f;

    Renderer Enemy_HitColor;

    public Material Mid_Material;
    public Material TopBottom_Material;

    void Start()
    {
        Enemy_HitColor = gameObject.GetComponent<Renderer>();
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
        // 총알 / 플레이어 / 벽 에 닿았을 경우 오브젝트가 삭제된다.
        if (other.CompareTag("Destroy_Wall"))
        {
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Bullet") || other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

    }
}
