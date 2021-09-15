using UnityEngine;

public class PlayerWinHandler : MonoBehaviour
{
    [SerializeField] BattleStateController _battleStateController;
    
    
    public void TriggerWinBattleState()
    {
        _battleStateController.RestartBattle();
    }
    
}
