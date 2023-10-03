using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ActionManager : MonoBehaviour
{

    //UnityEvents

    public UnityEvent jump;
    public UnityEvent<int> moveCheck;
    public UnityEvent jumpHold;


    public void OnJumpHoldAction(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("JumpHold was started");
        else if (context.performed)
        {
            Debug.Log("JumpHold was performed");
            Debug.Log(context.duration);
            jumpHold.Invoke();
        }
        else if (context.canceled)
            Debug.Log("JumpHold was cancelled");
    }

    // called twice, when pressed and unpressed
    public void OnJumpAction(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("Jump was started");
        else if (context.performed)
        {
            jump.Invoke();
            Debug.Log("Jump was performed");
        }
        else if (context.canceled)
            Debug.Log("Jump was cancelled");

    }

    // called twice, when pressed and unpressed
    public void OnMoveAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("move started");
            int faceRight = context.ReadValue<float>() > 0 ? 1 : -1;
            moveCheck.Invoke(faceRight);

            //Debug.Log($"move value: {move}"); // will return null when not pressed
        }
        if (context.canceled)
        {
            Debug.Log("move stopped");
            moveCheck.Invoke(0);
        }
    }

    public void OnClickAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("mouse click started");
        }
        else if (context.performed)
        {
            Debug.Log("mouse click performed");
        }
        else if (context.canceled)
        {
            Debug.Log("mouse click cancelled");
        }
    }

    public void OnPointAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 point = context.ReadValue<Vector2>();
            Debug.Log($"point detected: {point}");
        }
    }





    //// Input Action Asset

    //public InputActionAsset actions;
    //private InputAction moveAction;

    // // public MarioActions marioActions;


    //private void Start()
    //{
    //    moveAction = actions.FindActionMap("Gameplay").FindAction("Move");
    //    actions.FindActionMap("Gameplay").FindAction("Jump").performed += OnJump;
    //    actions.FindActionMap("Gameplay").FindAction("Jumphold").performed += OnJumphold;
    //    //marioActions = new MarioActions();
    //    //marioActions.gameplay.Enable();
    //    //marioActions.gameplay.jump.performed += OnJump;
    //    //marioActions.gameplay.jumphold.performed += OnJumphold;
    //    //marioActions.gameplay.move.started += OnMove;
    //    //marioActions.gameplay.move.canceled += OnMove;


    //}

    //void Update()
    //{
    //    // our update loop polls the "move" action value each frame
    //    Vector2 moveVector = moveAction.ReadValue<Vector2>();
    //}

    //private void OnJump(InputAction.CallbackContext context)
    //{
    //    // this is the "jump" action callback method
    //    Debug.Log("Jump!");
    //}

    //private void OnJumphold(InputAction.CallbackContext context)
    //{
    //    // this is the "jump" action callback method
    //    Debug.Log("JumpHold!");
    //}

    //void OnMove(InputAction.CallbackContext context)
    //{
    //    if (context.started)
    //    {
    //        Debug.Log("move started");
    //    }
    //    if (context.canceled)
    //    {
    //        Debug.Log("move stopped");
    //    }

    //    float move = context.ReadValue<float>();
    //    Debug.Log($"move value: {move}"); // will return null when not pressed

    //    // TODO
    //}

    //void OnEnable()
    //{
    //    actions.FindActionMap("gameplay").Enable();
    //}
    //void OnDisable()
    //{
    //    actions.FindActionMap("gameplay").Disable();
    //}



    // SendMessages Behaviour


    //// triggered upon performed interaction (default successful press)
    //public void OnJump()
    //{
    //    Debug.Log("OnJump called");
    //    // TODO
    //}

    //// triggered upon 1D value change (default successful press and cancelled)
    //public void OnMove(InputValue input)
    //{
    //    if (input.Get() == null)
    //    {
    //        Debug.Log("Move released");
    //    }
    //    else
    //    {
    //        Debug.Log($"Move triggered, with value {input.Get()}"); // will return null when released
    //    }
    //    // TODO
    //}

    //// triggered upon performed interaction (custom successful hold)
    //public void OnJumphold(InputValue value)
    //{
    //    Debug.Log($"OnJumpHold performed with value {value.Get()}");
    //    // TODO

    //}





}
