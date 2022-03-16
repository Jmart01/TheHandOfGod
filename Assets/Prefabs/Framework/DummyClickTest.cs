using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyClickTest : MonoBehaviour
{
    int clickTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        clickTime += 1;
        Debug.Log($"button clicked {clickTime} times");
    }
}
