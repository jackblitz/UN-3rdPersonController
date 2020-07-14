using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConsoleRenderer : MonoBehaviour
{
    public PlayerModel mActiveLog;

    private Text mTextRenderer;

    // Use this for initialization
    void Start()
    {
        mTextRenderer = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        mTextRenderer.text = mActiveLog.GetConsoleLog();
    }
}
