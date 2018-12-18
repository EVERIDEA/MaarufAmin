using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region EXAMPLE-EVENT-CLASS
public class SomeClass : GameEvent {

    public string SomeString;
    public int SomeInt;

    public SomeClass(string someString, int someInt)
    {
        SomeString = someString;
        SomeInt = someInt;
    }
}
#endregion

#region CORE-EVENT-INIT

#endregion
