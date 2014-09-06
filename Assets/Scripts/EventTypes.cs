using UnityEngine;
using System.Collections;

public static partial class StealthEventTypes {

    public delegate void Vector3Event(Vector3 v);
    public delegate void FloatEvent(float f);
    public delegate void BoolEvent(bool b);
    public delegate void StringEvent(string s);
    public delegate void VoidEvent();
    public delegate void PlayerEvent(Player p);

}
