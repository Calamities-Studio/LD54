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
        Debug.Log($"OnMove: {v}");

        v *= speed;
        velocity = v;
    }

    private void Update()
    {
        gameObject.transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
