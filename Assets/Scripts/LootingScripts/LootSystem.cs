using UnityEngine;
[System.Serializable]
public class ItemToSpawn
{
    public GameObject item; // Item will given for loot
    public float spawnRate;
    [HideInInspector] public float minSpawnProb, maxSpawnProb;
}
public class  LootSystem: MonoBehaviour
{
    public ItemToSpawn[] itemToSpawn;// Array of class 
     //int index;
    void Start()
    {
        for (int i = 0; i < itemToSpawn.Length; i++)
        {
            if (i == 0)
            {
                itemToSpawn[i].minSpawnProb = 0;
                itemToSpawn[i].maxSpawnProb = itemToSpawn[i].spawnRate - 1;
            }
            else
            {
                itemToSpawn[i].minSpawnProb = itemToSpawn[i - 1].maxSpawnProb + 1; // Range of min and max value of to spwan the loot 
                itemToSpawn[i].maxSpawnProb = itemToSpawn[i].minSpawnProb + itemToSpawn[i].spawnRate - 1;
            }
        }
    }
    public void Spawnner(Transform t)
    {
        float randomNum = Random.Range(0, 100);
       /* Debug.Log("Here transform " + t);
        Debug.Log("position " + t.position);*/
        for (int index = 0; index < itemToSpawn.Length; index++)
        {
            if (randomNum >= itemToSpawn[index].minSpawnProb && randomNum <= itemToSpawn[index].maxSpawnProb)
            {
                Debug.Log(randomNum + " " + itemToSpawn[index].item.name);
                Vector3  position =new Vector3(t.position.x, t.position.y + 5f ,t.position.z);
                Instantiate(itemToSpawn[index].item, position, Quaternion.identity);
                break;
            }
            else if(randomNum >= itemToSpawn[itemToSpawn.Length -1].minSpawnProb && randomNum <= itemToSpawn[itemToSpawn.Length - 1].maxSpawnProb)
            {
               // Debug.Log(randomNum + " No Gifts this time"  );
                break;
            }
        }
    }
}