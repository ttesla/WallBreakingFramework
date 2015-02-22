//----------------------------------------------
// File: SimManager.cs
// Copyright © 2014 Inspire13
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// You may throw away this code, this is just some gui code to demonstrate how wall builder works
/// </summary>
public class SimManager : InspireBehaviour 
{
	#region Fields

    public WallCreator WallMaker;
    public ShootCannon Cannon;

    private const int GuiBoxWidth = 300;
    private const int GuiBoxHeight = 450;
    private const int GuiWidth = 100;
    private const int GuiHeight = 25;
    private const int GuiOffSet = 5;
    private const int GuiHeightGap = 1;
    private const int GuiNextHeight = GuiHeight + GuiHeightGap;
    private const int GuiButtonHeight = 25;
    private const int GuiButtonWidth = 25;
    private const int GuiWidthGap = 1;
    private const int GuiNextWidth = GuiButtonWidth + GuiWidthGap;

    private Light mDirectionalLight;
    private Light mSpotLight1;
    private Light mSpotLight2;
    private int mMaterialsIndex;
    private int mRenderingPathIndex;
    private string mTextureQualityStr;
    private string mAntialiasingStr;
    private string mMaterialStr;
    private GameObject[] mPerfSpheres;
    private bool mLightMap;
    private bool mBlobShadow;
    LightmapData[] mLightmapData;
    private bool mFastShadow;

    private int mWallWidth;
    private int mWallHeight;
    private int mWallTick;
    private int mQuality;
    private bool mBrickShadow;
    private bool mRealtimeGen;
    private bool mSimStarted;

	#endregion
	
	#region UnityMethods

	void Start() 
	{
        mWallWidth   = 30;
        mWallHeight  = 25;
        mWallTick    = 2;
        mSimStarted = false;
        mQuality = QualitySettings.GetQualityLevel();
        mBrickShadow = WallMaker.Brick.renderer.castShadows;
        mRealtimeGen = false;
	}

