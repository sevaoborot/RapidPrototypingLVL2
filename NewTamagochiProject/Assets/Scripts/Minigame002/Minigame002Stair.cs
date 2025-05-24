using UnityEngine;

public class Minigame002Stair : MonoBehaviour
{
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("triggered");
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GetComponent<Collider2D>().bounds.center, GetComponent<Collider2D>().bounds.size);
    }
}
