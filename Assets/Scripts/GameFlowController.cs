using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    public static GameFlowController Instance;

    private bool gameLaunched = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

        public bool checkGameLaunched()
    {
        if (!gameLaunched)
        {
            gameLaunched = true;
            return false;
        }
        return gameLaunched;
    }
}
