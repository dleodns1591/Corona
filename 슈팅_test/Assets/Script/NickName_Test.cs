using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.SceneManagement;

public class NickName_Test : MonoBehaviour
{
    public InputField NicName;
    public float _Score;
    public string Name;
    public GameObject rankbord;
    string[] TextList = new string[5];

    void Start()
    {
        var StreamRead = new StreamReader("Assets/Resources/Ranking.txt");
        for (int i = 0; i < 5; i++)
        {
            string TextLine = StreamRead.ReadLine();
            TextList[i] = (TextLine == null || TextLine == "") ? "NONE,0" : TextLine;
        }

        if (rankbord == null)
        {
            for (int k = 0; k < 5; k++)
            {
                GameObject Rankbord = rankbord.transform.GetChild(k).gameObject;
                Rankbord.GetComponent<Text>().text = $"{ k + 1}";
                Rankbord.transform.GetChild(0).GetComponent<Text>().text = $"{TextList[k].Split(',')[0]}";
                Rankbord.transform.GetChild(1).GetComponent<Text>().text = $"{TextList[k].Split(',')[1]}";
            }
        }
    }

    void Update()
    {

    }

    public void Input_Name()
    {
        _Score = AddScore.instance.Stage_AddPoint;
        Name = NicName.GetComponent<InputField>().text;
        StartCoroutine("AddRank");
    }

    IEnumerator AddRank()
    {
        StreamWriter sw = new StreamWriter("Assets/Resources/Ranking.txt");
        for (int i = 0; i < 5; i++)
        {
            string[] TextScore = TextList[i].Split(',');
            if (int.Parse(TextScore[i]) < _Score)
            {
                for (int k = 0; k < 5; i++)
                {
                    TextList[k + 1] = TextList[k];
                }
                TextList[i] = name + ",";
                _Score = 0f;
            }
            sw.WriteLine(TextList[i]);

        }
        sw.Close();
        SceneManager.LoadScene("Title");
        yield return null;
    }
}
