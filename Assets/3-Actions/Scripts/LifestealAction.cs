using Squeak;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

[CreateAssetMenu(fileName = "new Action", menuName = "Action Menu SO/Action/LifeSteal")]

public class LifestealAction : AttackAction
{

    [Range(0f, 100f)] [SerializeField] float _lifeStealPercentage;
    [SerializeField] Animation _healAnimation;
    
    public override void DoAction(Animator anim, EnemyStatusController enemy, PlayerStatusController player)
    {
        base.DoAction(anim, enemy, player);
        var healAmount = _lifeStealPercentage/100 * PhysicalDamage;

        /*
            Mudanças quanto a cura:
            A ideia principal da habilidade é que ela serve para curar o jogador mais enquanto
            quando a sua vida estiver baixa, essas mudancas se encaixam com essa ideia.
            Ver Changelog para entender a equação utilizada. :P
        */

        float playerx = player._healthBar.value;
        Debug.Log("Valor healthbar ="+ player._healthBar.value);
        Debug.Log("Valor Preset ="+ player.preset.maxHealth);
        Debug.Log("Valor x = " + playerx);
        healAmount = 5 * PhysicalDamage * (1 - (playerx/Mathf.Pow(playerx,playerx)));
        Debug.Log("Cura = " + healAmount);
        player.Heal(healAmount);

    }

}
