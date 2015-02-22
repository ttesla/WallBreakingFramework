//----------------------------------------------
// File: Explosive.cs
// Copyright © 2014 Inspire13
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;

public class Explosive : InspireBehaviour 
{
	#region Fields

    public float Radius;
    public float Power;
    public float UpShift;

	#endregion
	
	#region UnityMethods

    void OnCollisionEnter() 
    {
        Detonate();
        GameObject.Destroy(gameObject);
    }

    #endregion

    #region PrivateMethods

    void Detonate()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, Radius);

        foreach (Collider hit in colliders)
        {
            if (hit && hit.rigidbody) 
            {
                hit.rigidbody.isKinematic = false;
                hit.rigidbody.AddExplosionForce(Power, explosionPos, Radius, UpShift);
            }
        }
    }

    #endregion
}
