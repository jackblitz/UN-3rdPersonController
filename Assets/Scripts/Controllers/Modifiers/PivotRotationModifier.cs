using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotationModifier : MonoBehaviour
{
    public enum Side {
        LEFT = -1,
        RIGHT = 1
    }

    public Side SideRotation
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


    /**
     * Set where you would like the transform to rotate to towards
     */
    public void TranslateTo(Vector3 translation)
    {
        RotationTowards = translation;
        RotationTo = CalculateAngle();

        CalulateRotationDirection();

        RotationAmount = CalculateRotation();
    }

    /**
     * Calculates the Angle to roatate to based on Direction we wish to rotate towards
     */
    private float CalculateAngle()
    {
        Vector3 rotationOffset = OffsetPivot != null ? OffsetPivot.TransformDirection(RotationTowards) : RotationTowards;
        rotationOffset.y = 0;

        //Work out angle from MovementDirection to player position
        return RotationUtils.AngleBetweenVector(Pivot.position, Pivot.position + rotationOffset);
    }

    private void CalulateRotationDirection()
    {
        float angle = CalculateAngle();
        //Rotate Point around player and work our position
        Quaternion rotationTo = Quaternion.Euler(0, angle, 0);
        SideRotation = (((rotationTo.eulerAngles.y - transform.eulerAngles.y) + 360f) % 360f) > 180.0f ? Side.LEFT : Side.RIGHT;
    }

    private float CalculateRotation()
    {
        float angle = CalculateAngle();
        //Rotate Point around player and work our position
        Quaternion rotationTo = Quaternion.Euler(0, angle, 0);

        float rotationSpeed = RotationSpeed;

        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, rotationTo, Time.deltaTime * rotationSpeed);

        transform.rotation = rotation;

        // Amount left to rotation toward angle
        return Quaternion.Angle(transform.rotation, rotationTo) * (int)SideRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Do we need to rotate
        if (RotationTowards != null)
        {
            if (RotationTowards.magnitude > 0)
            {
                RotationRemaining = CalculateRotation();

                Forward = RotationUtils.RotatePointAroundPivot(Vector3.forward, Vector3.zero, transform.rotation.eulerAngles);
            }

            transform.position = Pivot.transform.position + Forward;
        }

        Debug.Log("RotationModifier");
    }
}
