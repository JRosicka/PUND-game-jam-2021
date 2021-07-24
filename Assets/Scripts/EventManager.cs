using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public class DialogEvent : UnityEvent<DialogLine> { }

    public static DialogEvent dialogEvent = new DialogEvent();
}
