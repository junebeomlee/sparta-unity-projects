using System;
using UnityEngine;

namespace Actor
{
    public class GroundDetector: MonoBehaviour
    {
        [HideInInspector] public bool isGrounded = false;
        [SerializeField] private float _distance = 0.5f;
        
        // fix 스크립트 간의 연결 필요
        // private Animator _animator;
        // private static readonly int IsJump = Animator.StringToHash("isJumping");

        void Awake()
        {
            // _animator = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            if (!Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _distance) || hit.collider.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, Vector3.down * _distance);
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
            isGrounded = true;
            // if (_animator.GetBool(IsJump)) _animator.SetBool(IsJump, false);
        }
    }
}