using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Movable : MonoBehaviour
{
    [Header("For PathDrawer's DeleteVertexFromStart()")]
    public UnityEvent OnDestinationReached;
    public UnityAction<int> OnCoinCollected;

    [SerializeField] private PathDirector _pathDirector;
    [SerializeField] private float _speed;

    private Vector3 _movementTarget;
    private Vector3 _startPosition;

    private float _desiredDurationMovement;
    private float _elapsedTimeMovement;

    private bool _targetReached = true;
    private bool _isMovementAllowed = true;
    private bool _isPathExist = true;

    private Healthable _healthable;
    private Rigidbody2D _rigidbody2D;

    private const float MovementErrorDistance = 0.015f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _pathDirector.OnWaypointAdded = GetNextTarget;

        if (TryGetComponent(out _healthable) == true)
            _healthable.OnDamaged.AddListener(DisableMovement);
    }

    private void FixedUpdate()
    {
        if (_isMovementAllowed == false)
            return;

        if (_targetReached == true)
        {
            if (_isPathExist == false)
                return;
            
            GetNextTarget();
            _isPathExist = IsPathExist();
        }

        CheckTargetReached();
        MoveToTarget();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Collectable collectable))
        {
            collectable.Collect();
            OnCoinCollected?.Invoke(collectable.Value);
        }
    }
    public void EnableMovement() => SwitchMovementOn(true);
    public void DisableMovement() => SwitchMovementOn(false);

    private void CheckTargetReached()
    {
        if (_targetReached == false && IsDistanceReached() == true)
        {
            _targetReached = true;
            OnDestinationReached?.Invoke();
        }
    }

    private void GetNextTarget()
    {
        if (_targetReached == false)
        {
            _isPathExist = IsPathExist();
            return;
        }

        if (_pathDirector.TryGetNextTarget(out Vector3Wrapper newTarget))
        {
            _startPosition = transform.position;
            _movementTarget = newTarget.GetVector();

            _desiredDurationMovement = Vector3.Distance(_startPosition, _movementTarget) / _speed;
            _elapsedTimeMovement = 0f;

            _targetReached = false;
            _isPathExist = IsPathExist();
        }
    }

    private bool IsPathExist() => _pathDirector.WaypointsCount > 0;

    private bool IsDistanceReached() =>
        Vector3.Distance(transform.position, _movementTarget) < MovementErrorDistance;

    private void MoveToTarget()
    {
        _elapsedTimeMovement += _speed * Time.fixedDeltaTime;
        float t = _elapsedTimeMovement / _desiredDurationMovement;

        Vector3 newPosition = Vector3.Lerp(_startPosition, _movementTarget, Mathf.SmoothStep(0f, 1f, t));

        _rigidbody2D.MovePosition(newPosition);
    }

    private void SwitchMovementOn(bool newState)
    {
        _rigidbody2D.isKinematic = !newState;
        _isMovementAllowed = newState;
    }
}
