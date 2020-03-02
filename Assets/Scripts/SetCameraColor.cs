using UnityEngine;
using Fungus;

[CommandInfo(
    "Camera", 
    "Set Camera Color",
    "Set the color for the camera")
]


public class SetCameraColor : Command
{
    public Color cameraColor;
    public override void OnEnter()
    {
        Camera.main.backgroundColor = cameraColor;

        Continue();
    }

    public override string GetSummary()
    {
        return cameraColor.ToString();
    }

    public override Color GetButtonColor()
    {
        return new Color32(216, 228, 170, 255);
    }
}
