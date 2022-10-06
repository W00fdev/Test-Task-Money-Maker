using UnityEngine;

public class MobileInputService : InputService
{
    public MobileInputService() : base()
    {
    }

    public override bool IsClickedHandle(out Vector3Wrapper clickPosition)
    {
        clickPosition = ClickPositionCached;

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                clickPosition.GetVectorByRef() = Input.GetTouch(0).position;
                return true;
            }
        }

        return false;
    }
}