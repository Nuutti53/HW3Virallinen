using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour {

    SkinnedMeshRenderer mesh;
    Collider Collider;

    //Animation
    public InputActionReference gripInput;
    public InputActionReference triggerInput;
    public InputActionReference indexInput;
    public InputActionReference thumbInput;

    private Animator animator;

    //Collider Part
    [SerializeField] private GameObject followObject;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    private Transform _followTarget;
    private Rigidbody _body;


    private void Awake() {
        animator = GetComponent<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        Collider = GetComponentInChildren<Collider>();

        //Physics
        _followTarget = followObject.transform;
        _body = GetComponent<Rigidbody>();
        _body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _body.interpolation = RigidbodyInterpolation.Interpolate;
        _body.mass = 20f;

        //Teleport hands
        _body.position = _followTarget.position;
        _body.rotation = _followTarget.rotation;

    }

    void Update() {
        if (!animator) return;

        float grip = gripInput.action.ReadValue<float>();
        float trigger = triggerInput.action.ReadValue<float>();
        float indexTouch = indexInput.action.ReadValue<float>();
        float thumbTouch = thumbInput.action.ReadValue<float>();

        animator.SetFloat("Grip", grip);
        animator.SetFloat("Trigger", trigger);
        animator.SetFloat("Index", indexTouch);
        animator.SetFloat("Thumb", thumbTouch);

        PhysicsMove();
    }

    private void PhysicsMove() {
        _body.position = _followTarget.position;
 
        //Rotation
        var rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        _body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed * Time.deltaTime);
    }

    public void ToggleVisibilityAndCollision() {
        mesh.enabled = !mesh.enabled;
        Collider.enabled = !Collider.enabled;
    }
}
