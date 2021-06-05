using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTimer
{
    private float startTime = 0;
    private float actionTime = 1;
    private float currentState = 0;
    private bool isAction = false;
    
    public void StartAction()
    {
        startTime = Time.time;
        currentState = 0;
        isAction = true;
    }

    public bool UpdateState()
    {
     float t;

        if (!isAction) return false;
        
        t = Time.time - startTime;
        if (t >= actionTime)
        {
            currentState = 1;
            isAction = false;
        }
        else currentState = t / actionTime;

     return isAction;
    }

    public void SetActionTime(float t)
    {
        actionTime = t;
    }

    public float GetState()
    {
        return currentState;
    }

    public void ResetState()
    {
        currentState = 0;
    }

    public bool IsActive()
    {
        return isAction;
    }
}
