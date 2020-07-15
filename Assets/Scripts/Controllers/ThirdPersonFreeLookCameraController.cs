using UnityEngine;
using System.Collections;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class ThirdPersonFreeLookCameraController : MonoBehaviour
{
    public Transform playerController;

    private PlayerModel mPlayerModel;
    private CinemachineFreeLook mFreeLook;

    [Range(0f, 10f)] public float LookSpeed = 1f;
    public bool InvertY = false;

    // Use this for initialization
    void Awake()
    {
       mPlayerModel = playerController.GetComponent<PlayerModel>();
       mFreeLook = GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {
        Vector2 lookMovement = mPlayerModel.LookAtDirection.normalized;
        lookMovement.y = InvertY ? -lookMovement.y : lookMovement.y;

        // This is because X axis is only contains between -180 and 180 instead of 0 and 1 like the Y axis
        lookMovement.x = lookMovement.x * 180f;

        //Ajust axis values using look speed and Time.deltaTime so the look doesn't go faster if there is more FPS
        mFreeLook.m_XAxis.Value += lookMovement.x * LookSpeed * Time.deltaTime;
        mFreeLook.m_YAxis.Value += lookMovement.y * LookSpeed * Time.deltaTime;
    }
}
