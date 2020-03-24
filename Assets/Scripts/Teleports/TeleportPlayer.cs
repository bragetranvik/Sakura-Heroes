using UnityEngine;
using UnityEngine.UI;

public class TeleportPlayer : MonoBehaviour
{
    public Vector3 newPos;
    private bool playerIsInCollider = false;
    Collider2D player;
    public GameObject dialogBox;
    public Text boxText;
    [TextArea(5, 5)]
    public string dialog;

    //OnTriggerEnter is called once the player is inside the collider area
    private void OnTriggerEnter2D(Collider2D other)
    {
        playerIsInCollider = true;
        player = other;
        boxText.text = dialog;
        dialogBox.SetActive(true);
    }

    //OnTriggerExit is called once the player has left the collider area
    private void OnTriggerExit2D(Collider2D other)
    {
        playerIsInCollider = false;
        player = null;
        dialogBox.SetActive(false);
    }

    //If the player is in the collider and the keyboard button "e" is pressed down
    //the player will be teleported.
    private void Update()
    {
        if (playerIsInCollider && Input.GetKeyDown("e"))
        {
            player.transform.position = newPos;
        }
    }

    private void Start() {
        dialogBox.SetActive(false);   
    }
}
