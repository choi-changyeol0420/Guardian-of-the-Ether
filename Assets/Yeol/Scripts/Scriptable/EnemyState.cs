using UnityEngine;

[CreateAssetMenu(fileName = "EnemyState", menuName = "Enemy/EnemyState")]
public class EnemyState : ScriptableObject
{
    public string enemyName;
    public EnemyController enemyPrefab;
    public float maxHp;
    public float attack;
    public float moveSpeed;
    public int poolSize;
    public GameObject projectTile;
    public int money;
    public float exp;
}
