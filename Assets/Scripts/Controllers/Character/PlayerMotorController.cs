using System;
using UnityEngine;

public class PlayerMotorController : CharacterMotorController
{
    public GameObject mCameraReference;

    public override void Update()
    {
        base.Update();
        HandleLookAtMotor();
    }

    private void HandleLookAtMotor()
    {
        Vector3 cameraDirection = RotationUtils.RotatePointAroundPivot(Vector3.forward, Vector3.zero, mCameraReference.transform.rotation.eulerAngles);

        HeadRotation.setDirection(cameraDirection);
       
        Vector3 cameraForward = Camera.main.transform.TransformPoint(Vector3.forward * 8);

       // HeadRotation.transform.localPosition = new Vector3(HeadRotation.transform.position.x, cameraForward.y, HeadRotation.transform.position.z);
    }
}

