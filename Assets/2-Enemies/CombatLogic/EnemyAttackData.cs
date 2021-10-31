using System.Collections.Generic;
using UnityEngine;
using DirectionSystem;

public class EnemyAttackData : MonoBehaviour
{
    public enum AttackType
    {
        OuterGrid,
        InnerGrid,
        
    }

    [Tooltip("innerGrid Attacks will spawn inside all the cells of the grid and OuterGrid Attacks will spawn in the outermost tile + the offset")]
    public AttackType attackType = AttackType.OuterGrid;

    [Tooltip("Works better when grid dimension is odd, because that way the grid has a center cell")]
    public int gridDimension = 3;
    public int offsetFromBoundary = 0;
    public Direction baseSpawnSide;

    [Range(0.0f, 100.0f)]
    public float accuracy = 90f;
    public float timeToNextAttack;

    [Header("Possible Spawn Sides")]
    [SerializeField] private bool _Up = true;
    [SerializeField] private bool _Down = true;
    [SerializeField] private bool _Right = true;
    [SerializeField] private bool _Left = true;


    [Header("Successive Attacks")]
    public int numberSuccessiveAttacks = 0;
    public float timeBetweenSuccessiveAttacks = 0;

    private float[] weights;
    private List<Direction> _possibleDirections = new List<Direction>();

    void Awake()
    {
        if (_Up) _possibleDirections.Add(Direction.up);
        if (_Down) _possibleDirections.Add(Direction.down);
        if (_Right) _possibleDirections.Add(Direction.right);
        if (_Left) _possibleDirections.Add(Direction.left);

        if (_possibleDirections.Count == 0)
        {
            Debug.LogError("ERROR:: Ataque " + gameObject.name + " não tem nenhuma direção válida");
        }
    }


    //Get a random possible direction from the possible directions list
    public Direction GetRandomPossibleDirection()
    {
        int rand = Random.Range(0, _possibleDirections.Count);
        return _possibleDirections[rand];
    }
}


