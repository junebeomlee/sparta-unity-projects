using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // private CharacterController _characterController;
    // private Controls _controls;
    // private Vector3 _movementDirection = Vector3.zero;
    //
    // void Awake()
    // {
    //     _controls = new Controls();
    //     _characterController = GetComponent<CharacterController>();
    // }
    //
    void Start()
    {
        // _controls.Enable();
        // _controls.Player.Move.performed += ctx => { var value = ctx.ReadValue<Vector2>(); _movementDirection = new(value.x, 0, value.y); };
        // transform.Transition(Vector3.up * 3f, 0.3f);
    }
    //
    // void Update()
    // {
    //     _characterController.Move(transform.TransformDirection(_movementDirection) * Time.deltaTime);
    // }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }
}
