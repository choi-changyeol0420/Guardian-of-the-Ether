using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager Instance;
    public PoolManager pool;
    public PlayerController player;
    #endregion
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

}
