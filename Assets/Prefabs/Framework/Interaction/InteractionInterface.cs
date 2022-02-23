using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragable
{
    public void Grabbed(GameObject grabber, Vector3 grabPoint);
    public void Released(Vector3 ThrowVelocity);
}


