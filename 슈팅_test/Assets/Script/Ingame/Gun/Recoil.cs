using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    private float Time_Left = 0.2f;
    private float Next_Time = 0.0f;

    public Transform Target_01;
    public Transform Target_02;


    void Start()
    {

    }

    void Update()
    {
        if (Time.time > Next_Time)
        {
            Next_Time = Time.time + Time_Left;

            Fire();
        }
    }

    private void Fire()
    {

        if (Input.GetMouseButton(0))
        {
            StartCoroutine("Gun_Recoil");
        }
    }

    IEnumerator Gun_Recoil()
    {
        transform.position = Vector3.Lerp(gameObject.transform.position, Target_01.transform.position, 0.05f);
        yield return new WaitForSeconds(0.05f);
        transform.position = Vector3.Lerp(gameObject.transform.position, Target_02.transform.position, 0.05f);


    }
}
