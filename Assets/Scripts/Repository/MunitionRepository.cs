using System.Collections.Generic;
using UnityEngine;

public class MunitionRepository
{
    private int maxLevel1Munitions = 5;
    private int maxLevel2Munitions = 5;
    private int maxLevel3Munitions = 1;
    private int maxLevel4Munitions = 1;
    private int munitionToBlockFactor = 4;

    private List<MunitionDAO> databasePlayer1;
    private List<MunitionDAO> databasePlayer2;

    public MunitionRepository()
    {
        databasePlayer1 = new List<MunitionDAO>();
        databasePlayer1.Add(new MunitionDAO(1, maxLevel1Munitions, munitionToBlockFactor));
        databasePlayer1.Add(new MunitionDAO(2, maxLevel2Munitions, munitionToBlockFactor));
        databasePlayer1.Add(new MunitionDAO(3, maxLevel3Munitions, munitionToBlockFactor));
        databasePlayer1.Add(new MunitionDAO(4, maxLevel4Munitions, munitionToBlockFactor));

        databasePlayer2 = new List<MunitionDAO>();
        databasePlayer2.Add(new MunitionDAO(1, maxLevel1Munitions, munitionToBlockFactor));
        databasePlayer2.Add(new MunitionDAO(2, maxLevel2Munitions, munitionToBlockFactor));
        databasePlayer2.Add(new MunitionDAO(3, maxLevel3Munitions, munitionToBlockFactor));
        databasePlayer2.Add(new MunitionDAO(4, maxLevel4Munitions, munitionToBlockFactor));
    }

    public MunitionDAO removeMunition(int playerID, int munitionLevel, int numberOfMunitions)
    {
        if (munitionLevel < 1 || munitionLevel > 4)
        {
            Debug.LogError("A munition of that level doesnt exist");
        }
        MunitionDAO munition;
        if (playerID == 1)
        {
            munition = databasePlayer1[munitionLevel-1];
        }
        else
        {
            munition = databasePlayer2[munitionLevel-1];
        }

        if (munition.numberOfMunitionsLeft > 0)
        {
            munition.numberOfMunitionsLeft--;
        }

        return munition;
    }

    public MunitionDAO removeBlock(int playerID, int munitionLevel, int numberOfBlocks)
    {
        if (munitionLevel < 1 || munitionLevel > 4)
        {
            Debug.LogError("A munition of that level doesnt exist");
        }
        MunitionDAO munition;
        if (playerID == 1)
        {
            munition = databasePlayer1[munitionLevel-1];
        }
        else
        {
            munition = databasePlayer2[munitionLevel-1];
        }

        if (munition.numberOfBlocksLeft == 1 && munition.numberOfMunitionsLeft != 0)
        {
            removeMunition(playerID, munitionLevel, 1);
            munition.numberOfBlocksLeft = munition.blocksPerMunition;
        }
        else if (munition.numberOfBlocksLeft > 0)
        {
            munition.numberOfBlocksLeft--;
        }

        return munition;
    }

    public MunitionDAO getInfo(int playerID, int munitionLevel)
    {
        int munitionIndex = munitionLevel-1;
        if (playerID == 1)
        {
            return databasePlayer1[munitionIndex];
        }
        else
        {
            return databasePlayer2[munitionIndex];
        }
    }
}
