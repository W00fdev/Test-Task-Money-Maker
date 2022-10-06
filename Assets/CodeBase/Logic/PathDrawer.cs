using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathDrawer : MonoBehaviour
{
    public Transform StartTransform;
    public int PathLength => _lineRenderer.positionCount;

    private Vector3[] _pathVerticesCached = new Vector3[VertexesCachedCount];
    private LineRenderer _lineRenderer;

    private bool _enabled = true;

    private const int VertexesCachedCount = 25;
    
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 0;
    }

    private void FixedUpdate()
    {
        if (_lineRenderer.positionCount == 0 || _enabled == false)
            return;

        UpdateStartPosition();
    }

    public void AddVertexToPath(Vector3 vertex)
    {
        if (_enabled == false)
            return;

        if (PathLength == 0)
        {
            _pathVerticesCached[0] = StartTransform.position;
            _pathVerticesCached[1] = vertex;

            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPositions(_pathVerticesCached[0..2]);
        }
        else
        {
            _pathVerticesCached[PathLength] = vertex;

            _lineRenderer.positionCount++;
            _lineRenderer.SetPositions(_pathVerticesCached[0..PathLength]);
        }
    }

    public void DeleteVertexFromStart()
    {
        if (PathLength <= 2 || _enabled == false)
        {
            _lineRenderer.positionCount = 0;
            return;
        }

        Vector3[] slicedPositions = GetSlicedVertices();

        _lineRenderer.positionCount--;
        _lineRenderer.SetPositions(slicedPositions);
    }

    public void ClearPathAndDisable()
    {
        _lineRenderer.positionCount = 0;
        _enabled = false;
        _lineRenderer.enabled = false;
    }

    private void UpdateStartPosition()
    {
        _pathVerticesCached[0] = StartTransform.position;
        _lineRenderer.SetPosition(0, StartTransform.position);
    }

    private Vector3[] GetSlicedVertices()
    {
        Vector3[] slicedPositions = _pathVerticesCached[1..PathLength];
        slicedPositions[0] = StartTransform.position;

        for (int i = 0; i < slicedPositions.Length; i++)
            _pathVerticesCached[i] = slicedPositions[i];
        
        return slicedPositions;
    }
}