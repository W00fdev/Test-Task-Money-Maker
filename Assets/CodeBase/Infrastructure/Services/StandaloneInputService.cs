using UnityEngine;

public class StandaloneInputService : InputService
{
    public StandaloneInputService() : base()
    {
    }

    public override bool IsClickedHandle(out Vector3Wrapper clickPosition)
    {
        clickPosition = ClickPositionCached;

        if (Input.GetMouseButtonUp(0))
        {
            clickPosition.GetVectorByRef() = Input.mousePosition;
            return true;
        }

        return false;
    }
}