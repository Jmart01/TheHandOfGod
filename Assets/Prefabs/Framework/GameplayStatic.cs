using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameplayStatic
{
    static WalkMan walkMan;
    static Player player;
    static Earth earth;

    public static Transform GetWalkmanTransform()
    {
        if(GetWalkman())
        {
            return GetWalkman().transform;
        }
        return null;
    }
    public static WalkMan GetWalkman()
    {
        if(walkMan == null)
        {
            walkMan = GameObject.FindObjectOfType<WalkMan>();
        }
        return walkMan;
    }

    public static Player GetPlayer()
    {
        if(player == null)
        {
            player = GameObject.FindObjectOfType<Player>();
        }
        return player;
    }
    
    public static Earth GetEarth()
    {
        if(earth == null)
        {
            earth = GameObject.FindObjectOfType<Earth>();
        }
        return earth;
    }
}
