using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MunitionController : MonoBehaviour
{
    public List<Sprite> munitions;
    public Image previousSelectedMunitionPlayer1;
    public Image selectedMunitionPlayer1;
    public Image nextSelectedMunitionPlayer1;
    public Image previousSelectedMunitionPlayer2;
    public Image selectedMunitionPlayer2;
    public Image nextSelectedMunitionPlayer2;

    public Text munitionLeftTextPlayer1;
    public Text munitionLeftTextPlayer2;

    public Text blocksLeftTextPlayer1;
    public Text blocksLeftTextPlayer2;

    private int selectedMunitionIndexPlayer1;
    private int selectedMunitionIndexPlayer2;
    private MunitionRepository munitionRepository;

    void Start()
    {
        this.munitionRepository = LevelManager.getInstance().getMunitionRepository();

        selectedMunitionIndexPlayer1 = 0;
        selectedMunitionIndexPlayer2 = 0;
        previousSelectedMunitionPlayer1.sprite = munitions[2];
        selectedMunitionPlayer1.sprite = munitions[0];
        nextSelectedMunitionPlayer1.sprite = munitions[1];

        previousSelectedMunitionPlayer2.sprite = munitions[2];
        selectedMunitionPlayer2.sprite = munitions[0];
        nextSelectedMunitionPlayer2.sprite = munitions[1];

        updateCurrentlySelectedInfo();
    }

    internal void updateCurrentlySelectedInfo()
    {
        int munitionlevelPlayer1 = selectedMunitionIndexPlayer1 + 1;
        MunitionDAO player1MunitionInfo = munitionRepository.getInfo(1, munitionlevelPlayer1);
        int munitionlevelPlayer2 = selectedMunitionIndexPlayer2 + 1;
        MunitionDAO player2MunitionInfo = munitionRepository.getInfo(2, munitionlevelPlayer2);
        updateCurrentlySelectedMunitionInfo(1, player1MunitionInfo.numberOfMunitionsLeft, player1MunitionInfo.numberOfBlocksLeft);
        updateCurrentlySelectedMunitionInfo(2, player1MunitionInfo.numberOfMunitionsLeft, player1MunitionInfo.numberOfBlocksLeft);
    }

    private int changeIndex(int direction, int index)
    {
        if (direction == -1)
        {
            if (index == 0)
            {
                index = munitions.Count - 1;
            }
            else
            {
                index--;
            }
        }
        else
        {
            if (index == munitions.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
        return index;
    }

    internal void changeSelection(int direction, int playerID)
    {
        if (playerID == 1)
        {
            selectedMunitionIndexPlayer1 = changeIndex(direction, selectedMunitionIndexPlayer1);
            int previousIndex = changeIndex(1, selectedMunitionIndexPlayer1);
            previousSelectedMunitionPlayer1.sprite = munitions[previousIndex];
            selectedMunitionPlayer1.sprite = munitions[selectedMunitionIndexPlayer1];
            int nextIndex = changeIndex(-1, selectedMunitionIndexPlayer1);
            nextSelectedMunitionPlayer1.sprite = munitions[nextIndex];
        }
        else
        {
            selectedMunitionIndexPlayer2 = changeIndex(direction, selectedMunitionIndexPlayer2);
            int previousIndex = changeIndex(1, selectedMunitionIndexPlayer2);
            previousSelectedMunitionPlayer2.sprite = munitions[previousIndex];
            selectedMunitionPlayer2.sprite = munitions[selectedMunitionIndexPlayer2];
            int nextIndex = changeIndex(-1, selectedMunitionIndexPlayer2);
            nextSelectedMunitionPlayer2.sprite = munitions[nextIndex];
        }

        updateCurrentlySelectedInfo();
    }

    public void updateCurrentlySelectedMunitionInfo(int playerID, int munitionsLeft, int blocksLeft)
    {
        if (playerID == 1)
        {
            munitionLeftTextPlayer1.text = munitionsLeft.ToString();
            blocksLeftTextPlayer1.text = "x" + blocksLeft.ToString();
        }
        else
        {
            munitionLeftTextPlayer2.text = munitionsLeft.ToString();
            blocksLeftTextPlayer2.text = "x" + blocksLeft.ToString();
        }
    }
}
