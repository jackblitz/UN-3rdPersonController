using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private static PlayerInputActions mInputAction;

    public static PlayerInputActions.PlayerControlsActions getPlayerInputAction()
    {
        if (mInputAction == null)
        {
            mInputAction = new PlayerInputActions();
        }

        return mInputAction.PlayerControls;
    }

    private void Awake()
    {
        if (mInputAction == null)
        {
            mInputAction = new PlayerInputActions();
        }

    }

    public void OnEnable()
    {
        mInputAction.Enable();
    }

    private void OnDisable()
    {
        mInputAction.Disable();
    }

    private void OnDestroy()
    {
        mInputAction.Dispose();
        mInputAction = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
