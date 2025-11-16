//using UnityEngine;

//public class PlayerAnimator : MonoBehaviour
//{
//    [SerializeField] private Animator animator;

//    public void UpdateAnimator(Vector3 isoInput)
//    {
//        bool isMoving = isoInput.sqrMagnitude > 0.01f;
//        animator.SetBool("isWalking", isMoving);

//        if (isMoving)
//        {
//            animator.SetFloat("MoveX", isoInput.x);
//            animator.SetFloat("MoveZ", isoInput.z);
//        }
//    }
//}
