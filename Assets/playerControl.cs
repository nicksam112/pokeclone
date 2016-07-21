using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour {
    Vector3 goal;
    float horizontalSpeed = 1;
	
	// Rotate Player
	void Update () {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                var h = horizontalSpeed * touch.deltaPosition.x;
                transform.Rotate(0, h, 0, Space.World);
            }
        }
        
	}
}
