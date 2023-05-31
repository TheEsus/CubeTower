using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float speed = 10f;

    private void Update() {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
