using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TooltipText { ClickToTalk, ClickToEnter, ClickToRead}
public class ToolTip : MonoBehaviour {
    public Text tooltipText;
    public Text tooltipBorder;
    public TooltipText tooltipToShow;
    public static bool toolTipIsOn = true;

    private bool tooltipHasBeenShown = false;
    private bool toolTipIsUp = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if(toolTipIsOn && !tooltipHasBeenShown && !toolTipIsUp && other.gameObject.CompareTag("Player")) {
            tooltipHasBeenShown = true;
            toolTipIsUp = true;
            StartCoroutine(ShowTooltip(GetTooltipText()));
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
        tooltipHasBeenShown = false;
        }
    }

    /// <summary>
    /// Show the tooltip text then fades it away.
    /// </summary>
    /// <param name="stringToPlay">Text to be shown.</param>
    private IEnumerator ShowTooltip(string stringToPlay) {
        tooltipText.color = new Color(1f, 1f, 1f, 1f);
        tooltipText.text = stringToPlay;
        tooltipBorder.color = new Color(0.1960784f, 0.1960784f, 0.1960784f, 1);
        tooltipBorder.text = stringToPlay;

        for (float i = 1; i >= 0; i -= 0.01f) {
            Color newColorForText = new Color(1f, 1f, 1f, i);
            tooltipText.color = newColorForText;
            Color newColorForBorder = new Color(0.1960784f, 0.1960784f, 0.1960784f, i);
            tooltipBorder.color = newColorForBorder;
            yield return new WaitForSeconds(0.022f);
        }
        toolTipIsUp = false;
    }

    /// <summary>
    /// Return a string depending on the enum case.
    /// </summary>
    /// <returns>Tooltip text depending on the enum case.</returns>
    private string GetTooltipText() {
        string tooltipText = "";

        switch(tooltipToShow) {
            case TooltipText.ClickToTalk:
                tooltipText = "Click 'E' to talk.";
                break;

            case TooltipText.ClickToEnter:
                tooltipText = "Click 'E' to enter.";
                break;

            case TooltipText.ClickToRead:
                tooltipText = "Click 'E' to read.";
                break;
        }
        return tooltipText;
    }
}
