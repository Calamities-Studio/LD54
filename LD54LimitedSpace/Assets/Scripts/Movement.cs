using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Movement : MonoBehaviour
{
    private Vector2 velocity = Vector2.zero;
    [SerializeField]
    private float speed = 4f;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    public void OnMove(InputValue value)
    {
        var v = value.Get<Vector2>();
        v *= speed;
        velocity = v;
    }

    private void Update()
    {
        #region mouse controls
        var mouse = Mouse.current;
        bool mouseIsPressed = mouse.press.value == 1;

        if (mouseIsPressed)
        {
            SetVelocityFromScreenPosition(mouse.position.value);
        }
        else if (mouse.press.wasReleasedThisFrame)
        {
            velocity = Vector2.zero;
        }
        #endregion

        #region touch controls
        foreach (var touch in Touch.activeTouches)
        {
            if (touch.ended)
                velocity = Vector2.zero;
            else
                SetVelocityFromScreenPosition(touch.screenPosition);
        }
        #endregion

        Vector3 newPlayerPosition = transform.position + (Vector3)velocity * Time.deltaTime;

        if (!CheckIfPlayerWouldBeOutOfBoundaries(newPlayerPosition))
            transform.position = newPlayerPosition;
    }

    private void SetVelocityFromScreenPosition(Vector2 screenPosition)
    {
        velocity = Camera.main.ScreenToWorldPoint(screenPosition) - transform.position;
        velocity = Vector2.ClampMagnitude(velocity, 1f) * speed;
    }

    private bool CheckIfPlayerWouldBeOutOfBoundaries(Vector2 newPosition)
    {
        Vector2 playerHalfSize = transform.localScale;
        Vector2 outOfBounds = new(8.88f - playerHalfSize.x / 2, 5f - playerHalfSize.y / 2);

        if (newPosition.x > outOfBounds.x || newPosition.x < -outOfBounds.x ||
            newPosition.y > outOfBounds.y || newPosition.y < -outOfBounds.y)
            return true;
        else
            return false;
    }
}
