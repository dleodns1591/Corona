using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_rotation : MonoBehaviour
{
    [Header("회전 속도")]
    public float Rotate_Speed = 0f;

    void Start()
    {

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Stage_01")
        {
            if (Boss.instance.Cur_Hp == 500)
            {
                transform.Rotate(Vector3.forward * Time.deltaTime * Rotate_Speed);

            }
            else if (Boss.instance.Cur_Hp <= 0)
            {
                Rotate_Speed = 0f;
            }
        }

        if (SceneManager.GetActiveScene().name == "Stage_02")
        {
            if (Boss.instance.Cur_Hp == 1000)
            {
                transform.Rotate(Vector3.forward * Time.deltaTime * Rotate_Speed);

            }
            else if (Boss.instance.Cur_Hp <= 0)
            {
                Rotate_Speed = 0f;
            }
        }


    }
}
