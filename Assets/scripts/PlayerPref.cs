using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPref : MonoBehaviour
{

    public Playercontroller playerscript;

    void Start()
    {
        
    }

    void SaveXP()
    {
        PlayerPrefs.GetFloat("xp", playerscript.xp);
    }

    void Update()
    {
        
    }

}
