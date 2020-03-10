using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
///
public class ObjectInteraction : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown("e") && playerInRange) {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
        }
    }

    //OnTriggerEnter is called once the player is inside the collider area
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    //OnTriggerExit is called once the player has left the collider area
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            dialogBox.SetActive(false);
            playerInRange = false;
        }
    }
}
