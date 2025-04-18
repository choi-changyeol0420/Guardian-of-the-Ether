using UnityEngine;

public class EnemyDead : MonoBehaviour
{
    public GameObject parent;
    public void Die()
    {
        parent.SetActive(false);
    }
}
