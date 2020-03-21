using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileSpawnScript : MonoBehaviour
{
    public GameObject[] floorTile;
   
    private float tileLength = 10;
    private GameObject player;
    public float amountOfTilesOnScreen = 10;
    private float spawnStart = -10;
    
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < amountOfTilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //if (player.transform.z )
        //SpawnTile();
        //DeleteTile();
    }
    void SpawnTile()
    {
        GameObject tile;
        tile = Instantiate(floorTile[0]) as GameObject;
        tile.transform.SetParent(transform);
        tile.transform.position = Vector3.forward * spawnStart; //spawns tiles at z
        spawnStart += tileLength; //increases z by 10 


    }

    void DeleteTile()
    {

    }
}
