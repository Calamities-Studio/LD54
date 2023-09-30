using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public void OnMove(InputValue value)
    {
        var v = value.Get<Vector2>();
        Debug.Log($"move: {v}");
        gameObject.transform.position += (Vector3)v;
    }
}
