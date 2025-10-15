using System;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    [SerializeField] GameObject background; //referencia ao game object que contem o sprite do background
    [SerializeField] Transform player; //referencia a posicao do player para saber quando spawnar um novo background
    [SerializeField] int tileCount = 5; //quantos tiles de solo irao existir na cena ao mesmo tempo
    [SerializeField] float tileWidth;
    [SerializeField] float scrollSpeed;

    GameObject[] tiles;

    void Start()
    {
        tiles = new GameObject[tileCount];
        for (int i = 0; i < tileCount; i++)
        {
            tiles[i] = Instantiate(background, new Vector3(i * tileWidth, 0, 0), Quaternion.identity);
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

                //resposicionando o tile do background para adiante da cena a fim de recicla-lo
                tile.transform.position = new Vector3(
                    rightmost.transform.position.x + tileWidth,
                    tile.transform.position.y,
                    tile.transform.position.z
                );
                tile.GetComponentInChildren<SpriteRenderer>().sprite = background.GetComponentInChildren<SpriteRenderer>().sprite;
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