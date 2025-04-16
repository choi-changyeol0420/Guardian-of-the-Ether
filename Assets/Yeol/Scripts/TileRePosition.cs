using UnityEngine;

public class TileRePosition : MonoBehaviour
{
    #region Variables
    public GameObject grid; // Grid 오브젝트
    public Transform player;
    private Vector3Int playerGridPosition;
    private Vector3Int lastPlayerGridPosition;

    private Vector2 tilemapSize; // 타일맵 크기
    #endregion

    void Start()
    {
        tilemapSize = new Vector2(10.24f, 10.24f);

        // 초기 플레이어 위치 저장
        playerGridPosition = GetGridPosition(player.position);
        lastPlayerGridPosition = playerGridPosition;
    }

    void Update()
    {
        // 현재 플레이어 위치를 그리드 좌표로 변환
        playerGridPosition = GetGridPosition(player.position);

        // 플레이어가 다른 그리드 칸으로 이동했는지 확인
        if (playerGridPosition != lastPlayerGridPosition)
        {
            UpdateTilemaps();
            lastPlayerGridPosition = playerGridPosition;
        }
    }

    // 월드 좌표를 그리드 좌표로 변환
    Vector3Int GetGridPosition(Vector3 position)
    {
        return new Vector3Int(Mathf.RoundToInt(position.x / tilemapSize.x), Mathf.RoundToInt(position.y / tilemapSize.y), 0);
    }

    // 타일맵 업데이트
    void UpdateTilemaps()
    {
        Vector3Int offset = playerGridPosition - lastPlayerGridPosition;

        foreach (Transform child in grid.transform)
        {
            child.position += new Vector3(offset.x * tilemapSize.x, offset.y * tilemapSize.y, 0);
        }
    }
}
