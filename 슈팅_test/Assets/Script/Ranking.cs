using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;


public class Ranking : MonoBehaviour
{
    public static Ranking instance;

    public GameObject rankbord;

    void Start()
    {
        var StreamReader = new StreamReader("Assets/Resources/Ranking.txt");
        string[] TextList = new string[5];
        for (int i = 0; i < 5; i++)
        {
            string TextLine = StreamReader.ReadLine();
            if (TextLine == "")
            {
                TextList[i] = "NONE,0";
            }
            else
                TextList[i] = (TextLine == null) ? "NONE,0" : TextLine;
        }

        for (int k = 0; k < 5; k++)
        {
            GameObject Rankbord = rankbord.transform.GetChild(k).gameObject;
            rankbord.GetComponent<Text>().text = $"{k + 1 }";
            rankbord.transform.GetChild(0).GetComponent<Text>().text = $"{TextList[k].Split(',')[0]}";
            rankbord.transform.GetChild(1).GetComponent<Text>().text = $"{TextList[k].Split(',')[1]}";
        }
    }

    void Update()
    {

    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
}
