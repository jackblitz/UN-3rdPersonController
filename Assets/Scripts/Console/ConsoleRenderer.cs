﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConsoleRenderer : MonoBehaviour
{
    public PlayerMotorModel mActiveLog;

    private Text mTextRenderer;

    // Use this for initialization
    void Start()
    {
        mTextRenderer = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mActiveLog != null)
        {
            mTextRenderer.text = mActiveLog.GetConsoleLog();
        }
    }
}
