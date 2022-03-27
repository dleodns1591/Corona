using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn_Enemy : MonoBehaviour
{
    [Header("���׸���")]
    public GameObject Enemy_Prefab_01;

    [Header("����")]
    public GameObject Enemy_Prefab_02;

    [Header("������")]
    public GameObject White_Blood;

    [Header("������")]
    public GameObject Red_Blood;

    [Header("����_01")]
    public GameObject Boss_01;

    void Start()
    {
        InvokeRepeating("Probability_White", 1f, 10f);
        InvokeRepeating("Probability_Red", 1f, 20f);
        InvokeRepeating("Enemy01_Spawn", 3f, 1.5f);
        InvokeRepeating("Enemy02_Spawn", 10f, 8);
    }

    void Update()
    {

    }

    private void Enemy01_Spawn()
    {
        if (UI_Manager.instance.EnemyDie_Point <= -1)
        {
            float Random_Z = Random.Range(500f, 620f);
            float Random_Y = Random.Range(70f, 100f);
            float Random_X = Random.Range(500f, 620f);
            GameObject Enemy = (GameObject)Instantiate(Enemy_Prefab_01, new Vector3(Random_X, Random_Y, Random_Z), Quaternion.identity);
        }
    }

    private void Enemy02_Spawn()
    {
        if (UI_Manager.instance.EnemyDie_Point <= -1)
        {
            float Random_Z = Random.Range(500f, 620f);
            float Random_X = Random.Range(500f, 620f);
            GameObject Enemy = (GameObject)Instantiate(Enemy_Prefab_02, new Vector3(Random_X, 80f, Random_Z), Quaternion.identity);
        }
    }

    public void Boss()
    {
    }

    public void White_Cell()
    {
        float Random_Z = Random.Range(500f, 620f);
        float Random_X = Random.Range(460f, 630f);
        GameObject Enemy = (GameObject)Instantiate(White_Blood, new Vector3(Random_X, 56f, Random_Z), Quaternion.identity);
    }

    public void Red_Cell()
    {
        float Random_Z = Random.Range(500f, 620f);
        float Random_X = Random.Range(460f, 630f);
        GameObject Enemy = (GameObject)Instantiate(Red_Blood, new Vector3(Random_X, 56f, Random_Z), Quaternion.identity);
    }

    public void Probability_White()
    {
        int Probability_White = Random.Range(1, 101);
        //print(Probability_White);

        // 30% Ȯ���� ������ ����
        if (Probability_White <= 30)
        {
            White_Cell();
        }
    }

    public void Probability_Red()
    {
        int Probability_Red = Random.Range(1, 101);
        //print(Probability_Red);

        // 20% Ȯ���� ������ ����
        if (Probability_Red <= 20)
        {
            Red_Cell();
        }
    }
}
