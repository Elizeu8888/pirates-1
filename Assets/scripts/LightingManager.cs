using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{


    public Color desertfog, forrestfog;
    public Collider desertArea, forrestArea;
    public GameObject desertPPV, player;
    public Material desertSkyBox, forrestSkyBox;



    public void ChangeBiome(Collider col)
    {
        Collider[] biome = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Player"));
        foreach (Collider c in biome)
        {
            
            if(col == desertArea)
            {
                RenderSettings.fogColor = desertfog;
                //RenderSettings.skybox = desertSkyBox;
                //RenderSettings.fogDensity = 0.005f;
                //desertPPV.active = true;
                
            }

            if (col == forrestArea)
            {
                RenderSettings.fogColor = forrestfog;
                //RenderSettings.skybox = forrestSkyBox;
                //RenderSettings.fogDensity = 0.003f;
                //desertPPV.active = false;

            }

            if (c.gameObject == player)
            {
                
            }

        }

    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        ChangeBiome(desertArea);
        ChangeBiome(forrestArea);
    }
}
