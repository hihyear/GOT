using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;



    public void OnMove(InputValue value)
    {
        Debug.Log("OnMove");
        move = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            Debug.Log("OnLook");
            look = value.Get<Vector2>();
        }
    }

    public void OnJump(InputValue value)
    {
        //jump = value.isPressed;
    }

    public void OnSprint(InputValue value)
    {
       sprint = value.isPressed;
    }

    public void OnAttack(InputValue value)
    {
        Debug.Log("OnAttack");
    }

    //private void OnApplicationFocus(bool hasFocus)
    //{
    //    SetCursorState(cursorLocked);
    //}
    //
    //private void SetCursorState(bool newState)
    //{
    //    Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    //}
}
