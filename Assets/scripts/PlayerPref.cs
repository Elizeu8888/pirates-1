using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPref : MonoBehaviour
{

    public Playercontroller playerscript;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void SaveXP()
    {
        PlayerPrefs.GetFloat("xp", playerscript.xp);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
