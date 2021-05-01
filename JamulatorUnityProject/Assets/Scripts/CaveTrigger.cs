using UnityEngine;

public enum InCaveTrigger {
    START,
    MID,
    END
}

public class CaveTrigger : MonoBehaviour
{
    public InCaveTrigger caveTriggerType;
    
    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") {
            return;
        }
        print("in cave");
        EventManager.Instance.NotifyOfCaveEntered();
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag != "Player") {
            return;
        }
        EventManager.Instance.NotifyOfCaveExited();
    }
}
