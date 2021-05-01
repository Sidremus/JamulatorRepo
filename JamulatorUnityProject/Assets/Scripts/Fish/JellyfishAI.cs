using UnityEngine;

public class JellyfishAI : BaseFishAI
{
    public float bobSpeed = 1f;
    public float bobHeight = 0.5f;

    private Vector3 pos;

    private void Start() {
        pos = transform.position;
    }

    private void FixedUpdate() {
        float newY = Mathf.Sin(Time.time * bobSpeed) * bobHeight + pos.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
