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
    public GameObject end_loc;
    private float doneDist;
    private bool done = false;
    private bool scored = false;

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
                if((playerDist <= pickupDist) && (player.GetComponent<scr_player_movement>().carrying == false) && (done == false))
                {
                    pickup();
                }
            }
        }

        
        if (pickedUp == true)
        {
            this.transform.position = handle.position;
        } else
        {
            doneDist = Vector3.Distance(this.transform.position, end_loc.transform.position);
            if (doneDist <= pickupDist)
            {
                this.transform.position = end_loc.transform.position;
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                this.transform.rotation = end_loc.transform.rotation;
                
                if( scored == false)
                {
                    scoreUp();
                }
            }
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

        FMODUnity.RuntimeManager.PlayOneShotAttached ("event:/SFX/SkullPickup", this.player);
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

    void OnCollisionEnter (Collision col)
    {
        FMODUnity.RuntimeManager.PlayOneShot ("event:/SFX/SkullPutdown", GetComponent<Transform> ().position);
    }

    void scoreUp()
    {
        player.GetComponent<scr_player_movement>().skullScore++;
        scored = true;
        done = true;
        FMODUnity.RuntimeManager.PlayOneShot ("event:/SFX/SkullPutdownCorrect", GetComponent<Transform> ().position);
    }
}
