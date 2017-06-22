using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlayerInputManager : MonoBehaviour
{

    VRTK_ControllerEvents myEventsController;
    public ArcController myArc;
    public PlayerMovement myMovement;

    // Use this for initialization
    void Start()
    {
        myEventsController = GetComponent<VRTK_ControllerEvents>();
        myEventsController.TouchpadTouchStart += new ControllerInteractionEventHandler(TouchpadTouchPresent);
        myEventsController.TouchpadTouchEnd += new ControllerInteractionEventHandler(TouchpadRelease);
        myEventsController.TouchpadPressed += new ControllerInteractionEventHandler(TouchpadPressed);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void TouchpadPressed(object sender, ControllerInteractionEventArgs e)
    {
        myMovement.SetCarpetDestination(myArc.GetHitPos());
    }

    void TouchpadTouchPresent(object sender, ControllerInteractionEventArgs e)
    {
        myArc.DrawLineFunc();
    }

    void TouchpadRelease(object sender, ControllerInteractionEventArgs e)
    {
        myArc.EndDraw();
    }
}
