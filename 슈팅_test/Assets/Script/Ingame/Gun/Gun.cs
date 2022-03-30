using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject Bullet_Prefab;

    private float Time_Left = 0.2f;
    private float Next_Time = 0.0f;

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
            Vector3 Fire_Pos = transform.position + this.gameObject.transform.forward + new Vector3(0f, 0.5f, 0f);
            var Bullet = Instantiate(Bullet_Prefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
            Bullet.Fire(this.gameObject.transform.forward);
        }
    }
}
