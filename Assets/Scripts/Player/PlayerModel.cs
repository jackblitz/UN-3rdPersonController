using UnityEngine;
using System.Collections;
using System.Text;

public class PlayerModel : MonoBehaviour, ConsoleLogs
{
    /**
     * Players Look at Direction
     **/
    public Vector2 LookAtDirection
    {
        get;
        set;
    }

    /**
     * Player Look at Direction
     **/
    public Vector2 MovementDirection
    {
        get;
        set;
    }

    public bool IsRunning
    {
        get;
        set;
    }

    public bool IsCrouched
    {
        get;
        set;
    }

    public bool IsGrounded
    {
        get;
        set;
    }

    public string GetConsoleLog()
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append("Look At Direction: ");
        stringBuilder.Append(LookAtDirection.ToString());

        return stringBuilder.ToString();
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
