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
    private Vector3 lastDirection;
    private float speed;
    private Animator mAnimation;

    public float KeyFrameDelta = 3f;
    private float lastSpeed;

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
        HandleInputData();
        //mPlayerModel.IsGrounded = Physics.Raycast(transform.position, -Vector3.up, out hit);


        //Debug.DrawRay(transform.position, -Vector3.up, Color.red);
        //float yawCamera = Camera.main.transform.rotation.eulerAngles.y;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 offset = new Vector3(0, -0.2f, 0f);
        if (mPlayerModel.IsGrounded = Physics.Raycast(transform.position - offset, -Vector3.up, out hit, .5f))
            print("Found an object - distance: " + hit.distance);
    }

    private void HandleInputData()
    {
        float horLerp = mPlayerModel.MovementDirection.x;//Mathf.Lerp(lastDirection.x, mPlayerModel.MovementDirection.x, KeyFrameDelta * Time.deltaTime);
        float verLerp = mPlayerModel.MovementDirection.y;// Mathf.Lerp(lastDirection.y, mPlayerModel.MovementDirection.y, KeyFrameDelta * Time.deltaTime);

        Vector3 directionShift = lastDirection - new Vector3(horLerp, 0, verLerp);
        /* mMomentumShift = directionShift.sqrMagnitude;

         if (mMomentumShift != 0)
         {
             Debug.Log(mMomentumShift);
         }*/

        lastDirection = new Vector3(horLerp, 0, verLerp);

        speed = lastDirection.magnitude;

        speed += mPlayerModel.IsRunning ? 1 : 0;

        float newSpeed = Mathf.Lerp(lastSpeed, speed, KeyFrameDelta * Time.deltaTime);

        mAnimation.SetFloat("Speed", newSpeed);

        lastSpeed = newSpeed;
    }

    private void OnAttachPerfomed()
    {

    }

    private void OnInteractionPerformed()
    {
        
    }

    private void OnContextActionPerfomed()
    {
        mBody.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }
}
