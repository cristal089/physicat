using UnityEngine;

public class GroundManager : MonoBehaviour
{
    [SerializeField] GameObject[] groundPrefabs; //prefabs dos diferentes tipos de chao, exemplo: normal, lento, rapido
    [SerializeField] Transform player;
    [SerializeField] Transform groundPosReference; //referencia a posicao na cena onde o chao sera criado
    [SerializeField] int tileCount; //quantos tiles de chao irao existir na cena ao mesmo tempo
    [SerializeField] float tileWidth;
    [SerializeField] float scrollSpeed;

    GameObject[] tiles;


    void Start()
    {
        tiles = new GameObject[tileCount];
        for (int i = 0; i < tileCount; i++)
        {
            GameObject prefab = groundPrefabs[Random.Range(0, groundPrefabs.Length)];
            tiles[i] = Instantiate(prefab, new Vector3(i * tileWidth, groundPosReference.position.y, 0), Quaternion.identity);
        }
    }

    void Update()
    {
        foreach (var tile in tiles)
        {
            if (tile == null) continue;
            tile.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        }

        foreach (var tile in tiles)
        {

            if (player.position.x - tile.transform.position.x > tileWidth * 1.5f)
            {
                GameObject rightmost = GetRightmostTile();

                GameObject prefab = groundPrefabs[Random.Range(0, groundPrefabs.Length)]; //escolhe um prefab aleatorio para usar

                //resposicionando o tile do chao para adiante da cena a fim de recicla-lo
                tile.transform.position = new Vector3(
                    rightmost.transform.position.x + tileWidth,
                    groundPosReference.position.y,
                    tile.transform.position.z
                );
                tile.GetComponentInChildren<SpriteRenderer>().sprite = prefab.GetComponentInChildren<SpriteRenderer>().sprite;
            }

        }
    }

    GameObject GetRightmostTile()
    {
        GameObject rightmost = tiles[0];
        foreach (GameObject t in tiles)
            if (t.transform.position.x > rightmost.transform.position.x)
                rightmost = t;
        return rightmost;
    }

}
