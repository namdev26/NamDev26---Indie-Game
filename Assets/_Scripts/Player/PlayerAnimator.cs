using UnityEngine;

public class PlayerAnimator
{
    private readonly Animator _animator;

    public PlayerAnimator(Animator animator)
    {
        _animator = animator;
    }

    public void UpdateAnimation(bool isWalking, bool isJumping)
    {
        _animator.SetBool("isWalking", isWalking);
        _animator.SetBool("isJumping", isJumping);
    }

    public void TriggerJump()
    {
        _animator.SetTrigger("Jump");
    }
}
