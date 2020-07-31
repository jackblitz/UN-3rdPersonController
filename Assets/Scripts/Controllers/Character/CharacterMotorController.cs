using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterMotorController : MonoBehaviour
{

    public float KeyFrameDelta = 3f;

    private BaseMotorModel mMotorModel;
    private float mLastSpeed;

    private Vector3 mLastDirection;
    private RaycastHit hit;

    public PivotRotationModifier HipRotation;
    public PivotRotationModifier HeadRotation;

    public DirectionRotationModifier HeadAimAt;


    private void Start()
    {
        mMotorModel = GetComponent<BaseMotorModel>();
    }

    public virtual void Update()
    {
        GroundCheck();
        HandleInput();
        CalculateSpeed();

    }

    private void GroundCheck()
    {
        Vector3 offset = new Vector3(0, -0.2f, 0f);
        if (mMotorModel.IsGrounded = Physics.Raycast(transform.position - offset, -Vector3.up, out hit, .5f)) { }
    }
    public virtual void FixedUpdate()
    {
        //UpdatePlayerHipRotation();
        UpdateHipRotation();
    }
    private void HandleInput()
    {
        Vector3 direction = new Vector3(mMotorModel.MovementInput.x, 0, mMotorModel.MovementInput.y);

        if (Vector3.Distance(direction, mLastDirection) != 0)
        {
            HipRotation.TranslateTo(direction);
        }

        mLastDirection = direction;

        HeadAimAt.setDirection(direction);
    }

    private void UpdateHipRotation()
    {
        mMotorModel.HipRotationMotor.DirectionSide = HeadAimAt.DirectionOfRotation;
        mMotorModel.HipRotationMotor.AngleDesintation = HeadAimAt.RotationDestination;
        mMotorModel.HipRotationMotor.AngleVelocityChange = HeadAimAt.RotationVelocity;
        mMotorModel.HipRotationMotor.AngleChangeRemaining = HeadAimAt.RotationRemaining;
        transform.forward = HeadAimAt.ForwardPosition;
    }

    private void UpdatePlayerHipRotation()
    {
      // mMotorModel.HipRotationMotor.DirectionSide = HipRotation.SideRotation;
        mMotorModel.HipRotationMotor.AngleDesintation = HipRotation.RotationTo;
        mMotorModel.HipRotationMotor.AngleVelocityChange = HipRotation.RotationAmount;
        mMotorModel.HipRotationMotor.AngleChangeRemaining = HipRotation.RotationRemaining;
        transform.forward = HipRotation.Forward;
    }


    private void CalculateSpeed()
    {
        float speed = mMotorModel.MovementInput.magnitude;

        speed += mMotorModel.IsRunning ? 1 : 0;

        float newSpeed = Mathf.Lerp(mLastSpeed, speed, KeyFrameDelta * Time.deltaTime);

        mMotorModel.Velocity = newSpeed;
        mLastSpeed = newSpeed;
    }

}

