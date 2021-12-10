using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpNumber : MonoBehaviour
{
    public GameObject player;

    Playercontroller playerscript;

    public Text textObject;

    // Start is called before the first frame update
    void Start()
    {
        playerscript = player.GetComponent<Playercontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        textObject.text = playerscript.xp.ToString();
    }
}
