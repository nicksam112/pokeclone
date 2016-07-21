using UnityEngine;

public class camControl : MonoBehaviour
{
    private const int LevelArea = 100;

    private const int ScrollArea = 25;
    private const int ScrollSpeed = 25;
    private const int DragSpeed = 100;

    private const float ZoomSpeed = 1f;
    private const int ZoomMin = 100;
    private const int ZoomMax = 500;

    private const int PanSpeed = 100;
    private const int PanAngleMin = 30;
    private const int PanAngleMax = 80;

    public float speed = 1;
    float t = 0;

    public float perspectiveZoomSensitivity;

    bool isPressed = false;
    // Update is called once per frame

    //Honestly this is a mess
    //TODO: Update/fix/smooth out
    void Update()
    {
        var translation = transform.position.y;

        t += Time.deltaTime / 5f;     //  t is a float (starts at 0.0f)
        if (t > 1f) t = 0.0f;       // when t reaches 1.0f your gameObject will be at the dest rotation
        Quaternion start = transform.rotation;   // ai start rotation
        Quaternion dest = Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Player").transform.position - transform.position);     // some other rotation
        transform.rotation = Quaternion.Lerp(start, dest, t);

        if (Input.touchCount >= 2)
        {
            isPressed = true;
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0_prevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1_prevPos = touch1.position - touch1.deltaPosition;

            float prev_TouchDeltaMag = (touch0_prevPos - touch1_prevPos).magnitude;
            float current_TouchDeltaMag = (touch0.position - touch1.position).magnitude;

            float deltaMagDiff = prev_TouchDeltaMag - current_TouchDeltaMag;

            translation += deltaMagDiff * ZoomSpeed;
            translation = Mathf.Clamp(translation, 100, 400);
        }
        else
        {
            isPressed = false;
        }
        transform.position = new Vector3(transform.position.x, translation, transform.position.z);
    }
}

