using UnityEngine;

public class GroundManager : MonoBehaviour
{
    [SerializeField] GameObject[] groundPrefabs; // e.g., normal, mud, ice
    [SerializeField] int tileCount = 5;
    [SerializeField] float tileWidth = 20f;
    [SerializeField] Transform player;
    [SerializeField] Transform groundReference;

    GameObject[] tiles;

    void Start()
    {
        tiles = new GameObject[tileCount];
        for (int i = 0; i < tileCount; i++)
        {
            GameObject prefab = groundPrefabs[Random.Range(0, groundPrefabs.Length)];
            tiles[i] = Instantiate(prefab, new Vector3(i * tileWidth, groundReference.position.y, 0), Quaternion.identity);
        }
    }

    void Update()
    {
        foreach (var tile in tiles)
        {
            if (player.position.x - tile.transform.position.x > tileWidth * 1.5f)
            {
                GameObject rightmost = GetRightmostTile();
                Destroy(tile);
                GameObject prefab = groundPrefabs[Random.Range(0, groundPrefabs.Length)];
                var newTile = Instantiate(prefab, new Vector3(rightmost.transform.position.x + tileWidth, groundReference.position.y, 0), Quaternion.identity);
                tile.transform.position = newTile.transform.position;
            }
        }
    }

    GameObject GetRightmostTile()
    {
        GameObject rightmost = tiles[0];
        foreach (var t in tiles)
            if (t.transform.position.x > rightmost.transform.position.x)
                rightmost = t;
        return rightmost;
    }

}
