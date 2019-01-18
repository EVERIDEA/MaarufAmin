using UnityEngine;

public class GrassTriggerManager : MonoBehaviour {

    public static GrassTriggerManager thisClass;

    public bool isClickOnGrass = false;
    public bool isReadyToGrass = false;

    private void Start()
    {
        thisClass = this;
    }

    private void OnMouseDown()
    {
        isClickOnGrass = true;
        Debug.Log("OnGrass");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            //if(!PlayerBehaviour.thisClass.isOnCathingPeople)
            //    isReadyToGrass = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            //if (!PlayerBehaviour.thisClass.isOnCathingPeople)
            //    isReadyToGrass = false;
        }
    }
}
