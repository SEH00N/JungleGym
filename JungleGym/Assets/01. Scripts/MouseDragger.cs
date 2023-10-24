using UnityEngine;
using UnityEngine.Events;

public class MouseDragger : MonoBehaviour
{
    [SerializeField, Range(0, 1f)] float moveThresholdFactor = 0.01f;
    private float moveThreshold;
    private float MoveThreshold {
        get {
            Resolution resolution = Screen.currentResolution;
            return (new Vector2(resolution.width, resolution.height).magnitude * moveThresholdFactor);
        }
    }

    [SerializeField] UnityEvent<Vector2> onDraggedEvent;

    private Vector3 startPosition;
    private bool isClicked = false;

    private void Awake()
    {
        Resolution resolution = Screen.currentResolution;
        moveThreshold = (new Vector2(resolution.width, resolution.height).magnitude * moveThresholdFactor);

        Debug.Log(moveThreshold);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
            startPosition = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(0))
            isClicked = false;

        if (isClicked == false)
            return;

        Vector3 error = Input.mousePosition - startPosition;
        if(error.sqrMagnitude > MoveThreshold * MoveThreshold)
        {
            startPosition = Input.mousePosition;
            onDraggedEvent?.Invoke(error.normalized);
        }
    }
}
