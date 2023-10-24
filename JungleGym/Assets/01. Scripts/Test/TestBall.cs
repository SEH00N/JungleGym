using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBall : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    
    private Vector3 currentDestination;
    private Vector3 nextDestination;

	private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
            nextDestination = Vector3.left * 5f;
        if(Input.GetKeyDown(KeyCode.RightArrow))
            nextDestination = Vector3.right * 5f;
        if(Input.GetKeyDown(KeyCode.UpArrow))
            nextDestination = Vector3.up * 5f;
        if(Input.GetKeyDown(KeyCode.DownArrow))
            nextDestination = Vector3.down * 5f;
    }

    private void FixedUpdate()
    {
        if((transform.position - currentDestination).sqrMagnitude <= 0.01f)
        {
            transform.position = currentDestination;

            if(nextDestination.sqrMagnitude > 0)
            {
                currentDestination = transform.position + nextDestination;
                nextDestination = Vector3.zero;
            }
            else
                return;
        }

        Debug.Log((transform.position - currentDestination).sqrMagnitude);
        
        Vector3 dir = (currentDestination - transform.position).normalized;
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
}
