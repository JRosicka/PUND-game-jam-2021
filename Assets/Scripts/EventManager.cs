using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public class DialogEvent : UnityEvent<DialogLine> { }
    public class IntEvent : UnityEvent<int> { }

    public static DialogEvent dialogEvent = new DialogEvent();
    public static IntEvent damageEvent = new IntEvent();
    public static IntEvent healEvent = new IntEvent();
    public static IntEvent mapFragmentCollectionEvent = new IntEvent();
}
