using UnityEngine;
using System.Collections;
using static UnityEngine.InputSystem.InputAction;
using System;

[RequireComponent(typeof(PlayerModel))]
public class PlayerController : MonoBehaviour
{
    private PlayerModel mPlayerModel;
    private PlayerInputActions.PlayerControlsActions mPlayerInput;

    private Rigidbody mBody;
    private CapsuleCollider mCollider;

    public float GroundDistance = 0.2f;
    public float JumpHeight = 2f;
    public float KeyFrameDelta = 3f;
    public float RotationSpeed = 2f;

    //Max Velocity to which the player can change direction
    public float MaxVelocityAngle = 120;

    private Animator mAnimation;
    private float mLastSpeed;

    //Transform to ditate walk direction
    public Transform DirectionTransform;
    private Vector3 mForwardVector;

    private void Awake()
    {
        mPlayerModel = GetComponent<PlayerModel>();

        mPlayerInput = InputController.getPlayerInputAction();
        mPlayerInput.LookAt.performed += ctx => mPlayerModel.LookAtDirection = ctx.ReadValue<Vector2>();
        mPlayerInput.Move.performed += ctx => mPlayerModel.MovementDirection = ctx.ReadValue<Vector2>();

        mPlayerInput.Crouch.performed += ctx => mPlayerModel.IsCrouched = !mPlayerModel.IsCrouched;

        mPlayerInput.Run.performed += ctx => mPlayerModel.IsRunning = true;
        mPlayerInput.Run.canceled += ctx => mPlayerModel.IsRunning = false;

        mPlayerInput.ContextAction.performed += ctx => OnContextActionPerfomed();

        mPlayerInput.Interact.performed += ctx => OnInteractionPerformed();

        mPlayerInput.ContextAttach.performed += ctx => OnAttachPerfomed();

        mPlayerInput.ContextAttack.performed += ctx => mPlayerModel.IsAttacking = true;
        mPlayerInput.ContextAttack.canceled += ctx => mPlayerModel.IsAttacking = false;

        mPlayerInput.ContextLock.performed += ctx => mPlayerModel.IsLocked = true;
        mPlayerInput.ContextLock.canceled += ctx => mPlayerModel.IsLocked = false;
    }

    private void Start()
    {
        mBody = GetComponent<Rigidbody>();
        mAnimation = GetComponentInChildren<Animator>();

        mCollider = GetComponent<CapsuleCollider>();
        GroundDistance = mCollider.bounds.extents.y;

    }

    private void Update()
    {
        CalculateSpeed();
        HandleInputData();


        //Debug.DrawRay(transform.position, -Vector3.up, Color.red);
        //float yawCamera = Camera.main.transform.rotation.eulerAngles.y;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        HandleWalkDirection();

        RaycastHit hit;
        Vector3 offset = new Vector3(0, -0.2f, 0f);
        if (mPlayerModel.IsGrounded = Physics.Raycast(transform.position - offset, -Vector3.up, out hit, .5f))
            print("Found an object - distance: " + hit.distance);
    }

    /**
     *  Send Input data updates to player controller.
     *  Animator controll is informed on changes to players speed
     */
    private void HandleInputData()
    { 
        mAnimation.SetFloat("Speed", mPlayerModel.Speed);
        mAnimation.SetFloat("AngleVelocity", mPlayerModel.VelocityChange);

    }

    private void HandleWalkDirection()
    {
        Vector3 newDirection = new Vector3(mPlayerModel.MovementDirection.x, 0, mPlayerModel.MovementDirection.y);

        if (newDirection.magnitude > 0)
        {
            //Find offset based on cameraPositon
            Vector3 rotationOffset = Camera.main.transform.TransformDirection(newDirection);
            rotationOffset.y = 0;

            //Work out angle from MovementDirection to player position
            float angle = AngleBetweenVector(transform.position, transform.position + rotationOffset);

           
            //Rotate Point around player and work our position
            Quaternion rotationTo = Quaternion.Euler(0, angle, 0);
            Quaternion rotation = Quaternion.RotateTowards(DirectionTransform.transform.rotation, rotationTo, Time.deltaTime * RotationSpeed);//Quaternion.RotateTowards(DirectionTransform.transform.rotation, rotationTo, Time.deltaTime * (RotationSpeed + Math.Abs(angle) * mPlayerModel.Speed));

            //Figure out is rotation is left or right direction
            float rightleft = (((rotationTo.eulerAngles.y - DirectionTransform.transform.eulerAngles.y) + 360f) % 360f) > 180.0f ? -1 : 1;

            mPlayerModel.VelocityChange = Quaternion.Angle(DirectionTransform.transform.rotation, rotationTo) * rightleft;

            mForwardVector = RotatePointAroundPivot(new Vector3(0, 0, 1), Vector3.zero, rotation.eulerAngles);

            if (mPlayerModel.VelocityChange < MaxVelocityAngle)
           {
                transform.forward = mForwardVector;
           }

            DirectionTransform.transform.rotation = rotation;
        }

        DirectionTransform.transform.position = transform.position + mForwardVector;
    }

    private float AngleBetweenVector(Vector3 to, Vector3 from)
    {
        return Quaternion.FromToRotation(Vector3.back, to - from).eulerAngles.y;
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }

    private void OnAttachPerfomed()
    {

    }

    private void CalculateSpeed()
    {
        float speed = mPlayerModel.MovementDirection.magnitude;

        speed += mPlayerModel.IsRunning ? 1 : 0;

        float newSpeed = Mathf.Lerp(mLastSpeed, speed, KeyFrameDelta * Time.deltaTime);

        mPlayerModel.Speed = newSpeed;
        mLastSpeed = newSpeed;
    }

    private void OnInteractionPerformed()
    {
        
    }

    private void OnContextActionPerfomed()
    {
        mBody.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }
}
