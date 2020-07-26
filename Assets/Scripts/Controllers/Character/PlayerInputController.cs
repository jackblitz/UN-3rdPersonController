using UnityEngine;
using System.Collections;
using static UnityEngine.InputSystem.InputAction;
using System;

[RequireComponent(typeof(PlayerMotorModel))]
[RequireComponent(typeof(PlayerAnimationController))]
[RequireComponent(typeof(PlayerMotorController))]
public class PlayerInputController : MonoBehaviour
{
    private PlayerMotorModel mPlayerModel;
    private PlayerInputActions.PlayerControlsActions mPlayerInput;

    private Rigidbody mBody;

    private void Awake()
    {
        mPlayerModel = GetComponent<PlayerMotorModel>();

        mPlayerInput = InputController.getPlayerInputAction();
        mPlayerInput.LookAt.performed += ctx => mPlayerModel.LookAtInput = ctx.ReadValue<Vector2>();
        mPlayerInput.Move.performed += ctx => mPlayerModel.MovementInput = ctx.ReadValue<Vector2>();

        mPlayerInput.Crouch.performed += ctx => mPlayerModel.IsCrouched = !mPlayerModel.IsCrouched;

        mPlayerInput.Run.performed += ctx => mPlayerModel.IsRunning = true;
        mPlayerInput.Run.canceled += ctx => mPlayerModel.IsRunning = false;

        mPlayerInput.ContextAction.performed += ctx => OnContextActionPerfomed();

        mPlayerInput.Interact.performed += ctx => OnInteractionPerformed();

        mPlayerInput.ContextAttach.performed += ctx => OnAttachPerfomed();

        //TODO REMEMEBER WHAT THESE DO. THEy are not attached or aiiming
        mPlayerInput.ContextAttack.performed += ctx => mPlayerModel.IsAttached = true;
        mPlayerInput.ContextAttack.canceled += ctx => mPlayerModel.IsAttached = false;

        mPlayerInput.ContextLock.performed += ctx => mPlayerModel.IsAiming = true;
        mPlayerInput.ContextLock.canceled += ctx => mPlayerModel.IsAiming = false;
    }

    private void Start()
    {
        mBody = GetComponent<Rigidbody>();
        //mPlayerMotor = GetComponent<PlayerMotorController>();
        //mPlayerAnimator = GetComponent<PlayerAnimationController>();
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, -Vector3.up, Color.red);
        //float yawCamera = Camera.main.transform.rotation.eulerAngles.y;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
    }


    private void OnAttachPerfomed()
    {

    }

    private void OnInteractionPerformed()
    {
        
    }

    private void OnContextActionPerfomed()
    {
        //mBody.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }
}
