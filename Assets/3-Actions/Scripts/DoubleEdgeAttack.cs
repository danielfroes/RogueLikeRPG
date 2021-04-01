using Squeak;
using UnityEngine;

[CreateAssetMenu(fileName = "new Action", menuName = "Action Menu SO/Action/DoubleEdgeAttack")]
public class DoubleEdgeAttack : AttackAction {

    [Range(0f, 100f)] [SerializeField] float _playerDamageLifePercentage;
    [SerializeField] Animation _playerDamageAnimation;
    
    public override void DoAction(Animator anim, EnemyStatusController enemy, PlayerStatusController player)
    {
        base.DoAction(anim, enemy, player);

        var damageAmount = player.preset.maxHealth * _playerDamageLifePercentage / 100;
        
        //Modificacoes Lucas:
        /*Habilidade não deveri tocar a animacao de dano quando castada*/
        //player._healthBar.Decrease(damageAmount);
        /*
            Criacao de uma nova funcao no jogador: HealthDecrease (meio cagado, psé) Para permitir
            Essa mudanca sem chamar o evento se não peciso faze-lo;
        */
        //player.TrueDamage(damageAmount);
        player.SelfDamage(damageAmount);
    }
}
