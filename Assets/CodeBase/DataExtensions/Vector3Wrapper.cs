using UnityEngine;

public class Vector3Wrapper
{
    private Vector3 _vector3;

    public Vector3Wrapper(Vector3 vector3)
    {
        _vector3 = vector3;
    }

    public ref Vector3 GetVectorByRef() => ref _vector3;
    public Vector3 GetVector() => _vector3;
}
