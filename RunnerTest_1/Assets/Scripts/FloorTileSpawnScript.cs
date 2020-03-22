using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileSpawnScript : MonoBehaviour
{
    public GameObject floorTile;
    public List<GameObject> floorTilesPool;
    public int floorTilesPoolAmount = 10;
    

    // Start is called before the first frame update
    void Start()
    {
        floorTilesPool = new List<GameObject>();
        for (int i = 0; i < floorTilesPoolAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(floorTile); //creates placeholder object and instantiates needed object to it
            obj.SetActive(false); //object is deactivated for now
            obj.transform.SetParent(transform); //sets parent to this (empty spawner) object
            floorTilesPool.Add(obj); //adds obj to the list
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
   

  
}
