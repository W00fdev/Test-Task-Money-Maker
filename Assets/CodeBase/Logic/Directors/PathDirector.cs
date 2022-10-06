using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathDirector : MonoBehaviour
{
    // No reasons for singletones / DI.
    [SerializeField] private Bootstrapper _bootstrapper;

    [SerializeField] private Camera _mainCamera;

    private IInputService _inputService;
    private PathDrawer _pathDrawer;

    private readonly Queue<Vector3> _waypoints = new Queue<Vector3>();
    private Vector3Wrapper _nextTargetCached;

    // Prevents every frame update check in movablePlayer.
    public UnityAction OnWaypointAdded;
    public int WaypointsCount => _waypoints.Count;

    private void Start()
    {
        _inputService = _bootstrapper.GetInputService();
        _nextTargetCached = new Vector3Wrapper(Vector3.zero);
        
        TryGetComponent(out _pathDrawer);
    }

    private void Update()
    {
        if (_inputService.IsClickedHandle(out Vector3Wrapper clickPosition))
        {
            Vector3 mousePositionInWorld = _mainCamera.ScreenToWorldPoint(clickPosition.GetVector());
            mousePositionInWorld.z = 0;

            _waypoints.Enqueue(mousePositionInWorld);
            _pathDrawer.AddVertexToPath(mousePositionInWorld);

            OnWaypointAdded?.Invoke();
        }
    }

    public bool TryGetNextTarget(out Vector3Wrapper nextTarget)
    {
        nextTarget = _nextTargetCached;

        if (_waypoints.Count <= 0)
            return false;

        nextTarget.GetVectorByRef() = _waypoints.Dequeue();
        return true;
    }
}
