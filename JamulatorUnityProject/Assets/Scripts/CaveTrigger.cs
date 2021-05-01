using UnityEngine;

public class CaveTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") {
            return;
        }
        EventManager.Instance.NotifyOfCaveEntered();
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag != "Player") {
            return;
        }
        EventManager.Instance.NotifyOfCaveExited();
    }
}
