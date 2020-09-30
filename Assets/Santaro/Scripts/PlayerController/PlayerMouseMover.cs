using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseMover : MonoBehaviour
{
    [SerializeField] private Vector2 canMoveMinPosition;
    [SerializeField] private Vector2 canMoveMaxPosition;

    private Rigidbody _rigidbody;
    private Vector3 mousePosition;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 20f;
        this.mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = (this.mousePosition - this.transform.position) * 10f;
        Vector3 movePosition = this.mousePosition;
        movePosition = new Vector2(Mathf.Min(this.canMoveMaxPosition.x, movePosition.x), Mathf.Min(this.canMoveMaxPosition.y, movePosition.y));
        movePosition = new Vector2(Mathf.Max(this.canMoveMinPosition.x, movePosition.x), Mathf.Max(this.canMoveMinPosition.y, movePosition.y));
        _rigidbody.MovePosition(movePosition);
    }
}
