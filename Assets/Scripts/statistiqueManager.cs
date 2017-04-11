using SpaceTowers;
using UnityEngine;

public class statistiqueManager : MonoBehaviour
{
    [SerializeField]
    private int scoreMultipilcator1 = 100;
    [SerializeField]
    private int scoreMultipilcator2 = 100;
    private int scorePlayer1Total;
    private int scorePlayer2Total;

    private int scorePlayer1A;
    private int scorePlayer2A;

    private int scorePlayer1B;
    private int scorePlayer2B;

    private bool attackMode;

    public bool AttackMode
    {
        get { return attackMode; }
        set { attackMode = value; }
    }

    void Start()
    {
        scorePlayer1Total = 0;
        scorePlayer2Total = 0;

        attackMode = false;
    }

    void Update()
    {
        calculatePlayerScore();
    }

    void calculatePlayerScore()
    {

        float player1Max = 0;
        float player2Max = 0;

        GameObject[] blockArray = GameObject.FindGameObjectsWithTag("Bloc");

        for (int i = 0; i < blockArray.Length; i++)
        {
            BlocBehavior currentBlock = blockArray[i].GetComponent<BlocBehavior>();
            if (currentBlock.isLaid) //Do not count the currently handled block
            {
                if (currentBlock.PlayerID == 1)
                {
                    if (blockArray[i].transform.position.y +7> player1Max)
                    {
                        player1Max = blockArray[i].transform.position.y + 7;
                    }
                }
                else
                {
                    if (currentBlock.transform.position.y + 7 > player2Max)
                    {
                        player2Max = blockArray[i].transform.position.y + 7;
                    }
                }
            }
        }

        if (attackMode)
        {
            scorePlayer1B = (int)(player1Max * scoreMultipilcator2);
            scorePlayer2B = (int)(player2Max * scoreMultipilcator2);
        }
        else
        {
            scorePlayer1A = (int)(player1Max * scoreMultipilcator1);
            scorePlayer2A = (int)(player2Max * scoreMultipilcator1);
        }

        scorePlayer1Total = scorePlayer1A + scorePlayer1B;
        scorePlayer2Total = scorePlayer2A + scorePlayer1B;
    }

    public Vector2 getScores()
    {
        return new Vector2(scorePlayer1Total, scorePlayer2Total);
    }
}
