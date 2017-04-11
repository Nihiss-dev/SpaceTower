using SpaceTowers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public ConstructHandler constructHandlerPlayer1;
    public ConstructHandler constructHandlerPlayer2;

    public HUDController hudController;
    protected static LevelManager instance;
    private Timer timer;
    private Timer startTimer;
    public statistiqueManager statsManager;

    private MunitionRepository munitionRepository;
    private BlockManager blockManager;
    private int selectedMunitionLevelPlayer1;
    private int selectedMunitionLevelPlayer2;
    private Turn turn;
    private Shoot shooterPlayer1;
    private Shoot shooterPlayer2;
    public Dictionary<string, int> layerMask = new Dictionary<string, int>();

    private GameState currentState;

    void Awake()
    {
        selectedMunitionLevelPlayer1 = 1; //TODO also set in MunitionController in UI
        selectedMunitionLevelPlayer2 = 1;
        blockManager = new BlockManager(hudController);
        munitionRepository = new MunitionRepository();
        
        GameObject.Find("CursorP1").GetComponent<Renderer>().enabled = false;
        GameObject.Find("CursorP2").GetComponent<Renderer>().enabled = false;
        turn = Turn.getInstance();
        layerMask.Add("Wood",8);
        layerMask.Add("Rock", 9);
        layerMask.Add("Metal", 3 << 10);

        hudController.showIntroTimer();
        InputManager.getInstance().turnOff();
        startTimer = new Timer();
        startTimer.setDuration(4);
        startTimer.registerCallback(startingGame);
        startTimer.startTimer();

        hudController.hideTimer();
        timer = new Timer();
        timer.registerCallback(timerOver);
    }

    private void startingGame()
    {
        Debug.Log("Start Game");
        hudController.hideIntroTimer();
        setGameState(GameState.CONTRUCTION);

        limitZoneResize[] limitZoneScript = GameObject.FindObjectsOfType<limitZoneResize>();
        foreach (var limiteZone in limitZoneScript)
        {
            limiteZone.setStart();
            limiteZone.setStopScaling(false);
        }
    }

    internal void setShooter(Shoot shooter, int playerID)
    {
        if(playerID == 1)
        {
            shooterPlayer1 = shooter;
        }else
        {
            shooterPlayer2 = shooter;
        }
    }

    public static LevelManager getInstance()
    {
        if (instance == null) {
            instance = (LevelManager)FindObjectOfType(typeof(LevelManager));

            if (instance == null) {
                Debug.LogError("An instance of " + typeof(LevelManager) +
                   " is needed in the scene, but there is none.");
            }
        }

        return instance;
    }

    internal void changeBlockSelection(int direction, int playerID)
    {
        if (playerID == 1)
        {
            selectedMunitionLevelPlayer1 = changeLevel(direction, selectedMunitionLevelPlayer1);
        }
        else
        {
            selectedMunitionLevelPlayer2 = changeLevel(direction, selectedMunitionLevelPlayer2);
        }

        updateSelectedBlock();
    }

    private int changeLevel(int direction, int level)
    {
        int numberOfLevels = 4;
        if (direction == -1)
        {
            if (level == 1)
            {
                level = numberOfLevels;
            }
            else
            {
                level--;
            }
        }
        else
        {
            if (level == numberOfLevels)
            {
                level = 1;
            }
            else
            {
                level++;
            }
        }
        return level;
    }

    private void updateSelectedBlock()
    {
        if(currentState == GameState.CONTRUCTION)
        {
            constructHandlerPlayer1.changeSelectedBlock();
            constructHandlerPlayer2.changeSelectedBlock();
        }
        else
        {
            int selectedBlockLevelPlayer1 = getSelectedBlockLevel(1);
            int selectedBlockLevelPlayer2 = getSelectedBlockLevel(2);
            shooterPlayer1.changeSelectedBlock(selectedBlockLevelPlayer1);
            shooterPlayer2.changeSelectedBlock(selectedBlockLevelPlayer2);
        }
    }

    void Update()
    {
        blockManager.update();
    }

    internal statistiqueManager getStatsManager()
    {
        return statsManager;
    }

    public void setGameState(GameState gameState)
    {
        currentState = gameState;
        hudController.setGameState(gameState);
        if (gameState == GameState.CONTRUCTION) {
            InputManager.getInstance().turnOn();
            hudController.showTimer();
            timer.startTimer();
        }
        else
        {
            Debug.Log("Destruction state");
            setDemolitionState();
        }
    }

    private void setDemolitionState()
    {
        hudController.transitionFlash();
        turn.registerPlayer(1, 5);
        turn.registerPlayer(2, 8);
        hudController.hideTimer();

        GameObject[] constructorToDestroy = GameObject.FindGameObjectsWithTag("Constructor");
        foreach (var constructor in constructorToDestroy)
        {
            constructor.SetActive(false);
            Destroy(constructor);
        }
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Bloc");
        for (int i = 0; i < obj.Length; ++i)
        {
            if (!obj[i].GetComponent<SpaceTowers.BlocBehavior>().isLaid)
                Destroy(obj[i]);
        }
        GameObject.Find("CursorP1").GetComponent<Renderer>().enabled = true;
        GameObject.Find("CursorP1").transform.position = GameObject.Find("Starting Point P2").transform.position;
        GameObject.Find("CursorP2").GetComponent<Renderer>().enabled = true;
        GameObject.Find("CursorP2").transform.position = GameObject.Find("Starting Point P1").transform.position;
    }

    public GameState getGameState()
    {
        return currentState;
    }

    private void timerOver()
    {
        setGameState(GameState.DEMOLITION);
    }

    public void StartChildCoroutine(IEnumerator coroutineMethod)
    {
        StartCoroutine(coroutineMethod);
    }

    public Timer getTimer()
    {
        return timer;
    }
    public Timer getTimerIntro()
    {
        return startTimer;
    }

    internal HUDController getHUDController()
    {
        return hudController;
    }

    public MunitionRepository getMunitionRepository()
    {
        return munitionRepository;
    }

    public BlockManager getBlockManager()
    {
        return blockManager;
    }

    internal int getSelectedBlockLevel(int _playerID)
    {
        if(_playerID == 1)
        {
            return selectedMunitionLevelPlayer1;
        }
        else
        {
            return selectedMunitionLevelPlayer2;
        }
    }
}
