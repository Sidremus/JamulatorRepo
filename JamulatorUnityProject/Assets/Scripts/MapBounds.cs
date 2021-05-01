using UnityEngine;

public class MapBounds : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") {
            return;
        }
        SubmarineState.Instance.outOfBounds = false;
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag != "Player") {
            return;
        }
        SubmarineState.Instance.outOfBounds = true;
    }
}
