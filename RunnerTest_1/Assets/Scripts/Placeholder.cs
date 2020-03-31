/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeholder : MonoBehaviour
{

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
}*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewObjectPool : MonoBehaviour
{

    public static NewObjectPool instance;

    /// The object prefabs which the pool can handle.
    public GameObject[] objectPrefabs;

    /// The pooled objects currently available.
    public List<GameObject>[] pooledObjects;

    /// The amount of objects of each type to buffer.
    public int[] amountToBuffer;

    public int defaultBufferAmount = 3;

    /// The container object that we will keep unused pooled objects so we dont clog up the editor with objects.
    protected GameObject containerObject;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        containerObject = new GameObject("ObjectPool");

        //Loop through the object prefabs and make a new list for each one.
        //We do this because the pool can only support prefabs set to it in the editor,
        //so we can assume the lists of pooled objects are in the same order as object prefabs in the array
        pooledObjects = new List<GameObject>[objectPrefabs.Length];

        int i = 0;
        foreach (GameObject objectPrefab in objectPrefabs)
        {
            pooledObjects[i] = new List<GameObject>();

            int bufferAmount;

            if (i < amountToBuffer.Length) bufferAmount = amountToBuffer[i];
            else
                bufferAmount = defaultBufferAmount;

            for (int n = 0; n < bufferAmount; n++)
            {
                GameObject newObj = Instantiate(objectPrefab) as GameObject;
                newObj.name = objectPrefab.name;
                PoolObject(newObj);
            }

            i++;
        }
    }

    /// <summary>
    /// Gets a new object for the name type provided.  If no object type exists or if onlypooled is true and there is no objects of that type in the pool
    /// then null will be returned.
    /// </summary>
    /// <returns>
    /// The object for type.
    /// </returns>
    /// <param name='objectType'>
    /// Object type.
    /// </param>
    /// <param name='onlyPooled'>
    /// If true, it will only return an object if there is one currently pooled.
    /// </param>
    public GameObject GetObjectForType(string objectType, bool onlyPooled)
    {
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            GameObject prefab = objectPrefabs[i];
            if (prefab.name == objectType)
            {

                if (pooledObjects[i].Count > 0)
                {
                    GameObject pooledObject = pooledObjects[i][0];
                    pooledObjects[i].RemoveAt(0);
                    pooledObject.transform.parent = null;
                    pooledObject.SetActive(true);

                    return pooledObject;

                }
                else if (!onlyPooled)
                {
                    return Instantiate(objectPrefabs[i]) as GameObject;
                }

                break;

            }
        }

        //If we have gotten here either there was no object of the specified type or non were left in the pool with onlyPooled set to true
        return null;
    }

    /// <summary>
    /// Pools the object specified.  Will not be pooled if there is no prefab of that type.
    /// </summary>
    /// <param name='obj'>
    /// Object to be pooled.
    /// </param>
    public void PoolObject(GameObject obj)
    {
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            if (objectPrefabs[i].name == obj.name)
            {
                obj.SetActive(false);
                obj.transform.parent = containerObject.transform;
                pooledObjects[i].Add(obj);
                return;
            }
        }
    }

}

