using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player_movement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float walkSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public float turnSmoothing = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 vel;
    bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // are you on the ground? Act like it
        onGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

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
        }

        if(Input.GetButtonDown("Jump") && onGround)
        {
            vel.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // under pressure (gravitational pressure, that is)
        vel.y += gravity * Time.deltaTime;

        controller.Move(vel * Time.deltaTime);
    }
}
