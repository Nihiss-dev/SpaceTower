using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private TimeSpan timerDuration = new TimeSpan(0, 1, 0);
    public delegate void TimerListener();
    private List<TimerListener> listenersCallbacks = new List<TimerListener>();
    private DateTime startTime;

    public void setDuration(int numSec)
    {
        timerDuration = new TimeSpan(0,0,numSec);
    }

    public void startTimer()
    {
        startTime = DateTime.Now;
        LevelManager.getInstance().StartChildCoroutine(timerCoroutine());
    }

    public IEnumerator timerCoroutine()
    {
        yield return new WaitForSeconds((float)timerDuration.TotalSeconds);
        notifyListeners();
    }

    public void registerCallback(TimerListener callback)
    {
        listenersCallbacks.Add(callback);
    }

    public void removeListenerOnTimer(TimerListener callback)
    {
        listenersCallbacks.Remove(callback);
    }

    private void notifyListeners()
    {
        foreach (TimerListener callback in listenersCallbacks) {
            callback();
        }
    }

    public TimeSpan getTimerDuration()
    {
        return timerDuration;
    }

    public TimeSpan getTimeLeft()
    {
        TimeSpan timeDone = DateTime.Now.Subtract(startTime);
        return timerDuration.Subtract(timeDone);
    }
}
