﻿using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.EventSystems;

public class DirectionRotationModifier : MonoBehaviour
{
    private Vector3 mDirection;
    private Vector3 mLastDirection = Vector3.zero;
    public Transform Parent;

    public Transform RotationOffset;

    public float Speed = 0.05f;

    //Once threshold is met the rotation is forced to rotation based on direction not on clostest
    public float RotationThreshold = 119;

    public enum RotationDirection
    {
        COUNTER_CLOCKWISE = -1,
        CLOCKWISE = 1
    }

    public RotationDirection DirectionOfRotation
    {
        get;
        set;
    }

    // Speed in change of direction. 
    public float RotationVelocity
    {
        get;
        set;
    }

    // Final Rotation Position
    public float RotationDestination
    {
        get;
        set;
    }

    //Remaining rotation left to get to RotationDesination
    public float RotationAmount
    {
        get;
        set;
    }

    public Vector3 ForwardPosition
    {
        get;
        set;
    }

    public void setDirection(Vector3 direction)
    {
        mDirection = direction;
    }

    private void Update()
    {
       // if (Vector3.Distance(mDirection, mLastDirection) != 0)
       // {
            RotationVelocity = CalculateRemainingRotation();
       // }
        
        DirectionOfRotation = CalcuateDirection();
        RotationDestination = CalculateRotationDestination();

        if (mDirection.magnitude > 0)
        {
            transform.rotation = OnUpdateRotation();
        }

        ForwardPosition = RotationUtils.RotatePointAroundPivot(Vector3.forward, Vector3.zero, transform.rotation.eulerAngles);
        transform.position = Parent.position + ForwardPosition;

        mLastDirection = mDirection;
    }

    private RotationDirection CalcuateDirection()
    {
        float directionY = Mathf.RoundToInt(Vector3.SignedAngle(mDirection, Vector3.forward, Vector3.down));

        float currentY = transform.rotation.eulerAngles.y;

        float destinationY = CalculateRotationDestination();

        float shortestAngle = Mathf.RoundToInt(Mathf.DeltaAngle(currentY, destinationY));

        RotationAmount = shortestAngle;

        if (shortestAngle == 0)
        {
            return DirectionOfRotation;
        }

        if(Math.Abs(shortestAngle) < RotationThreshold)
        {
            if(shortestAngle < 0)
            {
                return RotationDirection.COUNTER_CLOCKWISE;
            }
            else
            {
                return RotationDirection.CLOCKWISE;
            }
        }

        if (directionY < 0)
        {
            return RotationDirection.COUNTER_CLOCKWISE;
        }
        else
        {
            return RotationDirection.CLOCKWISE;
        }

    }

    private Quaternion OnUpdateRotation()
    {
        float currentY = transform.rotation.eulerAngles.y;
      
        float gotoy = Quaternion.Euler(transform.rotation.eulerAngles.x, RotationDestination, transform.rotation.eulerAngles.z).eulerAngles.y;

        if (DirectionOfRotation == RotationDirection.COUNTER_CLOCKWISE)
        {
          
            float counterY = (360 - currentY) * -1;
            gotoy =  ((360 - gotoy) * -1);

            if(Math.Abs(counterY) < Math.Abs(gotoy)){
                currentY = counterY;
            }
        }
        else
        {
            gotoy = Quaternion.Euler(transform.rotation.eulerAngles.x, gotoy, transform.rotation.eulerAngles.z).eulerAngles.y;

            if (currentY > gotoy)
            {
                currentY = (360 - currentY) * -1;
            }
        }

        float y = Mathf.Lerp(currentY, gotoy, Speed * Time.deltaTime);

        return Quaternion.Euler(transform.rotation.eulerAngles.x, y, transform.rotation.eulerAngles.z);
    }

    private float CalculateRotationDestination()
    {
        return Mathf.RoundToInt(Vector3.SignedAngle(RotationOffset.TransformDirection(mDirection), Vector3.forward, Vector3.down));
    }

    //TODO WORK OUT REMAINING BASED ON DIRECTION
    private float CalculateRemainingRotation()
    {
       return Mathf.RoundToInt(Vector3.SignedAngle(RotationOffset.TransformDirection(mDirection), transform.forward, Vector3.down)); 
    }
}

