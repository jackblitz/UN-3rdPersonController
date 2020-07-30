using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotationModifier : MonoBehaviour
{
    public enum Direction {
        ANTI_CLOCKWISE = -1,
        CLOCKWISE = 1
    }

    public Direction SideRotation
    {
        get;
        set;
    }

    /**
     * Gets the ammount the transform needs to rotation in order to get to the Rotate toward vector.
     */
    public float RotationAmount
    {
        get;
        set;
    }

    /**
     * The angle to which we will be rotating too
     */
    public float RotationTo
    {
        get;
        set;
    }

    /**
     *  The amount of RotationLeft
     */
    public float RotationRemaining
    {
        get;
        set;
    }

    /**
     * The foward direction the tranform should face based on rotation
     */
    public Vector3 Forward
    {
        get;
        set;
    }

    /**
     * Position we want the transform to rotation toward
     */
    public Vector3 RotationTowards
    {
        get;
        set;
    }

    /**
     * Transform to rotate around. Known at the pivot transform
     */
    public Transform Pivot;

    /**
     *  Offset Pivot position. If the pivot postion requires an offset
     */
    public Transform OffsetPivot;

    /**
     *  How fast should the transform rotation towards the direction
     */
    public float RotationSpeed;
    private Quaternion StartPosition;
    private float mLastWay;


    /**
     * Set where you would like the transform to rotate to towards
     */
    public void TranslateTo(Vector3 translation)
    {
        RotationTowards = translation;
        RotationTo = CalculateAngle();
        CalulateRotationDirection(true);
    }

    /**
     * Calculates the Angle to roatate to based on Direction we wish to rotate towards
     */
    private float CalculateAngle()
    {
        Vector3 rotationOffset = OffsetPivot != null ? OffsetPivot.TransformDirection(RotationTowards) : RotationTowards;
        rotationOffset.y = 0;

        return RotationUtils.AngleBetweenVector(Pivot.position, Pivot.position + rotationOffset);
    }

    private void CalulateRotationDirection(bool updateDirection)
    {
        //TODO Camera transform keeps moving back of the animation
        float whichWay = Vector3.SignedAngle(OffsetPivot.TransformDirection(RotationTowards), transform.forward, Vector3.down);
        float currentDirection = Vector3.SignedAngle(transform.forward, Vector3.forward, Vector3.down);

        //Are we going a new way
        if (whichWay != mLastWay)
        { 
            Direction side;

            RotationAmount = Math.Abs(whichWay);

            //Always make sure we go in the forward direction. 
            if (Math.Abs(whichWay) > 140)
            {
                if (currentDirection > 0.1f)
                {
                    whichWay = 180;
                }
                else
                {
                    whichWay = -180;
                }

                RotationAmount = 360 - Math.Abs(whichWay);
            }

            if (updateDirection)
            {
                if (whichWay > 0.1f)
                {
                    side = Direction.CLOCKWISE;
                }
                else
                {
                    side = Direction.ANTI_CLOCKWISE;
                }

                SideRotation = side;
            }

            StartPosition = transform.rotation;
        }

        mLastWay = whichWay;
    }

   /* private float CalculateRotationAmount()
    {
        //Rotate Point around player and work our position
        Quaternion rotationTo = Quaternion.Euler(0, RotationTo, 0);

        float rotationAmount = Quaternion.Angle(transform.rotation, rotationTo);

        float amountFromForwardPoint = Quaternion.Angle(transform.rotation, Quaternion.identity);

        float currentSide = Vector3.Cross(transform.position, Vector3.forward).y;
        float gotoSide = Vector3.Cross(transform.position, RotationTowards).y;

        // SideRotation = currentSide;

        /*if (currentSide != gotoSide)
        {
            //We must pass zero
            float toZero = Quaternion.Angle(transform.rotation, Quaternion.identity);
            float zeroToPoint = Quaternion.Angle(Quaternion.identity, rotationTo);
            rotationAmount = toZero + zeroToPoint;
        }

        return rotationAmount;
    }*/

    private float CalculateRotation()
    {
        //Rotate Point around player and work our position
        Quaternion rotationTo = Quaternion.Euler(0, RotationTo, 0);
        float rotationSpeedByDistance = ((RotationAmount / 360) * 100) / 100;

        Quaternion rotationAmount = Quaternion.Euler(0, RotationAmount, 0);
        Quaternion finalRotation = SideRotation == Direction.ANTI_CLOCKWISE ? StartPosition * Quaternion.Inverse(rotationAmount) : StartPosition * rotationAmount;
        //SideRotation == Side.LEFT ? StartRotation * Quaternion.Inverse(rotationAmount) :
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, finalRotation, (RotationSpeed * rotationSpeedByDistance));
        transform.rotation = rotation;

        // Amount left to rotation toward angle
        return Quaternion.Angle(transform.rotation, rotationTo);
    }

    // Update is called once per frame
    void Update()
    {
        //Do we need to rotate
        if (RotationTowards != null)
        {
            if (RotationTowards.magnitude > 0)
            {
                RotationTo = CalculateAngle();
                CalulateRotationDirection(true);
                RotationRemaining = CalculateRotation();
                Forward = RotationUtils.RotatePointAroundPivot(Vector3.forward, Vector3.zero, transform.rotation.eulerAngles);
            }

            transform.position = Pivot.transform.position + Forward;
        }
    }
}
