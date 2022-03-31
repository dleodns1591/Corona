using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddScore : MonoBehaviour
{
    public static AddScore instance;

    public float Stage01_Point;
    public float Stage02_Point;
    public float Stage_AddPoint;

    void Start()
    {

    }

    void Update()
    {
        Add_Point();
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Add_Point()
    {
        if (SceneManager.GetActiveScene().name == "Stage_01")
        {
            Stage01_Point = UI_Manager.instance.Point_01;
        }
        if (SceneManager.GetActiveScene().name == "Stage_02")
        {
            Stage02_Point = UI_Manager.instance.Point_02;
        }

        Stage_AddPoint = Stage01_Point + Stage02_Point;
    }
}
