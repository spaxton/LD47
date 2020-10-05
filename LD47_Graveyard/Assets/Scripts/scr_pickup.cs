using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_pickup : MonoBehaviour
{
    public Transform handle;
    public bool pickedUp = false;
    public GameObject player;
    public float pickupDist = 1f;
    private float playerDist;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(this.GetComponent<SphereCollider>(), player.GetComponent<CharacterController>(), true);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (pickedUp)
            {

                drop();
            }
            else
            {
                // check how far away the player is before getting picked up
                playerDist = Vector3.Distance(this.transform.position, player.transform.position);
                if((playerDist <= pickupDist) && (player.GetComponent<scr_player_movement>().carrying == false))
                {
                    pickup();
                }
            }
        }

        
        if (pickedUp == true)
        {
            this.transform.position = handle.position;

        }
        
    }

    void pickup()
    {
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        GetComponent<Rigidbody>().freezeRotation = true;
        this.transform.parent = GameObject.Find("Hands").transform;
        this.transform.position = handle.position;
        pickedUp = true;
        player.GetComponent<scr_player_movement>().carrying = true;
    }

    void drop()
    {
        GetComponent<SphereCollider>().enabled = true;
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().freezeRotation = false;
        pickedUp = false;
        player.GetComponent<scr_player_movement>().carrying = false;
    }
}
