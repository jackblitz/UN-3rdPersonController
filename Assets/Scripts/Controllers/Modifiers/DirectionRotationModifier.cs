using System;
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
    public float RotationRemaining
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
        RotationVelocity = CalculateRotationVelocity();
        RotationDestination = CalculateRotationDestination();

        transform.rotation = OnUpdateRotation();

        RotationRemaining = CalculateRemainingRotation();

        ForwardPosition = RotationUtils.RotatePointAroundPivot(Vector3.forward, Vector3.zero, transform.rotation.eulerAngles);
        transform.position = Parent.position + ForwardPosition;
    }

    private Quaternion OnUpdateRotation()
    {
        float currentY = transform.rotation.eulerAngles.y;
        Quaternion rotationTo = Quaternion.Euler(transform.rotation.eulerAngles.x, RotationDestination, transform.rotation.eulerAngles.z);
        float desination = rotationTo.eulerAngles.y;

        float y = Mathf.InverseLerp(currentY, desination, Time.deltaTime);

        return Quaternion.Euler(transform.rotation.eulerAngles.x, y, transform.rotation.eulerAngles.z);
    }

    private float CalculateRotationVelocity()
    {
        float lastAngle = Vector3.SignedAngle(mLastDirection, transform.forward, Vector3.down);

      //  float angleDifference = Quaternion.Angle(CalculateRemainingRotation(), Quaternion.Euler(0, lastAngle, 0));

        mLastDirection = RotationOffset.TransformDirection(mDirection);

        return 0;
    }

    private float CalculateRotationDestination()
    {
        return Mathf.RoundToInt(Vector3.SignedAngle(RotationOffset.TransformDirection(mDirection), Vector3.forward, Vector3.down));
    }

    private float CalculateRemainingRotation()
    {
        return Mathf.RoundToInt(Vector3.SignedAngle(RotationOffset.TransformDirection(mDirection), transform.forward, Vector3.down));
    }
}

