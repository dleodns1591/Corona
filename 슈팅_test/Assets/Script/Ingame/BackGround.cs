using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField]
    public Transform Target; // 현재 게임에서 두개의 배경이 서로의 타겟
    [SerializeField]
    public float Scroll_Range = 0f;
    [SerializeField]
    public float Move_Speed = 0f;
    [SerializeField]
    public Vector3 Move_Direction = Vector3.back;

    void Start()
    {
        
    }

    void Update()
    {
        //배경이 Move_Direction 방향으로 Move_Speed의 속도로 이동
        transform.position += Move_Direction * Move_Speed * Time.deltaTime;

        //배경이 설정된 범위를 벗어나면 위치 재설정
        if (transform.position.z <= -Scroll_Range)
        {
            transform.position = Target.position + Vector3.forward * 1000;
        }
    }
}
