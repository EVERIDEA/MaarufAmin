using UnityEngine;

public class GrassTriggerManager : MonoBehaviour {

    private static GrassTriggerManager _instance;

    public static GrassTriggerManager Instance {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<GrassTriggerManager>();
            return _instance;
        }
    }

    public bool isClickOnGrass = false;
    public bool isReadyToGrass = false;

    Player player;

    private void OnMouseDown()
    {
        isClickOnGrass = true;
        Debug.Log("OnGrass");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (!Player.Instance.DataPlayer.isOnCathingPeople)
                isReadyToGrass = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (!Player.Instance.DataPlayer.isOnCathingPeople)
                isReadyToGrass = false;
        }
    }
}
