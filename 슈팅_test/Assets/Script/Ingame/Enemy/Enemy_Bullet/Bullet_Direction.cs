using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Direction : MonoBehaviour
{
    public GameObject Bullet;
    public Transform Fire_Pos_01;
    public Transform Fire_Pos_02;

    private void Start()
    {
        InvokeRepeating("Bullet_Launch", 2f, 1f);

    }

    void Update()
    {

    }

    private void Bullet_Launch()
    {
        Instantiate(Bullet, Fire_Pos_01.position, Fire_Pos_01.transform.rotation);
        Instantiate(Bullet, Fire_Pos_02.position, Fire_Pos_02.transform.rotation);
    }
}
