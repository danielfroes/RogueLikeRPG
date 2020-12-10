using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DirectionSystem;

namespace Enemy
{
    public class AttackTransformFixer
    {   
        ///<summary>
        /// Applys offsets in the attack based by the its direction and 
        /// position of the player
        ///</summary>    
        public static void FixPosition(EnemyAttackData attack, Direction direction, Vector3 playerPos)
        {
            float spacing;
            Vector3 attackDir, playerPosTile, spawnOffset, accuracyOffset;

            //Fetch the equivalent vec3 to the side;
            attackDir = DirectionUtils.DirToVec3(direction);
            spacing = CombatGrid.Instance.GetSpacing();


            spawnOffset = CalculateSpawnOffset(attack, attackDir, spacing);
            accuracyOffset = CalculateAccuracyOffset(attack, attackDir, spacing);
            //Position of the tile where the player stands
            playerPosTile = CombatGrid.Instance.PositionToCellCenter(playerPos);  

            //applys offset
            attack.transform.position = playerPosTile + spawnOffset + accuracyOffset;
        }


        public static void FixRotation(EnemyAttackData attack,Direction direction)
        {
            float angleBetween = DirectionUtils.AngleBtwDirections(direction, attack.baseSpawnSide);
            float xAngle = 0;
            float yAngle = 0;

            Debug.Log(angleBetween);

            // if(angleBetween == 180)
            //     xAngle =180;

            // if(angleBetween == 90)
            //     yAngle = 180;

            attack.transform.rotation =  Quaternion.Euler(xAngle, yAngle, angleBetween);
        }

        ///<summary>  
        /// Calculate the offset in the direction of the attack to the 
        /// attack grid's outermost tile and then applys the offset from the grid's boundary.
        ///</summary>
        private static Vector3 CalculateSpawnOffset(EnemyAttackData attack, Vector3 attackDir, float gridSpacing)
        {
            float gridOffset;
            //Calculate the offset to the outermost tile of the grid 
            gridOffset = (0.5f * gridSpacing * attack.gridDimension - gridSpacing * 0.5f);
            gridOffset += attack.offsetFromBoundary;
            return attackDir * gridOffset;
        }
        ///<summary>  
        /// Calculate the orthogonal to direction offset that its apllied 
        /// based in the attack accuracy
        /// (Control if the attack targets or dont targets the player's cell)
        ///</summary>  
        private static Vector3 CalculateAccuracyOffset(EnemyAttackData attack, Vector3 attackDir, float gridSpacing)
        {
            float[] weights = new float[attack.gridDimension];

            //Calculate the rand range of the attack
            Vector3 ortDir = new Vector3(Mathf.Abs(attackDir.y), Mathf.Abs(attackDir.x));

            for (int i = 0; i < weights.Length; i++)
            {
                // Attack targets the player's cell
                if (i == attack.gridDimension / 2)
                    weights[i] = attack.accuracy;
                // Attack miss   
                else
                    weights[i] = (100 - attack.accuracy) / (attack.gridDimension - 1);
            }
            int rand = RandomUtils.GetRandomWeightedIndex(weights);
            rand = rand - (attack.gridDimension / 2); //centralizes the value;

            return ortDir * rand * gridSpacing;
        }



    }
}