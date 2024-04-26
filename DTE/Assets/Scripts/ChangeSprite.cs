using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeSprite : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;   // 타일맵 컴포넌트에 대한 참조
    [SerializeField] private TileBase[] oldTile; // 교체할 이전 타일 배열
    [SerializeField] private TileBase[] newTile; // 이전 타일을 교체할 새 타일 배열

    private void Update()
    {
        // 마우스 왼쪽 버튼 클릭 여부 확인 ( 임시임 )
        if (Input.GetMouseButtonDown(0))
        {
            ChangeTiles();
        }
    }

    private void ChangeTiles()
    {
        // 타일맵의 경계를 가져옴
        BoundsInt bounds = tilemap.cellBounds;

        // oldTile 배열을 반복함
        for (int i = 0; i < oldTile.Length; i++)
        {
            // 타일맵 경계 내의 모든 위치를 반복함
            foreach (Vector3Int pos in bounds.allPositionsWithin)
            {
                // 현재 위치(pos)의 타일이 이전 타일과 같은지 확인
                if (tilemap.GetTile(pos) == oldTile[i])
                {
                    // 같다면 해당 위치의 타일을 새 타일로 교체
                    tilemap.SetTile(pos, newTile[i]);
                }
                // 현재 위치(pos)의 타일이 새 타일과 같은지 확인
                else if (tilemap.GetTile(pos) == newTile[i])
                {
                    // 같다면 해당 위치의 타일을 이전 타일로 교체
                    tilemap.SetTile(pos, oldTile[i]);
                }
            }

            // 다음 반복을 위해 oldTile과 newTile 배열을 교환
            TileBase temp = oldTile[i];
            oldTile[i] = newTile[i];
            newTile[i] = temp;
        }
    }
}
