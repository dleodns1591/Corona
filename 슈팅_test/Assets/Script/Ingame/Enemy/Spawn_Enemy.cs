using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    [Header("박테리아")]
    public GameObject Enemy_Prefab_01;

    [Header("세균")]
    public GameObject Enemy_Prefab_02;

    [Header("바이러스")]
    public GameObject Enemy_Prefab_03;

    [Header("암세포")]
    public GameObject Enemy_Prefab_04;

    void Start()
    {
        InvokeRepeating("Enemy01_Spawn", 3f, 1.5f);
        InvokeRepeating("Enemy02_Spawn", 10f, 8f);
    }

    void Update()
    {

    }

    private void Enemy01_Spawn()
    {
        float Random_Z = Random.Range(500f, 620f);
        float Random_Y = Random.Range(70f, 100f);
        float Random_X = Random.Range(500f, 620f);
        GameObject Enemy = (GameObject)Instantiate(Enemy_Prefab_01, new Vector3(Random_X,Random_Y,Random_Z), Quaternion.identity);
    }

    private void Enemy02_Spawn()
    {
        float Random_Z = Random.Range(500f, 620f);
        float Random_X = Random.Range(500f, 620f);
        GameObject Enemy = (GameObject)Instantiate(Enemy_Prefab_02, new Vector3(Random_X, 80f, Random_Z), Quaternion.identity);
    }

    private void Enemy03_Spawn()
    {

    }

    private void Enemy04_Spawn()
    {

    }
}
