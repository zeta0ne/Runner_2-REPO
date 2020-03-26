using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileSpawnScript : MonoBehaviour
{
    //[SerializeField]
    public GameObject floorTile;

    public List<GameObject> floorTilesPool;
    private int floorTilesPoolAmount = 20;
    public int floorTilesOnScreen = 10;
    private float floorTileLength = 10f;
    private float pos = 0;
    private Transform playerTransform;
    private Transform lastTileTransform;
    private float safeZone = 50.0f;

    private int howMany;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        howMany = GameObject.FindGameObjectsWithTag("Floor Tile").Length;



        floorTilesPool = new List<GameObject>();
        for (int i = 0; i < floorTilesPoolAmount; i++)
        {
            GameObject myFloorTile = Instantiate(floorTile) as GameObject; //creates placeholder object and instantiates prefab into it
            myFloorTile.SetActive(false); //object is deactivated for now
            myFloorTile.transform.SetParent(transform); //sets parent to this (empty spawner) object
            floorTilesPool.Add(myFloorTile); //adds obj to the list
        }

        lastTileTransform = floorTilesPool[9].transform;
    }

    GameObject GetPooledObject() //goes through the pool and hands out active objects
    {
        for (int i = 0; i < floorTilesPool.Count; i++)
        {
            if (!floorTilesPool[i].activeInHierarchy)
            {
                return floorTilesPool[i];
            }
        }
        return null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(lastTileTransform.position.z);
        Debug.Log(howMany);

        FloorSpawning();

        if (playerTransform.position.z + safeZone > lastTileTransform.position.z)
        {
            DeactivateTile();
            
        }
      
    }

    private void FloorSpawning()
    {
        if (GetPooledObject() != null && howMany < floorTilesOnScreen) //if there are any tiles in the pool...
        {
            for (int i = 0; i < floorTilesOnScreen; i++)
            {
                SpawnTile();
            }
        }
    }

    
   
    void SpawnTile()
    {
        GameObject tile = GetPooledObject();
        if (tile != null)
        {
                tile.SetActive(true);
                tile.transform.position = Vector3.forward * pos;
                pos += floorTileLength;
        }
        
    }

   void DeactivateTile()
    {
        if (playerTransform.position.z > lastTileTransform.position.z)
        {

            for (int i = 0; i < 7; i++)
            {
                if (floorTilesPool[i].activeInHierarchy)
                {
                    floorTilesPool[i].SetActive(false);
                }

            }
        }
    }
}
