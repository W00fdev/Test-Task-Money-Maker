using UnityEngine;

public interface IInputService
{
    bool IsClickedHandle(out Vector3Wrapper clickPosition);
}
