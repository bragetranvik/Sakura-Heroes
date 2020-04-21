using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;

    public string unit1;
    public string unit2;
    public string unit3;

    public int playerLevel;

    public PlayerData (GameObject player)
    {
        position = new float[3];

        // position[0] = player.transform.position.x;
        position[0] = player.GetComponent<Transform>().position.x;
        Debug.Log("PlayerData: x coordinate is " + position[0]);

        // position[1] = player.transform.position.y;
        position[1] = player.GetComponent<Transform>().position.y;
        Debug.Log("PlayerData: y coordinate is " + position[1]);

        // position[2] = player.transform.position.z;
        position[2] = player.GetComponent<Transform>().position.z;
        Debug.Log("PlayerData: z coordinate is " + position[2]);

        playerLevel = player.GetComponent<PlayerInventory>().level;
        Debug.Log("PlayerData: level is: " + playerLevel);

    }

    public float[] getCoordinates()
    {
        return position;
    }
}
