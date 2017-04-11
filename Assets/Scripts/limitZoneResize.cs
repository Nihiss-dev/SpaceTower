using System;
using System.Collections;
using System.Collections.Generic;
using SpaceTowers;
using UnityEngine;

public class limitZoneResize : MonoBehaviour
{
    private bool stopScaling;
    private bool isStarted;

    private Timer timer;
    private float timerDuration;
    private float timeLeft;

    private Vector3 presentScale;

    [SerializeField]
    private Vector3 initialScale;
    [SerializeField]
    private Vector3 finalScale;

    [SerializeField]
    private GameObject constructor;

    // Use this for initialization

    void Start()
    {
        constructor.GetComponent<ConstructHandler>()._myLimitZone = this.gameObject.GetComponent<limitZoneResize>();
        stopScaling = true;
        isStarted = false;
        initialScale = transform.localScale;
        finalScale = new Vector3(2, 2, 1);

        presentScale = initialScale;

        timer = LevelManager.getInstance().getTimer();
        timerDuration = (float)timer.getTimerDuration().TotalMilliseconds;

      
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStarted)
        {
            return;
        }

        timeLeft = (float)timer.getTimeLeft().TotalMilliseconds;
        if (timeLeft <= 0) {
            Destroy(gameObject);
        }
        else
        {
            if (stopScaling)
            {
                return;
            }

            float scaleDelta = timeLeft / timerDuration;
            Vector3 newScale = Vector3.Lerp(finalScale, initialScale, scaleDelta);
            //transform.localScale = newScale;

            setGridLimit(newScale);

        }

    }

    public void setStart()
    {
        isStarted = true;
    }

    public void setStopScaling(bool StopScale)
    {
        stopScaling = StopScale;
    }


    private void setGridLimit(Vector3 newScale)
    {

        if (newScale.x < presentScale.x) {
            if (presentScale.x % 2 == 0 && presentScale.x != initialScale.x) {
                constructor.GetComponent<ConstructHandler>().moveBackLeftLimit();
                if (stopScaling)
                    return;
                constructor.GetComponent<ConstructHandler>().moveBackRightLimit();
                if (stopScaling)
                    return;
                transform.localScale = new Vector3(presentScale.x, transform.localScale.y, 1);
            }
            presentScale.x = --presentScale.x;
        }

        if (newScale.y < presentScale.y) {

            if (presentScale.y % 2 == 0 && presentScale.y != initialScale.y) {
                constructor.GetComponent<ConstructHandler>().moveBackTopLimit();
                if (stopScaling)
                    return;
                transform.localScale = new Vector3(transform.localScale.x, presentScale.y, 1);
            }
            presentScale.y = --presentScale.y;
        }


    }
}
