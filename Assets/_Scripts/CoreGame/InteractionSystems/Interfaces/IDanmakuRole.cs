using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    public interface IDanmakuRole
    {
        public bool IsRevealed { get; protected set;}
        public DanmakuPlayerController DanmakuPlayerController { get; set; }
        public bool HasRole(DanmakuRoleEnum danmakuRoleEnum);
        public bool IsGoalReached();
        
        public bool CanRevealRole();
        public void RevealRole();
    }
}
