using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlSpawn : MonoBehaviour
{

    public GameObject owlprefab;
    public GameObject Owlspawnloc;
    public bool owlspawned = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey("p"))
        {
            GameObject owl = Instantiate(owlprefab, Owlspawnloc.transform.position, Quaternion.identity);

            owlspawned = !owlspawned;




        }




    }
}
