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

    public float Speed
    {
        get;
        set;
    }

    public bool IsAttacking { get; set; }

    public bool IsLocked { get; set; }

    public string GetConsoleLog()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(this.name);
        stringBuilder.Append("\n");
        stringBuilder.Append("Look At Direction: ");
        stringBuilder.Append(LookAtDirection.ToString());
        stringBuilder.Append("\n");
        stringBuilder.Append("Movement Direction: ");
        stringBuilder.Append(MovementDirection.ToString());
        stringBuilder.AppendLine();
        stringBuilder.Append("Is Running: ");
        stringBuilder.Append(IsRunning);
        stringBuilder.AppendLine();
        stringBuilder.Append("Is Crouched: ");
        stringBuilder.Append(IsCrouched);
        stringBuilder.AppendLine();
        stringBuilder.Append("Is Grounded: ");
        stringBuilder.Append(IsGrounded);
        stringBuilder.AppendLine();
        stringBuilder.Append("Is Attacking: ");
        stringBuilder.Append(IsAttacking);
        stringBuilder.AppendLine();
        stringBuilder.Append("Is Locked: ");
        stringBuilder.Append(IsLocked);

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
