using UnityEngine;

namespace Player
{
    public class PlayerAnimHash
    {
        public static int attackHash = Animator.StringToHash("Attack");
        public static int deadHash = Animator.StringToHash("IsDead");
        public static int isHitHash = Animator.StringToHash("IsHit");
        public static int isRunningHash = Animator.StringToHash("Speed");
    }
}