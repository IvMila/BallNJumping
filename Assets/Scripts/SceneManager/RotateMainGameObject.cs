using UnityEngine;

public class RotateMainGameObject : MonoBehaviour
{
    private Vector2 _lastPoint;
    private Vector2 _point;

    public int Multiplier = 100;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastPoint = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            _point = Input.mousePosition;

            Vector2 delta = (_lastPoint - _point) / Screen.width * Multiplier;

            _lastPoint = _point;

            transform.Rotate(Vector3.up * delta.x);
        }
    }

    public void DontRotate()
    {
        gameObject.GetComponent<RotateMainGameObject>().enabled = false;
    }
}
