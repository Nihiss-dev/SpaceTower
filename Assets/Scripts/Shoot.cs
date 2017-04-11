using System.Collections;
using System.Collections.Generic;
using Assets.Audio;
using UnityEngine;
using Assets.Source.Geometry;
using Assets.Shared;
using System;

public class Shoot : BaseComponent {

    private enum side
        {
        LEFT,
        RIGHT
    };

    private List<GameObject> projectilesList;
    private LevelManager levelManager;
    private PlayerAction cannonAction;
    private int lastPlayer = 0;
    private int selectedBlock = 0;
    private MunitionRepository munitionRepository;

    [SerializeField] private float shootPower = 4000.0f;

    [SerializeField]
    private GameObject[] projectile;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private side shooterSide;
	// Use this for initialization
	void Start () {
	    cannonAction = GetComponent<PlayerAction>();
        projectilesList = new List<GameObject>();
        foreach (GameObject proj in projectile)
            projectilesList.Add(proj);

        levelManager = LevelManager.getInstance();
        munitionRepository = levelManager.getMunitionRepository();
	}

    public int Fire(float x, float y, int playerID)
    {
        int selectedBlockLevel = levelManager.getSelectedBlockLevel(playerID);
        int oneMunition = 1;
        munitionRepository.removeMunition(playerID, selectedBlockLevel, oneMunition);
        levelManager.getHUDController().updateMunitions();

        GameObject bullet;
        cannonAction.ShootAction();
        //Debug.Log("last:" + lastPlayer.ToString() + " playerid: " + playerID.ToString());
        //if (lastPlayer == playerID)
            //return -1;
        var value = Degrees.BetweenPoints(new Vector2(x, y), new Vector2(transform.position.x, transform.position.y));
        if (levelManager.getGameState() == GameState.CONTRUCTION || (shooterSide == side.RIGHT && value.Value % 360 >= 90 && value.Value % 360 <= 270))
            return -1;
        if (levelManager.getGameState() == GameState.CONTRUCTION || (shooterSide == side.LEFT && (value.Value % 360 >= 270 || value.Value % 360 <= 90)))
            return -1;
        Vector3 dir = Quaternion.AngleAxis(value.Value, Vector3.forward) * Vector3.right;

        bullet = Instantiate(projectilesList[selectedBlock], spawnPoint.position, transform.rotation);
        if (bullet.GetComponent<WoodPlankActions>() != null) {
            bullet.GetComponent<WoodPlankActions>().isWasShot = true;
        }
            
        if (bullet.GetComponent<RocksAction>() != null) {
            bullet.GetComponent<RocksAction>().isWasShot = true;
        }
        
        bullet.GetComponent<Rigidbody2D>().AddForce(dir * -shootPower);
        
        lastPlayer = playerID;
        return 0;
    }

    internal void changeSelectedBlock(int selectedBlockLevel)
    {
        selectedBlock = selectedBlockLevel - 1;
    }
}
