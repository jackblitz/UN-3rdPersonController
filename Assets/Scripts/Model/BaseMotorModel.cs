
using System.Text;
using UnityEngine;

public class BaseMotorModel : MonoBehaviour, ConsoleLogs
{
    public class RotationMotor
    {
        /**
     * Shows how much motor needs to rotate by
     */
        public float AngleVelocityChange
        {
            get;
            set;
        }

        /**
         * Show much much the motor still need to rotate by
         */
        public float AngleChangeRemaining
        {
            get;
            set;
        }

        /**
         * Shows the desintation of the rotation
         */
        public float AngleDesintation
        {
            get;
            set;
        }

        public PivotRotationModifier.Direction DirectionSide
        {
            get;
            set;
        }
    }

    /**
     * Players Look at Direction
     **/
    public Vector2 LookAtInput
    {
        get;
        set;
    }

    /**
     * Player Look at Direction
     **/
    public Vector2 MovementInput
    {
        get;
        set;
    }

    /**
    * Vector points for the forward direction. Used for rotation to make sure transform is facing the correct direction
    */
    public Vector3 ForwardDirection
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

    public float Velocity
    {
        get;
        set;
    }

    public RotationMotor HipRotationMotor
    {
        get;
        set;
    }

    private void Start()
    {
        HipRotationMotor = new RotationMotor();
    }


    public string GetConsoleLog()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(this.name);
        stringBuilder.Append("\n");
        stringBuilder.Append("Look At Direction: ");
        stringBuilder.Append(LookAtInput.ToString());
        stringBuilder.Append("\n");
        stringBuilder.Append("Movement Direction: ");
        stringBuilder.Append(MovementInput.ToString());
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
        stringBuilder.Append("Velocity: ");
        stringBuilder.Append(Velocity);
        stringBuilder.AppendLine();
        stringBuilder.Append("Hip Desintation Angle: ");
        stringBuilder.Append(HipRotationMotor.AngleDesintation);
        stringBuilder.AppendLine();
        stringBuilder.Append("Hip Roation Amount: ");
        stringBuilder.Append(HipRotationMotor.AngleVelocityChange);
        stringBuilder.AppendLine();
        stringBuilder.Append("Hip Rotation Amount Left: ");
        stringBuilder.Append(HipRotationMotor.AngleChangeRemaining);
        stringBuilder.AppendLine();
        stringBuilder.Append("Hip Rotation Side: ");
        stringBuilder.Append(HipRotationMotor.DirectionSide.ToString());
        return stringBuilder.ToString();
    }
}
