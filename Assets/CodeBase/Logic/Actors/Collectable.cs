using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int Value;

    // All visual and audio impact logic can be here.

    public void Collect() => gameObject.SetActive(false);
}