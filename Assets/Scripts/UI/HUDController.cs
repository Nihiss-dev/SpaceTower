using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private Timer timer;
    private Timer timerIntro;
    public GameObject timerUI;
    public GameObject timerIntroUI;
    private Text timerText;
    private Text timerIntroText;
    private TimeSpan lastShownTimerValue = new TimeSpan();
    public MunitionController munitionController;
    public PhaseUiController phaseUiController;
    private bool flash;
    public CanvasGroup myCanvasGroup;

    void Start()
    {
        timer = LevelManager.getInstance().getTimer();
        timerIntro = LevelManager.getInstance().getTimerIntro();
        timerText = timerUI.GetComponent<Text>();
        timerIntroText = timerIntroUI.GetComponent<Text>();
    }

    void Update()
    {
        updateTimerUI();
        handleFlash();
    }

    private void updateTimerUI()
    {
        TimeSpan time = timer.getTimeLeft();

        lastShownTimerValue = time;
        timerText.text = string.Format("{0:0}:{1:00}", time.Minutes, time.Seconds);

        TimeSpan timeIntro = timerIntro.getTimeLeft();

        timerIntroText.text = string.Format("{1:0}", timeIntro.Minutes, timeIntro.Seconds);
    }

    public void hideTimer()
    {
        timerUI.SetActive(false);
    }

    internal void showTimer()
    {
        timerUI.SetActive(true);
    }

    public void hideIntroTimer()
    {
        timerIntroUI.SetActive(false);
    }

    internal void showIntroTimer()
    {
        timerIntroUI.SetActive(true);
    }

    public void changeMunitionSelection(int direction, int playerID)
    {
        munitionController.changeSelection(direction, playerID);
        LevelManager.getInstance().changeBlockSelection(direction, playerID);
    }

    internal void updateMunitions()
    {
        munitionController.updateCurrentlySelectedInfo();
    }

    internal void setGameState(GameState gameState)
    {
        if(gameState == GameState.CONTRUCTION)
        {
            phaseUiController.showConstructionPhase();
        }
        else{
            phaseUiController.showDestrutionPhase();
        }
    }

    public void transitionFlash()
    {
        myCanvasGroup.alpha = 1;
        flash = true;
        InputManager.getInstance().turnOff();
    }

    private void handleFlash()
    {
        if (flash)
        {
            myCanvasGroup.alpha = myCanvasGroup.alpha - Time.deltaTime;
            if (myCanvasGroup.alpha <= 0)
            {
                InputManager.getInstance().turnOn();
                myCanvasGroup.alpha = 0;
                flash = false;
            }
        }
    }
}
