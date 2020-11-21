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

    private void InvokeAttack()
    {
        int rand = Random.Range(0,enemyAttacks.Length);

        
        EnemyAttackData attack = Instantiate<EnemyAttackData>(enemyAttacks[rand], _attackContainer.transform);
        
        //Fetch a random possible direction for the attack
        Direction attackDir = attack.GetRandomPossibleDirection();

        attack.transform.position = FixAttackPosition(attackDir, attack.offsetFromBoundary, attack.gridDimension);
        // ajeitar a rotação
        attack.transform.rotation = FixAttackRotation(attack.baseDirection, attackDir);
     
        // attack.transform.rotation = FixAttackRotation();
        


    }

    Vector3 FixAttackPosition(Direction direction, float offsetFromBoundary, int gridDimension)
    {
        //negate the value because you are getting the initial point of the vector;
        Vector3 attackDir = -DirectionUtils.DirToVec3(direction);

        //Calculate the offset From The grid
        float spacing = CombatGrid.Instance.GetSpacing();
        float gridOffset = (0.25f * spacing * gridDimension + spacing * 0.25f); //Calculate the offset to the outermost tile of the grid 
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

        if(angleBetween == 180)
            xAngle =180;

        if(angleBetween == 90)
            yAngle = 180;

        
        return Quaternion.Euler(xAngle,yAngle,angleBetween);
    }


    // [SerializeField] private float timeBtwAttacks;
    // private List<AttackTag> attacksLists;
    // private Animator anim;
    
    // //**Prototipo
    // private int lastRand = -1;
    

    // private void Awake() {
    //     attacksLists = new List<AttackTag>(GetComponentsInChildren<AttackTag>(true));
    //     anim = GetComponent<Animator>();
    // }

    // private void Start() {
    //       //**Prototipo
    //     InvokeRepeating("InvokeAttack", timeBtwAttacks, timeBtwAttacks);
    // }

    // //**Prototipo
    

    // //function that is called by the animation events
    // public void ActivateAttack(string tag){
    //     if(tag != null)
    //     {
    //         bool attackIsListed = false;
    //         if(tag[0] != '#')
    //         {
    //             tag = '#' + tag;
    //         }
            
    //         foreach (AttackTag attack in attacksLists)
    //         {
    //             if(attack.tag == tag)
    //             {
    //                 attackIsListed = true;
    //                 if(!attack.gameObject.activeInHierarchy)
    //                     attack.gameObject.SetActive(true);
    //                 else
    //                 {
    //                     attack.gameObject.SetActive(false);
    //                     attack.gameObject.SetActive(true);
    //                 }
    //                 break;
    //             }
    //         }

    //         if(!attackIsListed)
    //         {
    //             Debug.LogError("Tag "+ tag +" insirida não está listada no AttackManager do gameObject " + gameObject.name);
    //         }
    //     }
    //     else
    //     {
    //         Debug.LogWarning("tag não foi inserida no animation event");
    //     }
    //     //attack.SetActive(true);
    // }
}
