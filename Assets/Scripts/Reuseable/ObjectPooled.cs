using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooled : MonoBehaviour, IPooledObject
{
    public void OnObjectSpawn()
    {
        //your action afted this object spawn
    }
    public void OnEndObjectSpawn()
    {
        //your action after object end spawn
    }

}
