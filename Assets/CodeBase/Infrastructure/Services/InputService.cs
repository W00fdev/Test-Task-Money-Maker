using UnityEngine;

public abstract class InputService : IInputService
{
    protected readonly Vector3Wrapper ClickPositionCached;

    public InputService()
    {
        ClickPositionCached = new Vector3Wrapper(Vector3.zero);
    }

    public abstract bool IsClickedHandle(out Vector3Wrapper clickPosition);
}
