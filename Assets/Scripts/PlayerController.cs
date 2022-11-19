using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class PlayerController : MonoBehaviour
{
    [Header("Controllers")]
    public XRController rightHand;
    public XRController leftHand;

    [Header("Left Hand Buttons")]
    public InputHelpers.Button sprintButton;
    public InputHelpers.Button changeUpButton;    
    public InputHelpers.Button changeDownButton;

    [Header("Right Hand Buttons")]
    public InputHelpers.Button jumpButton;
    public InputHelpers.Button crounchButton;
    public InputHelpers.Button shootButton;

    // Left Hand
    public bool SprintPressed()
    {
        bool sprint;
        leftHand.inputDevice.IsPressed(sprintButton, out sprint);

        return sprint;
    }

    public bool CrounchPressed()
    {
        bool crounch;
        rightHand.inputDevice.IsPressed(crounchButton, out crounch);

        return crounch;
    }
    
    public bool ChangeUp()
    {
        bool changeUp;
        
        leftHand.inputDevice.IsPressed(changeDownButton, out changeUp);
        return changeUp;
    }
    
    public bool ChangeDown()
    {
        bool changeDown;
        leftHand.inputDevice.IsPressed(changeDownButton, out changeDown);

        return changeDown;
    }

    // Right Hand
    public bool JumpPressed()
    {
        bool jump;
        rightHand.inputDevice.IsPressed(jumpButton, out jump);

        return jump;
    }
    
    public bool ShootPressed()
    {
        bool shoot;
        rightHand.inputDevice.IsPressed(shootButton, out shoot);
        
        Debug.Log(shoot);
        return shoot;
    }
}
