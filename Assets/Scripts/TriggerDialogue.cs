using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    private bool playerIsInCollider = false;
    private Collider2D npc;
    private Dialogues dialogueScript;


    // Start is called before the first frame update
    void Start()
    {
        dialogueScript = npc.GetComponent<Dialogues>();
    }

    // Run this methond when player approaches collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerIsInCollider = true;
    }

    // Rund this method when player exits collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerIsInCollider = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsInCollider && Input.GetKeyUp("e"))
        {
            dialogueScript.SetTree("dialogueTree1");
            dialogueScript.Reset();
            dialogueScript.GetCurrentDialogue();
        }
    }
}
