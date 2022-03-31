using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    [Header("인게임 UI")]
    public Text Score_Text;
    public Text Enemy_Die_Text;

    [Header("클리어 UI")]
    public Text Stage01_Score;
    public Text Stage01_EnemyDie;
    public Text Stage01_HP;
    public Text Stage01_Pain;
    public Text Stage01_Item;
    public Text Stage01_Point;

    [Header("점수")]
    public float Score_Point;
    public float EnemyDie_Point;
    public float HP_01;
    public float Pain_01;
    public float Item_01;
    public float Point_01;
    public float Point_02;

    void Start()
    {

    }

    void Update()
    {
        BossDie();
        HP_Pain_Point();
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    public void Score(int Score)
    {
        Score_Point += Score;
        Score_Text.text = "Score ; " + Score_Point.ToString();
    }
    public void EnemyDie(int Enemy)
    {
        EnemyDie_Point += Enemy;
        Enemy_Die_Text.text = "Enemy Die : " + EnemyDie_Point.ToString();
    }

    public void Item(int Item)
    {
        Item_01 += Item;
    }

    public void HP_Pain_Point()
    {
        if (Boss.instance.Cur_Hp >= 1)
        {
            HP_01 = Player_Move.instance.Cur_Hp;
            Pain_01 = 100f - Pain_Bar.instance.Cur_Pain;

            Point_01 = Score_Point + EnemyDie_Point + HP_01 + Pain_01 + Item_01;

            if (SceneManager.GetActiveScene().name == "Stage_02")
            {
                Point_02 = Score_Point + EnemyDie_Point + HP_01 + Pain_01 + Item_01;
            }
        }
    }

    public void Stage01_Clear_UI()
    {
        Stage01_Score.text = "Score : " + Score_Point.ToString();
        Stage01_EnemyDie.text = "Enemy Die : " + EnemyDie_Point.ToString();
        Stage01_HP.text = "HP : " + HP_01.ToString();
        Stage01_Pain.text = "Pain : " + Pain_01.ToString();
        Stage01_Item.text = "Item : " + Item_01.ToString();
        Stage01_Point.text = "Point : " + Point_01.ToString();
    }


    public void BossDie()
    {
        if (Boss.instance.Cur_Hp <= 0)
        {
            transform.GetChild(7).gameObject.SetActive(true);
            Stage01_Clear_UI();
        }
        else
        {
            transform.GetChild(7).gameObject.SetActive(false);
        }
    }

    public void Stage01_Clear_Button()
    {
        if (SceneManager.GetActiveScene().name == "Stage_01")
        {
            SceneManager.LoadScene("Stage_02");
        }
        else if (SceneManager.GetActiveScene().name == "Stage_02")
        {
            transform.GetChild(8).gameObject.SetActive(true);
        }
    }

    public void NicName()
    {
        SceneManager.LoadScene("Title");
    }
}
