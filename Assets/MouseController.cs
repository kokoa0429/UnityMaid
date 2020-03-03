using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 mousePosition;

    // Update is called once per frame
    void Update()
    {
        Vector3 touchScreenPosition = Input.mousePosition;
        touchScreenPosition.z = 5.0f;

        Camera gameCamera = Camera.main;
        Vector3 touchWorldPosition = gameCamera.ScreenToWorldPoint(touchScreenPosition);

        transform.position = touchWorldPosition;
    }
}
