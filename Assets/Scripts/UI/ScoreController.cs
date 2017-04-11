using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public Text scoreTextPlayer1;
    public Text scoreTextPlayer2;

    private statistiqueManager statsManager;

	void Start () {
        statsManager = LevelManager.getInstance().getStatsManager();
    }
	
	void Update () {
        updateScore();
	}

    private void updateScore()
    {
        Vector2 scores = statsManager.getScores();
        scoreTextPlayer1.text = scores.x.ToString();
        scoreTextPlayer2.text = scores.y.ToString();
    }
}
