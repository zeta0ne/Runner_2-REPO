using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileSpawnScript : MonoBehaviour
{
    
    public GameObject floorTile;

    private float poolAmount = 20;
    private List<GameObject> tilesPool;
    private float tileLength = 10.0f;
    private float Zpos = -10.0f;
    private int amountOfTilesOnScreen = 10;
    private Transform playerT;
    private float safeSpace = 15.0f;


    void Start()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        tilesPool = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(floorTile);
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

        if (playerT.position.z - safeSpace > (Zpos - amountOfTilesOnScreen * tileLength))
        {
            TileSpawn();
            DeactivateTile();
        }
        

    }

    void TileSpawn()
    {
        for (int i = 0; i < amountOfTilesOnScreen; i++)
        {
            if (!tilesPool[i].activeInHierarchy)
            {
                tilesPool[i].SetActive(true);
                tilesPool[i].transform.position = Vector3.forward * Zpos;
                Zpos += tileLength;
                //break;
            }
        }
    }

    void DeactivateTile()
    {
        for (int i = 0; i < amountOfTilesOnScreen; i++)
        {
            if (tilesPool[i].activeInHierarchy)
            {
                tilesPool[i].SetActive(false);
                break;
            }
        }
    }

    /*void TileSpawnOne()
    {
        GameObject obj;
        obj = Instantiate(floorTile) as GameObject;
        obj.transform.SetParent(transform);
        obj.transform.position = Vector3.forward * Zpos;
        Zpos += tileLength;
    } */
}
