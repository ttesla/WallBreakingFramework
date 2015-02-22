//----------------------------------------------
// File: WallCreator.cs
// Copyright © 2014 Inspire13
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;

public class WallCreator : InspireBehaviour 
{
	#region Fields

    public Vector3 StartPos;
    public GameObject Brick;
    public int Width;
    public int Height;
    public int Tick;
	public bool Zigzag;

	#endregion
	
	#region UnityMethods

	#endregion

    #region PublicMethods

    public void GenerateWall(int wallWidth, int wallHeight, int tick) 
    {
        Width  = wallWidth;
        Height = wallHeight;
        Tick   = tick;

        float voxelX = Brick.renderer.bounds.size.x;
        float voxelY = Brick.renderer.bounds.size.y;
        float voxelZ = Brick.renderer.bounds.size.z;

        for (int z = 0; z < Tick; z++)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Instantiate(Brick, new Vector3(StartPos.x + x * voxelX + (Zigzag ? (voxelX / 2) * (y % 2 + z % 2) : 0),
                                                   StartPos.y + y * voxelY,
                                                   StartPos.z + z * voxelZ),
                                                   Quaternion.identity);
                }
            }
        }
    }

    public IEnumerator GenerateWallRealtime(int wallWidth, int wallHeight, int tick)
    {
        Width = wallWidth;
        Height = wallHeight;
        Tick = tick;

        float voxelX = Brick.renderer.bounds.size.x;
        float voxelY = Brick.renderer.bounds.size.y;
        float voxelZ = Brick.renderer.bounds.size.z;

        for (int z = 0; z < Tick; z++)
        {
            for (int y = 0; y < Height; y++)
            {
                // Try this for generating line by line
                //yield return new WaitForSeconds(0.05f);

                for (int x = 0; x < Width; x++)
                {
                    Instantiate(Brick, new Vector3(StartPos.x + x * voxelX + (Zigzag ? (voxelX / 2) * (y % 2 + z % 2) : 0),
                                                   StartPos.y + y * voxelY,
                                                   StartPos.z + z * voxelZ),
                                                   Quaternion.identity);

                    yield return new WaitForSeconds(0.01f);
                }
            }
        }
    }

    #endregion
}
