using System;
using UnityEngine;

public class PlayerMotorController : CharacterMotorController
{
    public override void Update()
    {
        base.Update();
        HandleLookAtMotor();
    }

    private void HandleLookAtMotor()
    {
        Vector3 cameraDirection = RotationUtils.RotatePointAroundPivot(Vector3.forward, Vector3.zero, Camera.main.transform.rotation.eulerAngles);
        HeadRotation.TranslateTo(cameraDirection);
       
        Vector3 cameraForward = Camera.main.transform.TransformPoint(Vector3.forward * 20);
        GameObject gameObject = GameObject.Find("Camera_LookAt");

        Debug.Log(cameraForward + " Camera Look At: " + gameObject.transform.position);
        //HeadRotation.transform.position = inverse;
        // HeadRotation.TranslateTo(cameraForward);
        HeadRotation.transform.localPosition = cameraForward;
    }
}

