using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    /*----------Player position change with respect to moving tile position-------------*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(transform); //Set player child object of tile so that player will move with tile
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null); //remove the tile "parent object"
        }
    }
}
