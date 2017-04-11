using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunitionDAO {

    public int level;
    public int numberOfMunitionsLeft;
    public int numberOfBlocksLeft; //Number of munitions left with this munition
    public int maxLevelMunition;
    public int blocksPerMunition = 5;

    public MunitionDAO(int level, int maxLevelMunition, int blocksPerMunition)
    {
        this.level = level;
        this.numberOfMunitionsLeft = maxLevelMunition;
        this.numberOfBlocksLeft = blocksPerMunition;
        this.maxLevelMunition = maxLevelMunition;
        this.blocksPerMunition = blocksPerMunition;
    }
}
