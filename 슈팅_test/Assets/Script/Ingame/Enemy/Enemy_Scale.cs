using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Scale : MonoBehaviour
{
    [Header("크기 값")]
    public float time = 0;
    public float Size = 5;
    public float Up_SizeTime = 0.2f;

    [Header("회전 속도")]
    public float Rotate_Speed = 50;

    void Start()
    {

    }

    void Update()
    {
        if (time <= Up_SizeTime)
        {
            transform.localScale = new Vector3(2, 2, 2) * (1 + Size * time);
        }
        else if (time <= Up_SizeTime * 2)
        {
            transform.localScale = new Vector3(2, 2, 2) * (2 * Size * Up_SizeTime + 1 - time * Size);
        }
        else
        {
            transform.localScale = new Vector3(2, 2, 2);
            ResetAnim();
        }
        time += Time.deltaTime;

        transform.Rotate(Vector3.forward * Time.deltaTime * Rotate_Speed);
    }

    public void ResetAnim()
    {
        if (time >= 1.3)
        {
            time = 0;
        }
    }
}
