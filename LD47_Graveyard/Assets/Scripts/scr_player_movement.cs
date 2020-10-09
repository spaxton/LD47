using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_player_movement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float walkSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float doubleJumpHeight = 2f;

    public float turnSmoothing = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool carrying = false;

    public GameObject ghostModel;
    public int skullScore = 0;

    public Canvas canvas;
    public Text skullUI;
    public GameObject PopupPrefab;
    private bool ended = false;

    Vector3 vel;
    bool onGround;
    bool doubleJumped = false;

    FMOD.Studio.EventInstance GhostFloat;
    FMOD.Studio.EventInstance MusicLofi;
    // Start is called before the first frame update
    void Start()
    {
        GameObject pop = Instantiate(PopupPrefab, new Vector3(Screen.width / 2, Screen.height / 2, 0), new Quaternion(0, 0, 0, 0));
        pop.transform.SetParent(canvas.transform, false);

        GhostFloat = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/GhostFloat");
        GhostFloat.start();
        GhostFloat.release();

        MusicLofi = FMODUnity.RuntimeManager.CreateInstance("event:/Music/MusicLofi");
        MusicLofi.start();
        MusicLofi.release();
    }

    // Update is called once per frame
    void Update()
    {
        // are you on the ground? Act like it
        onGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (onGround) {
            doubleJumped = false;
            ghostModel.GetComponent<scr_animController>().doubleJump = false;
        }

        if(onGround && vel.y <0)
        {
            vel.y = -2f;
        }

        // Is the player trying to move?
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(hor, 0f, ver).normalized;

        
        // if you're moving, move, and look like you're moving
        if (dir.magnitude >= 0.1f)
        {
            float tarAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, tarAngle, ref turnSmoothVelocity, turnSmoothing);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, tarAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * walkSpeed * Time.deltaTime);

            GhostFloat.setParameterByName("Floating", 1f);
        } 
        else
        {
            GhostFloat.setParameterByName("Floating", 0f);
        }

        // regular jumps
        if(Input.GetButtonDown("Jump") && onGround)
        {
            vel.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            FMODUnity.RuntimeManager.PlayOneShotAttached ("event:/SFX/GhostJump", this.ghostModel);
        }

        // double jumps
        if (Input.GetButtonDown("Jump") && !onGround && !doubleJumped)
        {
            vel.y = Mathf.Sqrt(doubleJumpHeight * -2f * gravity);
            ghostModel.GetComponent<scr_animController>().doubleJump = true;
            doubleJumped = true;

            FMODUnity.RuntimeManager.PlayOneShotAttached ("event:/SFX/GhostJump", this.ghostModel);
        }

        // under pressure (gravitational pressure, that is)
        vel.y += gravity * Time.deltaTime;

        controller.Move(vel * Time.deltaTime);

        // exit strategy
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // gotta stay up to date!
        updateUI();
    }

    void updateUI()
    {
        skullUI.text = skullScore + "/4";
        MusicLofi.setParameterByName("Skulls", skullScore);
        if (skullScore == 4 && ended == false)
        {
            GameObject endgame = Instantiate(PopupPrefab, new Vector3(Screen.width / 2, Screen.height / 2, 0), new Quaternion(0, 0, 0, 0));
            endgame.transform.SetParent(canvas.transform, false);
            ended = true;
        }
    }
    void OnDestroy()
    {
        MusicLofi.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
   
}
