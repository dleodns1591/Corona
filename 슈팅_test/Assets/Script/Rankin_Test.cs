using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class Rankin_Test : MonoBehaviour
{
    public static Rankin_Test instance;

    public GameObject rankbord;

    void Start()
    {
        var StreamRead = new StreamReader("Assets/Resources/Ranking.txt");
        string[] TextList = new string[5];
        for (int i = 0; i < 5; i++)
        {
            string TextLine = StreamRead.ReadLine();
            TextList[i] = (TextLine == null || TextLine == "") ? "NONE,0" : TextLine;
        }

        for (int k = 0; k < 5; k++)
        {
            GameObject RankBord = rankbord.transform.GetChild(k).gameObject;
            RankBord.GetComponent<Text>().text = $"{k + 1 }";
            RankBord.transform.GetChild(0).GetComponent<Text>().text = $"{TextList[k].Split(',')[0]}";
            RankBord.transform.GetChild(1).GetComponent<Text>().text = $"{TextList[k].Split(',')[1]}";
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
