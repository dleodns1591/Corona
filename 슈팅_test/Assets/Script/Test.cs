using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.EventSystems;


public class Test : MonoBehaviour
{
    public GameObject rankbode;
    public float Score;
    public string Name;
    private string[] TextList = new string[5];
    public InputField NicName;

    void Start()
    {
        var StreamRead = new StreamReader("Assets/Resources/Ranking.txt");
        for (int i = 0; i < 5; i++)
        {
            string TextLine = StreamRead.ReadLine();
            TextList[i] = (TextLine == null || TextLine == "") ? "NONE,0" : TextLine;
        }

        if (rankbode != null)
        {
            for (int k = 0; k < 5; k++)
            {
                GameObject Rankbode = rankbode.transform.GetChild(k).gameObject;
                Rankbode.GetComponent<Text>().text = $"{k + 1}";
                Rankbode.transform.GetChild(0).GetComponent<Text>().text = $"{TextList[k].Split(',')[0]}";
                Rankbode.transform.GetChild(1).GetComponent<Text>().text = $"{TextList[k].Split(',')[1]}";
            }
        }
    }

    void Update()
    {
        
    }

    public void InputName()
    {
        Score = AddScore.instance.Stage_AddPoint;
        Name = NicName.GetComponent<InputField>().text;
        StartCoroutine("AddRank");
    }

    IEnumerator AddRank()
    {
        StreamWriter sw = new StreamWriter("Assets/Resources/Ranking.txt");
        for (int i = 0; i < 5; i++)
        {
            string[] TextScore = TextList[i].Split(',');
            if (int.Parse(TextScore[i]) < Score)
            {
                for (int k = 3; k >= i; k--)
                {
                    TextList[k + 1] = TextList[k];
                }
                TextList[i] = Name + "," + Score;
                Score = 0;
            }
            sw.WriteLine(TextList[i]);
        }
        sw.Close();
        SceneManager.LoadScene("Title");
        yield return null;
    }
}
