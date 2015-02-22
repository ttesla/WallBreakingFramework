//----------------------------------------------
// File: ShootCannon.cs
// Copyright © 2014 Inspire13
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootCannon : InspireBehaviour
{
    #region Fields

    public Camera MainCam;         // This should be the main camera where you throw balls from
    public GameObject CannonBall;
    public float Force;

    #endregion

    #region Properties

    public bool CanShoot { get; set; }

    #endregion

    #region UnityMethods

    void Update()
    {
        if (MainCam != null) 
        {
            if (Input.GetMouseButtonDown(0) && CanShoot)
            {
                Ray shootRay = MainCam.ScreenPointToRay(Input.mousePosition);
                GameObject cannon = Instantiate(CannonBall, MainCam.transform.position, Quaternion.identity) as GameObject;
                cannon.rigidbody.AddForce(shootRay.direction * Force);
            }
        }
    }

    #endregion
}
