using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        AddPosition();
    }

    private void Update()
    {
        var lineLastPosition = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

        if (player.transform.position != lineLastPosition)
            AddPosition();
    }

    private void AddPosition()
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, player.transform.position);
    }
}
