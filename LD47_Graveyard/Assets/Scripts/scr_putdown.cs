using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_putdown : MonoBehaviour
{
    public GameObject mySkull;
    private GameObject sphere;

    // Start is called before the first frame update
    void Start()
    {

        sphere = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (mySkull.GetComponent<scr_pickup>().pickedUp)
        {
            sphere.GetComponent<MeshRenderer>().enabled = true;

            
        } else
        {
            sphere.GetComponent<MeshRenderer>().enabled = false;

           //Where??? FMODUnity.RuntimeManager.PlayOneShotAttached ("event:/SFX/SkullPutdownCorrect", this.sphere);
        }
    }
}