    void OnGUI() 
    {
        float heightOffset = 0;
        float widthOffset = 0;

        if (!mSimStarted) 
        {
            heightOffset += GuiNextHeight;
            widthOffset = 0;

            GUI.Label(new Rect(GuiOffSet, heightOffset, 800, 25), "If you make a very big wall, your computer may cry! Total Bricks: " + (mWallWidth * mWallHeight * mWallTick));

            heightOffset += GuiNextHeight;

            // WIDTH
            GUI.Label(new Rect(GuiOffSet, heightOffset, GuiWidth, GuiHeight), "Wall Width: " + mWallWidth, GUI.skin.textField);
            widthOffset = 2 * GuiOffSet + GuiWidth;
            bool decWallWidth = GUI.Button(new Rect(widthOffset, heightOffset, GuiButtonWidth, GuiButtonHeight), "-");
            widthOffset += GuiNextWidth;
            bool incWallWidth = GUI.Button(new Rect(widthOffset, heightOffset, GuiButtonWidth, GuiButtonHeight), "+");

            if (decWallWidth)
                mWallWidth = mWallWidth > 0 ? mWallWidth - 1 : mWallWidth;
            else if (incWallWidth)
                mWallWidth++;

            // HEIGHT
            heightOffset += GuiNextHeight;
            GUI.Label(new Rect(GuiOffSet, heightOffset, GuiWidth, GuiHeight), "Wall Height: " + mWallHeight, GUI.skin.textField);
            widthOffset = 2 * GuiOffSet + GuiWidth;
            bool decWallHeight = GUI.Button(new Rect(widthOffset, heightOffset, GuiButtonWidth, GuiButtonHeight), "-");
            widthOffset += GuiNextWidth;
            bool incWallHeight = GUI.Button(new Rect(widthOffset, heightOffset, GuiButtonWidth, GuiButtonHeight), "+");

            if (decWallHeight)
                mWallHeight = mWallHeight > 0 ? mWallHeight - 1 : mWallHeight;
            else if (incWallHeight)
                mWallHeight++;

            // TICK
            heightOffset += GuiNextHeight;
            GUI.Label(new Rect(GuiOffSet, heightOffset, GuiWidth, GuiHeight), "Wall Tick: " + mWallTick, GUI.skin.textField);
            widthOffset = 2 * GuiOffSet + GuiWidth;
            bool decWallTick = GUI.Button(new Rect(widthOffset, heightOffset, GuiButtonWidth, GuiButtonHeight), "-");
            widthOffset += GuiNextWidth;
            bool incWallTick = GUI.Button(new Rect(widthOffset, heightOffset, GuiButtonWidth, GuiButtonHeight), "+");

            if (decWallTick)
                mWallTick = mWallTick > 0 ? mWallTick - 1 : mWallTick;
            else if (incWallTick)
                mWallTick++;

            // QUALITY
            heightOffset += GuiNextHeight;
            GUI.Label(new Rect(GuiOffSet, heightOffset, GuiWidth, GuiHeight), "Quality: " + mQuality, GUI.skin.textField);
            widthOffset = 2 * GuiOffSet + GuiWidth;
            bool decQuality = GUI.Button(new Rect(widthOffset, heightOffset, GuiButtonWidth, GuiButtonHeight), "-");
            widthOffset += GuiNextWidth;
            bool incQuality = GUI.Button(new Rect(widthOffset, heightOffset, GuiButtonWidth, GuiButtonHeight), "+");

            if (decQuality)
            {
                QualitySettings.DecreaseLevel(true);
                mQuality = QualitySettings.GetQualityLevel();
            }
            else if (incQuality)
            {
                QualitySettings.IncreaseLevel(true);
                mQuality = QualitySettings.GetQualityLevel();
            }

            // BRICK SHADOW
            heightOffset += GuiNextHeight;
            GUI.Label(new Rect(GuiOffSet, heightOffset, GuiWidth * 1.5f, GuiHeight), "Brick Shadows: " + mBrickShadow, GUI.skin.textField);
            widthOffset = 2 * GuiOffSet + GuiWidth * 1.5f;
            bool brickShadowOff = GUI.Button(new Rect(widthOffset, heightOffset, GuiButtonWidth*1.5f, GuiButtonHeight), "Off");
            widthOffset += GuiNextWidth * 1.5f;
            bool brickShadowOn = GUI.Button(new Rect(widthOffset, heightOffset, GuiButtonWidth * 1.5f, GuiButtonHeight), "On");

            if (brickShadowOff)
            {
                mBrickShadow = false;
                WallMaker.Brick.renderer.castShadows = mBrickShadow;
            }
            else if (brickShadowOn)
            {
                mBrickShadow = true;
                WallMaker.Brick.renderer.castShadows = mBrickShadow;
            }

            // REALTIME WALL GENERATION
            heightOffset += GuiNextHeight;
            GUI.Label(new Rect(GuiOffSet, heightOffset, GuiWidth * 1.5f, GuiHeight), "Realtime Generate: " + mRealtimeGen, GUI.skin.textField);
            widthOffset = 2 * GuiOffSet + GuiWidth * 1.5f;
            bool realtimeOff = GUI.Button(new Rect(widthOffset, heightOffset, GuiButtonWidth * 1.5f, GuiButtonHeight), "Off");
            widthOffset += GuiNextWidth * 1.5f;
            bool realtimeOn = GUI.Button(new Rect(widthOffset, heightOffset, GuiButtonWidth * 1.5f, GuiButtonHeight), "On");

            if (realtimeOff)
            {
                mRealtimeGen = false;
            }
            else if (realtimeOn)
            {
                mRealtimeGen = true;
            }

            heightOffset += GuiNextHeight + 10;
            bool generateWall = GUI.Button(new Rect(GuiOffSet, heightOffset, 100, 40), "Generate Wall");

            if (generateWall)
            {
                if(mRealtimeGen)
                    StartCoroutine(WallMaker.GenerateWallRealtime(mWallWidth, mWallHeight, mWallTick));    
                else
                    WallMaker.GenerateWall(mWallWidth, mWallHeight, mWallTick);
                
                Cannon.CanShoot = true;
                mSimStarted = true;
            }
        }
        else 
        {
            bool restartSim = GUI.Button(new Rect(GuiOffSet, Screen.height - 30, 100, 25), "RESTART");

            if (restartSim) 
            {
                // Restart scene! (the best and easiest way to restart things)
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    #endregion
}
