using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalScript : MonoBehaviour
{

    public Animator anim;
    public Animation ani;

    Playercontroller playscript;
    public GameObject player;

    public ParticleSystem speedfire;
    public GameObject playermesh;

    string day = System.DateTime.Now.ToString("dd");
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;

    
    public void Start()
    {

        playscript = player.GetComponent<Playercontroller>();

        speedfire = playermesh.GetComponent<ParticleSystem>();
        speedfire.enableEmission = false;

        button1 = button1.GetComponent<Button>();
        button2 = button2.GetComponent<Button>();
        button3 = button3.GetComponent<Button>();
        button4 = button4.GetComponent<Button>();
        button5 = button5.GetComponent<Button>();
        button6 = button6.GetComponent<Button>();
    }

   
    void Update()
    {
        if(day == "13")
        {
            
        }
        button1.onClick.AddListener(TaskOnClick);



    }


    void TaskOnClick()
    {
        anim.SetFloat("runspeed", 2);
        playscript.speed = playscript.speed + 10.0f;
        speedfire.enableEmission = true;
    }
}
