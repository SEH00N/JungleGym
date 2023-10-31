using UnityEngine;
using UnityEngine.Events;

public class MouseDragger : MonoBehaviour
{
    /// <summary>
    /// 임곗값에 대한 해상도 비율
    /// </summary>
    [SerializeField, Range(0, 1f)] float moveThresholdFactor = 0.01f;
    
    /// <summary>
    /// 드래그 감지 임곗값
    /// </summary>
    private float moveThreshold;
    // private float MoveThreshold {
    //     get {
    //         Resolution resolution = Screen.currentResolution;
    //         return (new Vector2(resolution.width, resolution.height).magnitude * moveThresholdFactor);
    //     }
    // }

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
        // 마우스를 클릭했을 때 시작점 저장 & 측정 시작
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
            startPosition = Input.mousePosition;
        }

        // 땠을 때 측정 취소
        if(Input.GetMouseButtonUp(0))
            isClicked = false;

        // 측정상태가 아니면 return
        if (isClicked == false)
            return;

        // 오차 책정 후 이벤트 발행
        Vector3 error = Input.mousePosition - startPosition;
        if(error.sqrMagnitude > moveThreshold * moveThreshold)
        {
            startPosition = Input.mousePosition;
            onDraggedEvent?.Invoke(error.normalized);
        }
    }
}
