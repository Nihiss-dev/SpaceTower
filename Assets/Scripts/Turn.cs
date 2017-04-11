using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour {

    private int[] playerAmmos = new int[2];
    private int lastPlayer = 0;
    private LevelManager levelManager;
    private MunitionRepository repository;

    protected static Turn instance;

    void Start()
    {
        levelManager = LevelManager.getInstance();
        repository = levelManager.getMunitionRepository();
    }

    public static Turn getInstance()
    {
        if (instance == null)
        {
            instance = (Turn)FindObjectOfType(typeof(Turn));
            if (instance == null)
                Debug.LogError("An instance of " + typeof(LevelManager) +
               " is needed in the scene, but there is none.");
        }
        return instance;
    }

    public void registerPlayer(int playerID, int ammo)
    {
        playerAmmos[playerID - 1] = ammo;
    }

    public bool isBothEmptyAmmo()
    {
        return (playerAmmos[0] == 0 && playerAmmos[1] == 0);
    }

    public bool canFire(int playerID)
    {
        MunitionDAO selectedMunition = repository.getInfo(playerID, levelManager.getSelectedBlockLevel(playerID));
        if(selectedMunition.numberOfMunitionsLeft > 0)
        {
            return true;
        }else
        {
            return true;
        }
    }
}
