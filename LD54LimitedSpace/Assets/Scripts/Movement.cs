using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Vector2 velocity = Vector2.zero;
    [SerializeField]
    private float speed = 4f;

    public void OnMove(InputValue value)
    {
        var v = value.Get<Vector2>();
        v *= speed;
        velocity = v;
        Debug.Log(velocity);
    }

    private void Update()
    {
        var mouse = Mouse.current;
        bool mouseIsPressed = mouse.press.value == 1;

        if (mouseIsPressed)
        {
            var mousePosition = mouse.position.value;
            velocity = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;
            velocity = Vector2.ClampMagnitude(velocity, 1f) * speed;
            Debug.Log(velocity);
        }
        else if (mouse.press.wasReleasedThisFrame)
        {
            velocity = Vector2.zero;
        }
        
        gameObject.transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
