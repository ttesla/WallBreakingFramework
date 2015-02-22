//----------------------------------------------
// File: InspireBehaviour.cs
// Copyright © 2014 Inspire13
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;

public class InspireBehaviour : MonoBehaviour
{
    private GameObject mGameObject;
    new public GameObject gameObject
    {
        get
        {
            if (mGameObject == null)
            {
                mGameObject = base.gameObject;
            }

            return mGameObject;
        }
    }

    private Renderer mRenderer;
    new public Renderer renderer
    {
        get
        {
            if (mRenderer == null)
            {
                mRenderer = base.renderer;
            }

            return mRenderer;
        }
    }

    private Transform mTransform;
    new public Transform transform 
    {
        get
        {
            if (mTransform == null) 
            {
                mTransform = base.transform;
            }

            return mTransform;
        }
    }

    private Rigidbody mRigidbody;
    new public Rigidbody rigidbody
    {
        get
        {
            if (mRigidbody == null)
            {
                mRigidbody = base.rigidbody;
            }

            return mRigidbody;
        }
    }

    private Rigidbody2D mRigidbody2D;
    new public Rigidbody2D rigidbody2D
    {
        get
        {
            if (mRigidbody2D == null)
            {
                mRigidbody2D = base.rigidbody2D;
            }

            return mRigidbody2D;
        }
    }

    private Collider mCollider;
    new public Collider collider
    {
        get
        {
            if (mCollider == null)
            {
                mCollider = base.collider;
            }

            return mCollider;
        }
    }

    private Collider2D mCollider2D;
    new public Collider2D collider2D
    {
        get
        {
            if (mCollider2D == null)
            {
                mCollider2D = base.collider2D;
            }

            return mCollider2D;
        }
    }
}
