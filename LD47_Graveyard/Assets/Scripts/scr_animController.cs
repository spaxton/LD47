﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_animController : MonoBehaviour
{

    public Animator anim;
    public bool doubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (doubleJump)
        {
            anim.Play("ghost_twirl");
        } else
        {
            anim.Play("ghost_idle");
        }
    }
}
