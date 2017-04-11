using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{

    [SerializeField]
    private float HSpeed = 0.4f;

    [SerializeField]
    private float VSpeed = 0.4f;

    [SerializeField]
    private Shoot shooter;

    [SerializeField]
    private int nbShoot = 3;

    [SerializeField]
    private int playerID;

    private int lastPlayer;

    private InputManager _inputManager;
    private Turn turn;

    private Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _inputManager = InputManager.getInstance();
        turn = Turn.getInstance();

        LevelManager.getInstance().setShooter(shooter, playerID);      
    }

    // Update is called once per frame
    void Update()
    {
        float HTranslation = _inputManager.GetAxis(SpaceTower.Inputs.LHorizontalPlayer, playerID) * HSpeed;
        float VTranslation = _inputManager.GetAxis(SpaceTower.Inputs.LVerticalPlayer, playerID) * VSpeed;
        rb.transform.Translate(HTranslation, VTranslation, 0);

        float minX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.05f, 0.0f, 0.0f)).x;
        float maxX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.95f, 0.0f, 0.0f)).x;
        float minY = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height * 0.05f, 0.0f)).y;
        float maxY = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height * 0.95f, 0.0f)).y;
        if (rb.transform.position.x < minX)
            rb.transform.position = new Vector2(minX, rb.position.y);
        if (rb.transform.position.x > maxX)
            rb.transform.position = new Vector2(maxX, rb.position.y);
        if (rb.transform.position.y < minY)
            rb.transform.position = new Vector2(rb.position.x, minY);
        if (rb.transform.position.y > maxY)
            rb.transform.position = new Vector2(rb.position.x, maxY);

        if (_inputManager.GetButtonDown(SpaceTower.Inputs.ButtonAPlayer, playerID)) {
                if (!turn.isBothEmptyAmmo() && turn.canFire(playerID))
                    shooter.Fire(rb.transform.position.x, transform.position.y, playerID);
        }
    }
}
