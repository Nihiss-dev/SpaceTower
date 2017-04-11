using UnityEngine;
using UnityEngine.UI;

public class PhaseUiController : MonoBehaviour {

    public Image constructionPhaseGreenLight;
    public Image destructionPhaseGreenLight;

    void Start()
    {
        showConstructionPhase();
    }

    internal void showConstructionPhase()
    {
        constructionPhaseGreenLight.enabled = true;
        destructionPhaseGreenLight.enabled = false;
    }

    internal void showDestrutionPhase()
    {
        constructionPhaseGreenLight.enabled = false;
        destructionPhaseGreenLight.enabled = true;
    }
}
