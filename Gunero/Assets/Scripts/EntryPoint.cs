﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    Room parentRoom;

    private void Start()
    {
        parentRoom = transform.parent.GetComponent<Room>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            parentRoom.AssignTarget(collision.transform);
            parentRoom.ActivateRoom();

            gameObject.SetActive(false);
        }
    }
}