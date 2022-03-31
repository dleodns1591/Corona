using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 고통 게이지 관리 스크립트
public class Pain_Bar : MonoBehaviour
{
    public static Pain_Bar instance;

    [SerializeField]
    private Slider Pain_Hader;

    private float Max_Pain = 100;
    public float Cur_Pain = 10;
    public Text Pain_Text;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Stage_01")
        {
            Cur_Pain = 10f;
        }
        
        if (SceneManager.GetActiveScene().name == "Stage_02")
        {
            Cur_Pain = 30f;
        }

        Pain_Hader.value = (float)Cur_Pain / (float)Max_Pain;
    }

    void Update()
    {

    }

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void Handle_Pain()
    {
        Pain_Hader.value = (float)Cur_Pain / (float)Max_Pain;
    }

    private void OnTriggerEnter(Collider other)
    {

        // Enemy_01 (박테리아) 와 닿았을 경우
        // Enemy_02 (세균) 과 닿았을 경우
        // Enemy_03 (바이러스) 와 닿았을 경우
        // Enemy_04 (암세포) 와 닿았을 경우
        Enemy_Move EM = other.GetComponent<Enemy_Move>();
        if (other.CompareTag("Enemy_01") || other.CompareTag("Enemy_02") || other.CompareTag("Enemy_03") || other.CompareTag("Enemy_04"))
        {
            Invoke("Handle_Pain", 0.01f);
            Cur_Pain += EM.Damage / 2;
            Pain_Text.text = "Pain : " + Cur_Pain + "/ 100";
        }

        if (other.CompareTag("Red_Cell(false)"))
        {
            Invoke("Handle_Pain", 0.01f);
            Cur_Pain += 10;
            Pain_Text.text = "Pain : " + Cur_Pain + "/ 100";
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Red_Cell"))
        {
            Destroy(other.gameObject);

        }

        if (other.CompareTag("Item_04"))
        {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Item_04(false)"))
        {
            if (Cur_Pain >= 0 && Cur_Pain <= 5)
            {
                Invoke("Handle_Pain", 0.01f);
                Cur_Pain = 0;
                Pain_Text.text = "Pain : " + Cur_Pain + "/ 100";
                Destroy(other.gameObject);
            }

            else if (Cur_Pain > 5)
            {
                Invoke("Handle_Pain", 0.01f);
                Cur_Pain -= 5;
                Pain_Text.text = "Pain : " + Cur_Pain + "/ 100";
                Destroy(other.gameObject);
            }
        }

        if (Cur_Pain >= 100)
        {
            Debug.Log("고통 게이지 100");
        }
    }
}
