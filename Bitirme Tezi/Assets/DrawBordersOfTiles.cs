using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DrawBordersOfTiles : MonoBehaviour
{
    // Start is called before the first frame update
    public Tilemap groundTileMap;
    public List<Vector3> tileWorldLocations;


    public void Start()
    {

        BoundsInt bounds = groundTileMap.cellBounds;
        TileBase[] allTiles = groundTileMap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                }
                else
                {
                    //Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }

    }

}
