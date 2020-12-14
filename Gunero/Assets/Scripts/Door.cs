using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int doorNo;
    bool isOpen = false;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        isOpen = true;
        anim.SetBool("isOpen", isOpen);
    }

    public void CloseDoor()
    {
        isOpen = false;
        anim.SetBool("isOpen", isOpen);
    }
}
