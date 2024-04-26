using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeSprite : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;   // Ÿ�ϸ� ������Ʈ�� ���� ����
    [SerializeField] private TileBase[] oldTile; // ��ü�� ���� Ÿ�� �迭
    [SerializeField] private TileBase[] newTile; // ���� Ÿ���� ��ü�� �� Ÿ�� �迭

    private void Update()
    {
        // ���콺 ���� ��ư Ŭ�� ���� Ȯ�� ( �ӽ��� )
        if (Input.GetMouseButtonDown(0))
        {
            ChangeTiles();
        }
    }

    private void ChangeTiles()
    {
        // Ÿ�ϸ��� ��踦 ������
        BoundsInt bounds = tilemap.cellBounds;

        // oldTile �迭�� �ݺ���
        for (int i = 0; i < oldTile.Length; i++)
        {
            // Ÿ�ϸ� ��� ���� ��� ��ġ�� �ݺ���
            foreach (Vector3Int pos in bounds.allPositionsWithin)
            {
                // ���� ��ġ(pos)�� Ÿ���� ���� Ÿ�ϰ� ������ Ȯ��
                if (tilemap.GetTile(pos) == oldTile[i])
                {
                    // ���ٸ� �ش� ��ġ�� Ÿ���� �� Ÿ�Ϸ� ��ü
                    tilemap.SetTile(pos, newTile[i]);
                }
                // ���� ��ġ(pos)�� Ÿ���� �� Ÿ�ϰ� ������ Ȯ��
                else if (tilemap.GetTile(pos) == newTile[i])
                {
                    // ���ٸ� �ش� ��ġ�� Ÿ���� ���� Ÿ�Ϸ� ��ü
                    tilemap.SetTile(pos, oldTile[i]);
                }
            }

            // ���� �ݺ��� ���� oldTile�� newTile �迭�� ��ȯ
            TileBase temp = oldTile[i];
            oldTile[i] = newTile[i];
            newTile[i] = temp;
        }
    }
}
