using UnityEngine;

namespace Enemy
{
    public static class EnemyAnimHash
    {
        //attacks
        public static int StabHash = Animator.StringToHash("Stab");
        public static int QuickSwipeHash = Animator.StringToHash("QuickSwipe");
        public static int ComboKickHash = Animator.StringToHash("ComboKick");   
        
        public static int IsHitHash = Animator.StringToHash("IsHit");  
        public static int IsDeadHash = Animator.StringToHash("IsDead");
        
        public static int RunningHash = Animator.StringToHash("IsRunning");
        
    }
}