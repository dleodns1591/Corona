using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Direction_02 : MonoBehaviour
{
    public GameObject Bullet;
    public Transform Fire_Pos_01;
    public Transform Fire_Pos_02;
    public Transform Fire_Pos_03;
    public Transform Fire_Pos_04;

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
        Instantiate(Bullet, Fire_Pos_03.position, Fire_Pos_03.transform.rotation);
        Instantiate(Bullet, Fire_Pos_04.position, Fire_Pos_04.transform.rotation);
    }
}
