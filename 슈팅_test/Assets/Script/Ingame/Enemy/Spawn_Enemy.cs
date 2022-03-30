using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [Header("백혈구")]
    public GameObject White_Blood;

    [Header("적혈구")]
    public GameObject Red_Blood;

    void Start()
    {
        InvokeRepeating("Probability_White", 10f, 10f);
        InvokeRepeating("Probability_Red", 15f, 25f);
        InvokeRepeating("Enemy01_Spawn", 3f, 1.5f);
        InvokeRepeating("Enemy02_Spawn", 10f, 4f);
        InvokeRepeating("Enemy03_Spawn", 3f, 2f);
        InvokeRepeating("Enemy04_Spawn", 10f, 4f);
    }

    void Update()
    {

    }

    private void Enemy01_Spawn()
    {
        if (SceneManager.GetActiveScene().name == "Stage_01")
        {
            if (UI_Manager.instance.EnemyDie_Point <= 30 && Boss.instance.All_Enemy_Check == true)
            {
                float Random_Z = Random.Range(500f, 620f);
                float Random_Y = Random.Range(70f, 100f);
                float Random_X = Random.Range(500f, 620f);
                GameObject Enemy = Instantiate(Enemy_Prefab_01, new Vector3(Random_X, Random_Y, Random_Z), Quaternion.identity);
            }
        }
    }

    private void Enemy02_Spawn()
    {
        if (SceneManager.GetActiveScene().name == "Stage_01")
        {
            if (UI_Manager.instance.EnemyDie_Point <= 30 && Boss.instance.All_Enemy_Check == true)
            {
                float Random_Z = Random.Range(500f, 620f);
                float Random_X = Random.Range(500f, 620f);
                GameObject Enemy = Instantiate(Enemy_Prefab_02, new Vector3(Random_X, 80f, Random_Z), Quaternion.identity);
            }
        }
    }

    private void Enemy03_Spawn()
    {
        if (SceneManager.GetActiveScene().name == "Stage_02")
        {
            if (UI_Manager.instance.EnemyDie_Point <= 30 && Boss.instance.All_Enemy_Check == true)
            {
                float Random_X = Random.Range(547, 724f);
                GameObject Enemy = Instantiate(Enemy_Prefab_03, new Vector3(Random_X, -150f, 1418), Quaternion.identity);
            }
        }
    }

    private void Enemy_04_Spawn()
    {
        if (SceneManager.GetActiveScene().name == "Stage_02")
        {
            if (UI_Manager.instance.EnemyDie_Point <= 30 && Boss.instance.All_Enemy_Check == true)
            {
                float Random_X = Random.Range(547, 724f);
                GameObject Enemy = Instantiate(Enemy_Prefab_04, new Vector3(Random_X, -150f, 1418), Quaternion.identity);
            }
        }
    }

    public void White_Cell()
    {
        float Random_Z = Random.Range(500f, 620f);
        float Random_X = Random.Range(460f, 630f);
        GameObject Enemy = Instantiate(White_Blood, new Vector3(Random_X, 56f, Random_Z), Quaternion.identity);
    }

    public void Red_Cell()
    {
        if (UI_Manager.instance.EnemyDie_Point <= 30 && Boss.instance.All_Enemy_Check == true)
        {
            float Random_Z = Random.Range(500f, 620f);
            float Random_X = Random.Range(460f, 630f);
            GameObject Enemy = (GameObject)Instantiate(Red_Blood, new Vector3(Random_X, 56f, Random_Z), Quaternion.identity);
        }
    }

    public void Probability_White()
    {
        int Probability_White = Random.Range(1, 101);
        //print(Probability_White);

        // 30% 확률로 백혈구 생성
        if (Probability_White <= 30)
        {
            White_Cell();
        }
    }

    public void Probability_Red()
    {
        int Probability_Red = Random.Range(1, 101);
        //print(Probability_Red);

        // 15% 확률로 적혈구 생성
        if (Probability_Red <= 15)
        {
            Red_Cell();
        }
    }
}
