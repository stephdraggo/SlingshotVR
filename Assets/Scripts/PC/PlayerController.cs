using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        GetComponentInChildren<MouseLook>().EnableMouseLook(transform);
    }
}
