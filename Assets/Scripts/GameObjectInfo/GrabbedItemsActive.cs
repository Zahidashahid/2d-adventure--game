
using System.Collections.Generic;
using UnityEngine;

public class GrabbedItemsActive : MonoBehaviour
{
    #region Variables
    public static GrabbedItemsActive instance;
    public  List<SaveableObjects> collectedObjects = new List<SaveableObjects>();
    public ObjectIDController[] giftsGameObjArray;
    #endregion
    #region Awake
    void Awake()
    {
        giftsGameObjArray = GetComponentsInChildren<ObjectIDController>();
        //SaveAbleObjectsDB.instance.Save("Test");
        for (int i = 0; i < giftsGameObjArray.Length; i++)
        {
            //SaveAbleObjectsDB.instance.saveableObjects.Add(new SaveableObjects(giftsGameObjArray[i].id, giftsGameObjArray[i].grabbed) );
            collectedObjects.Add(new SaveableObjects(giftsGameObjArray[i].id, giftsGameObjArray[i].grabbed) );
            
        }
        
    }
    #endregion
    #region Update in save file when any gift is collected, based on this that object will not appear on reload scene
    public void UpdateGrabbedItemObjects(string file)
    {
        /*---Updating gift objects in file---*/
        
    }
     /*public void Grab(int idOB)
     {
         collectedObjects.Add(idOB);
         Debug.Log("Added to list");
         grabbed = true;
     }*/
   /* public void UpdateGrabbedObjects(int grbId)
    {
        for (int i = 0; i < giftsGameObjArray.Length; i++)
        {
            if(grbId ==  SaveAbleObjectsDB.instance.saveableObjects[i].id )
                SaveAbleObjectsDB.instance.saveableObjects[i].grabbed = true;
        }
    }*/
    public void OnGrabbedObject(int grbId)
    {
        /*---Grabbed objects in script---*/
        for (int i = 0; i < giftsGameObjArray.Length; i++)
        {
            if (grbId == collectedObjects[i].id)
                collectedObjects[i].grabbed = true;
        }
    }
    #endregion
}
[System.Serializable]
public class SaveableObjects
{
    public int id;
    public bool grabbed ;
    public SaveableObjects(int id, bool grb)
    {
        this.id = id;
        this.grabbed = grb;

    }
}


