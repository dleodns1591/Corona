using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class Boss_Test : MonoBehaviour
{
    [Header("ü�� UI")]
    public Slider Boss_Sldier;
    public Text Boss_Text;
    public float Cur_BossHP;
    public float Max_BossHP;

    [Header("��ų ������")]
    public GameObject Bullet_Prefab;
    public GameObject Long_Tree;

    [Header("��ų ��ġ")]
    public Transform Target;

    [Header("���� �̵�")]
    public Vector3 Move = new Vector3(1f, 1f, 1f);

    [Header("���� �ó׸���")]
    public PlayableDirector Boss_Chinamasine;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Move_01()
    {
        if (UI_Manager.instance.EnemyDie_Point >= 30)
        {
            Vector3 velo = Vector3.zero;
            transform.position = Vector3.Lerp(transform.position, Move, 0.05f);

            Boss_Chinamasine.Play();
        }
    }

    IEnumerator Skill_01()
    {
        yield return new WaitForSeconds(3f);

        float Count = 30f;
        float Weight_Angle = 360f;
        float Internal_Angle = 0f;

        while (true)
        {
            for (int i = 0; i < Count; i++)
            {
                GameObject Clone = Instantiate(Bullet_Prefab, transform.position, Quaternion.identity);

                float Angle = Weight_Angle + Internal_Angle * i;
                float x = Mathf.Cos(Angle * Mathf.PI / 180f);
                float z = Mathf.Sin(Angle * Mathf.PI / 180f);

                Clone.GetComponent<MoveMent>().MoveTo(new Vector3(x, 0f, z));
            }

            Weight_Angle += 1f;
            yield return new WaitForSeconds(1.8f);
        }
    }

    IEnumerator Skill_02()
    {
        yield return null;
        yield return new WaitForSeconds(3f);
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        transform.GetChild(0).gameObject.SetActive(false);
        Instantiate(Long_Tree, Target.position, Target.transform.rotation);
    }

}
