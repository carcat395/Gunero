using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public void OpenAllDoors()
    {
        foreach(Transform child in transform)
        {
            Door doorInstance = child.GetComponent<Door>();
            doorInstance.OpenDoor();
        }
    }

    public void CloseAllDoors()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Door>().CloseDoor();
        }
    }
}
