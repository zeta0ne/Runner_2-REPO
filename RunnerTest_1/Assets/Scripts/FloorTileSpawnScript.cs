using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileSpawnScript : MonoBehaviour
{
    //[SerializeField]
    public GameObject floorTile;

    public List<GameObject> floorTilesPool;
    public int floorTilesPoolAmount = 20;
    public int floorTilesOnScreen = 10;
    private float floorTileLength = 10f;
 

    // Start is called before the first frame update
    void OnEnable()
    {
        floorTilesPool = new List<GameObject>();
        for (int i = 0; i < floorTilesPoolAmount; i++)
        {
            GameObject myFloorTile = Instantiate(floorTile) as GameObject; //creates placeholder object and instantiates prefab into it
            
            myFloorTile.SetActive(false); //object is deactivated for now
            myFloorTile.transform.SetParent(transform); //sets parent to this (empty spawner) object
            floorTilesPool.Add(myFloorTile); //adds obj to the list
        }
    }

    public GameObject GetPooledObject()
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
    private void Update()
    {
        GetPooledObject();
        if (floorTile != null)
        {
            for (int i = 0; i < floorTilesOnScreen; i++)
            {
                floorTilesPool[i].SetActive(true);
                floorTilesPool[i].transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z + floorTileLength) * i);
            }
        }  
    }

 

    void DeactivateTile()
    {
        floorTile.SetActive(false);
    }
   

  
}
