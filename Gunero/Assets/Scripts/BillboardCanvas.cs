using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardCanvas : MonoBehaviour
{
    Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }
    
    public void flip()
    {
        Vector3 currScale = transform.localScale;
        currScale.x *= -1;
        transform.localScale = currScale;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
