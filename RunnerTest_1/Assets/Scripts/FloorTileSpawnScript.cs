using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileSpawnScript : MonoBehaviour
{
    
    public GameObject floorTile;

    private int poolSize = 20;
    private List<GameObject> tilesPool;
    private float tileLength = 10.0f;
    private float Zpos = -10.0f;
    private int amountOfTilesOnScreen = 10;
    private Transform playerTr;
    private float safeSpace = 15.0f;


    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;

        tilesPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(floorTile);
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            tilesPool.Add(obj);
        }

        for (int i = 0; i < amountOfTilesOnScreen; i++) 
        {
            TileSpawn();
        }
        
    }

    private void FixedUpdate()
    {
        Debug.Log(GameObject.FindGameObjectsWithTag("Floor Tile").Length); 

        if (playerTr.position.z - safeSpace > (Zpos - amountOfTilesOnScreen * tileLength))
        {
            TileSpawn();
            DeactivateTile();
        }
    }

    void TileSpawn()
    {
        for (int i = 0; i < tilesPool.Count; i++)
        {
            if (!tilesPool[i].activeInHierarchy)
            {
                tilesPool[i].SetActive(true);
                tilesPool[i].transform.position = Vector3.forward * Zpos;
                Zpos += tileLength;
                break;
            }
        }
    }

    void DeactivateTile()
    {
        for (int i = tilesPool.Count - 1; i >= 0; --i)
        {
            if (tilesPool[i].activeInHierarchy)
            {
                tilesPool[i].SetActive(false);
                break;
            }
        }
    }
}
//for(i = pool.Count - 1; i >=0; —i)
