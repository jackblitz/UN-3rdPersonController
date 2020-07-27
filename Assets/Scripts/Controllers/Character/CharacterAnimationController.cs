using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimationController : MonoBehaviour, PlayerTurnBehaviour.ITurnBehaviour
{
    private Animator mAnimation;
    private BaseMotorModel mMotorModel;

    public float MaxVelocityAngle;

    public int AnimationState
    {
        get;
        set;
    }

    public enum CharacterAnimationState
    {
        IDLE = 0,
        EXPLORE = 1,
        QUICK_TURN = 2,
    }

    private void Start()
    {
        mAnimation = GetComponentInChildren<Animator>();
        PlayerTurnBehaviour[] turnBehaviuours = mAnimation.GetBehaviours<PlayerTurnBehaviour>();

        foreach (PlayerTurnBehaviour turnBehaviour in turnBehaviuours)
        {
            turnBehaviour.SetTurnBehaviourListener(this);
        }

        mMotorModel = GetComponent<BaseMotorModel>();
    }

    public void Update()
    {
        HandleInputData();
        HandleRotationData();
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        foreach(CharacterAnimationState state in (CharacterAnimationState[])Enum.GetValues(typeof(CharacterAnimationState)))
        {
            if (mAnimation.GetCurrentAnimatorStateInfo(0).IsTag(state.ToString()))
            {
                AnimationState = (int)state;
            }
        }
    }

    /**
 *  Send Input data updates to player controller.
 *  Animator controll is informed on changes to players speed
 */
    private void HandleInputData()
    {
        mAnimation.SetFloat("Speed", mMotorModel.Velocity);
    }

    public void HandleRotationData()
    {
        //If we are currently turned and we need to starting turnning animation again cancel out. This will stop large rotations being performed. 
        if (mMotorModel.HipRotationMotor.AngleVelocityChange > MaxVelocityAngle)
        {
            OnStopTurn();
        }
        mAnimation.SetFloat("AngleVelocity", mMotorModel.HipRotationMotor.AngleChangeRemaining);
    }

    public void OnTurnAnimiationStarted()
    {
    }

    public void OnTurnAnimationFinished()
    {
    }

    public void OnStopTurn()
    {
        mAnimation.SetTrigger("StopTurn");
    }
}

