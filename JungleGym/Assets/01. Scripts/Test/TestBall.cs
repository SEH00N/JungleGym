using UnityEngine;

public class TestBall : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    
    private Vector3 destination;
    private bool isStopped = true;

    private MagneticTable<float, Vector3> directionTable = new MagneticTable<float, Vector3>();

    // 0 ~ 67.5 => 45
    // 67.5 ~ 112.5 => 90
    // 112.5 ~ 180 => 135
    // 180 ~ 247.5 => 225
    // 247.5 ~ 292.5 => 270
    // 292.5 ~ 360 => 315

    private void Awake()
    {
        directionTable.RegisterTable(1, 0f,     67.5f,  Vector3.forward * 5f);
        directionTable.RegisterTable(2, 67.5f,  112.5f, Vector3.up * 5f);
        directionTable.RegisterTable(3, 112.5f, 180f,   Vector3.left * 5f);
        directionTable.RegisterTable(4, 180f,   247.5f, Vector3.back * 5f);
        directionTable.RegisterTable(5, 247.5f, 292.5f, Vector3.down * 5f);
        directionTable.RegisterTable(6, 292.5f, 360f,   Vector3.right * 5f);
    }

    public void SetDestination(Vector2 dir)
    {
        if(isStopped == false)
            return;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(angle < 0)
            angle += 360;

        destination = transform.position;
        destination += directionTable.GetFixedValue(angle);
    }

    private bool InRange(float left, float right, float value)
    {
        return (left <= value && value <= right);
    }

    private void FixedUpdate()
    {
        isStopped = ((transform.position - destination).sqrMagnitude <= 0.01f);
        if(isStopped)
        {
            transform.position = destination;
            return;
        }

        Vector3 dir = (destination - transform.position).normalized;
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
}
