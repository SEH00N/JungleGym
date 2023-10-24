using UnityEngine;
using UnityEngine.Events;

public class MouseDragger : MonoBehaviour
{
    public enum DragDirection {
        Up,
        Down,
        Left,
        Right,
        LeftUpper,
        RightUpper,
        LeftLower,
        RightLower
    }

    [SerializeField, Range(0, 1f)] float moveErrorFactor = 0.01f;
    private float MoveError {
        get {
            Resolution resolution = Screen.currentResolution;
            return (new Vector2(resolution.width, resolution.height).magnitude * moveErrorFactor);
        }
    }

    [SerializeField] UnityEvent<DragDirection> onDraggedEvent;

    private Vector2 startPosition;
    private bool isClicked = false;

    private void Awake()
    {
        Debug.Log(MoveError);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
            startPosition = Input.mousePosition;
        }

        if (isClicked == false)
            return;
        
        // if()
    }
}
