﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player_movement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float walkSpeed = 4f;

    public float turnSmoothing = 0.1f;
    float turnSmoothVelocity;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(hor, 0f, ver).normalized;

        if (dir.magnitude >= 0.1f)
        {
            float tarAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, tarAngle, ref turnSmoothVelocity, turnSmoothing);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, tarAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * walkSpeed * Time.deltaTime);
        }
    }
}
