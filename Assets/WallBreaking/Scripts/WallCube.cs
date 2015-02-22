//----------------------------------------------
// File: WallCube.cs
// Copyright © 2014 Inspire13
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;

public class WallCube : InspireBehaviour 
{
	#region Fields

    public Material TransMaterial;
    public float DestroySpeed;

    private bool mMaterialChanged;
    private bool mHit;
    private float mSleepTimer;
    private bool mCheckSleep;
	private bool mStartDestroy;

	#endregion
	
	#region UnityMethods

	void Start() 
	{
        mMaterialChanged = false;
        mSleepTimer = 0.1f;
        mCheckSleep = false;
        mHit = false;
		mStartDestroy = false;
	}

	void OnCollisionEnter(Collision other)
	{
        if (other.collider.name == "Plane") 
        {
            mStartDestroy = true;
        }
	}
	
	void Update() 
	{
        if (mStartDestroy) 
        {
            if (!mMaterialChanged)
            {
                renderer.material = TransMaterial;
                mMaterialChanged = true;
            }

            renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, renderer.material.color.a - Time.deltaTime * DestroySpeed);

            if (renderer.material.color.a <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        if (!rigidbody.isKinematic && !mHit) 
        {
            mHit = true;
        }

        if (mHit) 
        {
            mSleepTimer -= Time.deltaTime;

            if (mSleepTimer <= 0) 
            {
                mCheckSleep = true;
            }
        }

        if (mCheckSleep && rigidbody.IsSleeping())
        {
            rigidbody.isKinematic = true;
            mCheckSleep = false;
            mHit = false;
            mSleepTimer = 0.1f;
        }
	}
	
	#endregion
}
