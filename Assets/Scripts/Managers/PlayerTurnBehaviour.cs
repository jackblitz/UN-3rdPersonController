using UnityEngine;
using System.Collections;
using System;

public class PlayerTurnBehaviour : StateMachineBehaviour
{
    public interface ITurnBehaviour
    {
        void OnTurnAnimiationStarted();
        void OnTurnAnimationFinished();
    }

    private ITurnBehaviour mTurnBehaviour;

    public void SetTurnBehaviourListener(ITurnBehaviour turnBehaviour)
    {
        mTurnBehaviour = turnBehaviour;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (mTurnBehaviour != null)
            mTurnBehaviour.OnTurnAnimiationStarted();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(mTurnBehaviour != null)
            mTurnBehaviour.OnTurnAnimationFinished();
    }
}
