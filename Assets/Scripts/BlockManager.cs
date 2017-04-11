using System;
using UnityEngine;

public class BlockManager
{
    private float lastTimeChangedBlockPlayer1 = 0;
    private float lastTimeChangedBlockPlayer2 = 0;
    private InputManager inputManager;
    private HUDController hudController;

    public BlockManager(HUDController hudController)
    {
        this.inputManager = InputManager.getInstance();
        this.hudController = hudController;
    }

    public void update()
    {
        handleBlockTypeChange();
    }

    private void handleBlockTypeChange()
    {
        int left = -1;
        int right = 1;
        float minimumSecondsBetweenBlockChange = 0.2f;

        if (Time.time - lastTimeChangedBlockPlayer1 > minimumSecondsBetweenBlockChange) {
            lastTimeChangedBlockPlayer1 = Time.time;
            if (inputManager.GetAxis(SpaceTower.Inputs.LTriggerPlayer, 1) > 0.8) {
                hudController.changeMunitionSelection(left, 1);
            }
            if (inputManager.GetAxis(SpaceTower.Inputs.RTriggerPlayer, 1) > 0.8) {
                hudController.changeMunitionSelection(right, 1);
            }
        }

        if (Time.time - lastTimeChangedBlockPlayer2 > minimumSecondsBetweenBlockChange) {
            lastTimeChangedBlockPlayer2 = Time.time;
            if (inputManager.GetAxis(SpaceTower.Inputs.LTriggerPlayer, 2) > 0.8) {
                hudController.changeMunitionSelection(left, 2);
            }

            if (inputManager.GetAxis(SpaceTower.Inputs.RTriggerPlayer, 2) > 0.8) {
                hudController.changeMunitionSelection(right, 2);
            }
        }
    }

}
