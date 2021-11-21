using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappleaim : MonoBehaviour
{
    float h = Input.mousePosition.x - Screen.width / 2;
    float v = Input.mousePosition.y - Screen.height / 2;
    float angle = -Mathf.Atan2(v, h) * Mathf.Rad2Deg;

    




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(270, angle, 0);
    }
}
