using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleAim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        //transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.x, 0);
        if (Input.GetMouseButton(0))
        {
            transform.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, 0);
        }
        else
        {
            //transform.Rotate(Input.GetAxis("Vertical") * 3 * Time.deltaTime, 0, 0);
        }
    }
}
