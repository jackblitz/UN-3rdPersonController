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

    public DirectionRotationConstraintModifier HeadRotation;

    public DirectionRotationModifier HipRotation;


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

        mLastDirection = direction;

        HipRotation.setDirection(direction);
    }

    private void UpdateHipRotation()
    {
        mMotorModel.HipRotationMotor.DirectionSide = HipRotation.DirectionOfRotation;
        mMotorModel.HipRotationMotor.AngleDesintation = HipRotation.RotationDestination;
        mMotorModel.HipRotationMotor.AngleVelocityChange = HipRotation.RotationVelocity;
        mMotorModel.HipRotationMotor.AngleChangeAmount = HipRotation.RotationAmount;
        transform.forward = HipRotation.ForwardPosition;
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

