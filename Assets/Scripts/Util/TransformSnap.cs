using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the Transform snap to the specified snap values.
/// </summary>
public class TransformSnap : MonoBehaviour
{

    // snap to this value
    public float snap = 0.01f;
    public Transform _transform;
    private Vector3 _offset;

    // Use this for initialization
    void Start()
    {
        _offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GetSharedSnapPosition(_transform.position + _offset, 0.1f);
        transform.rotation = Quaternion.Euler(GetSharedSnapPosition( _transform.rotation.eulerAngles, snap));
    }

    /// <summary>
    /// Accepts a position, and sets each axis-value of the position to be snapped according to the value of snap
    /// </summary>
    public Vector3 GetSharedSnapPosition(Vector3 originalPosition, float snap = 0.01f)
    {
        return new Vector3(
                Mathf.Round(originalPosition.x / snap) * snap,
                Mathf.Round(originalPosition.y / snap) * snap,
                Mathf.Round(originalPosition.z / snap) * snap
            );
    }

    /// <summary>
    /// Accepts a value, and snaps it according to the value of snap
    /// </summary>
    public static float GetSnapValue(float value, float snap = 0.01f)
    {
        return (!Mathf.Approximately(snap, 0f)) ? Mathf.RoundToInt(value / snap) * snap : value;
    }
}
