using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public class DialogEvent : UnityEvent<DialogLine> { }
    public class IntEvent : UnityEvent<int> { }
    public class DoubleIntEvent : UnityEvent<int, int> { }

    public static DialogEvent dialogEvent = new DialogEvent();
    public static IntEvent damageEvent = new IntEvent();
    public static DoubleIntEvent healEvent = new DoubleIntEvent();
    public static DoubleIntEvent mapFragmentCollectionEvent = new DoubleIntEvent();
}
