﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



public class CombatGrid : MonoBehaviour
{

    public static CombatGrid Instance{get; private set;} = null;
    private Tilemap _combatTilemap = null;

    private Grid _grid = null;
    void Awake()
    {
        if(Instance == null || Instance == this)
            Instance = this;
        else
             Destroy(this);

        _combatTilemap = GetComponentInChildren<Tilemap>();
        _grid = GetComponent<Grid>();
        
    }

    public bool IsPositionInGrid(Vector3 pos)
    {
        Vector3Int gridPos = _grid.LocalToCell(pos);
        return _combatTilemap.HasTile(gridPos);
    }

    public Vector3 PositionToCellCenter(Vector3 pos)
    {
        return _grid.CellToLocal( _grid.LocalToCell(pos)) ;
    }
    
    public float GetSpacing()
    {
        return _grid.cellSize.x;
    }
    
    //RemoveTile()
    //ChangeTile()
    //GetCell()
}
