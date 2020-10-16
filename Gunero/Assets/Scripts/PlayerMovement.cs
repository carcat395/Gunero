using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Touch touch;
    public float speed = 0.1f;
    public static bool isMoving;
    public GameObject inputCheck;

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
        }
    }

    void FixedUpdate()
    {
        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            transform.position = new Vector3(
                transform.position.x + touch.deltaPosition.x * speed * Time.deltaTime,
                transform.position.y + touch.deltaPosition.y * speed * Time.deltaTime,
                transform.position.z
                );

            if (inputCheck.activeSelf)
            {
                inputCheck.transform.position = new Vector3 (touch.position.x, touch.position.y, inputCheck.transform.position.z);
            }
            else
            {
                inputCheck.SetActive(true);
            }

            isMoving = true;
        }
        else
        {
            inputCheck.SetActive(false);
            isMoving = false;
        }
    }
}
