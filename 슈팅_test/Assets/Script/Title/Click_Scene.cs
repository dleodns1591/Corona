using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class Click_Scene : MonoBehaviour
{
    [SerializeField] private GameObject Title_UI;
    [SerializeField] private PlayableDirector Title_pd;
    [SerializeField] private PlayableDirector Start_pd;

    public void Start_Click()
    {
        Title_UI.SetActive(false);

        Title_pd.Stop();
        Start_pd.Play();
    }

    public void Start_Scene()
    {
        SceneManager.LoadScene("Stage_01");
    }

    public void Exit_Btn()
    {
        Application.Quit();
    }
}
