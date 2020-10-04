﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Touch touch;
    public float speed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                    transform.position.x + touch.deltaPosition.x * speed * Time.deltaTime,
                    transform.position.y + touch.deltaPosition.y * speed * Time.deltaTime,
                    transform.position.z
                    );
            }
        }
    }
}
