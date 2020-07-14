using UnityEngine;
using System.Collections;
using static UnityEngine.InputSystem.InputAction;
using System;

[RequireComponent(typeof(PlayerModel))]
public class PlayerController : MonoBehaviour
{
    private PlayerModel mPlayerModel;
    private PlayerInputActions.PlayerControlsActions mPlayerInput;


    private void Awake()
    {
        mPlayerModel = GetComponent<PlayerModel>();

        mPlayerInput = InputController.getPlayerInputAction();
        mPlayerInput.LookAt.performed += ctx => mPlayerModel.LookAtDirection = ctx.ReadValue<Vector2>();
        mPlayerInput.Move.performed += ctx => mPlayerModel.MovementDirection = ctx.ReadValue<Vector2>();

        /*mPlayerInput.Crouch.performed += ctx => mPlayerModel.IsCrouched = !mPlayerModel.IsCrouched;

        mPlayerInput.Run.performed += ctx => mPlayerModel.IsRunning = false;
        mPlayerInput.Run.canceled += ctx => mPlayerModel.IsRunning = false;

        mPlayerInput.ContextAction.performed += ctx => ContextAction();*/
    }

    private void ContextAction()
    {
        var coroutine = Jump();
        StartCoroutine(coroutine);
    }

    private IEnumerator Jump()
    {
        yield return new WaitForSeconds(2f);
    }

}
