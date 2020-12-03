using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DirectionSystem;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] private  float timeBtwAttacks = 3;
    [SerializeField] private EnemyAttackData[] enemyAttacks =null;
    private Transform _playerTransform = null;
    private GameObject _attackContainer = null;    



    private void Awake()
    {
        _playerTransform = FindObjectOfType<PlayerController>().gameObject.transform;

    }

    private void Start()
    {
        StartAttacks();
    }
    
    public void StartAttacks()
    {  
        _attackContainer = new GameObject();
        _attackContainer.name = "[EnemyAttacksContainer-RunModeOnly]";
        InvokeRepeating("InvokeAttack", timeBtwAttacks, timeBtwAttacks);
    }

    public void StopAttacks()
    {
        CancelInvoke();
    }

    //invoke the attacks
    private void InvokeAttack()
    {
        int rand = Random.Range(0,enemyAttacks.Length);

        
        EnemyAttackData attack = Instantiate<EnemyAttackData>(enemyAttacks[rand], _attackContainer.transform);
        
        //Fetch a random possible direction for the attack
        Direction attackDir = attack.GetRandomPossibleDirection();

        attack.transform.position = FixAttackPosition(attackDir, attack.offsetFromBoundary, attack.gridDimension);
        // ajeitar a rotação
        attack.transform.rotation = FixAttackRotation(attack.baseSpawnSide, attackDir);
     
        // attack.transform.rotation = FixAttackRotation();
        


    }
 

    private Vector3 FixAttackPosition(Direction direction, float offsetFromBoundary, int gridDimension)
    {
        //Fetch the equivalent vec3 to the side;
        Vector3 attackDir = DirectionUtils.DirToVec3(direction);

        //Calculate the offset From The grid
        float spacing = CombatGrid.Instance.GetSpacing();
        print(spacing);
        float gridOffset = (0.5f * spacing * gridDimension - spacing * 0.5f); //Calculate the offset to the outermost tile of the grid 
        print(gridOffset);
        gridOffset += offsetFromBoundary;
        Vector3 attackOffset = attackDir * gridOffset ;
    
        //Calculate position of the tile where the player stands
        Vector3 playerPos = CombatGrid.Instance.PositionToCellCenter(_playerTransform.position);
        
        //Calculate the rand range of the attack
        Vector3 ortDir = new Vector3(Mathf.Abs(attackDir.y), Mathf.Abs(attackDir.x));
        int rand = Random.Range(0, gridDimension);  
        rand = rand - (gridDimension/2); //centralizes the value;
        Vector3 randOffset = ortDir * rand * spacing;

        return playerPos + attackOffset + randOffset;  
    }


    private Quaternion FixAttackRotation(Direction direction, Direction baseDirection)
    {
        float angleBetween = DirectionUtils.AngleBtwDirections(baseDirection, direction);
        float xAngle = 0;
        float yAngle = 0;

        // if(angleBetween == 180)
        //     xAngle =180;

        // if(angleBetween == 90)
        //     yAngle = 180;

        
        return Quaternion.Euler(xAngle,yAngle,angleBetween);
    }

}
