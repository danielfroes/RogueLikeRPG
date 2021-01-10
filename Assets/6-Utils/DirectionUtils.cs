using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirectionSystem{
    public enum Direction{
        up,
        down,
        left,
        right
    }
    public class DirectionUtils
    {
        private static Dictionary<Direction,Vector3> dirToVec3 = 
            new Dictionary<Direction, Vector3>()
            {
                {Direction.up, Vector3.up},
                {Direction.down, Vector3.down},
                {Direction.right, Vector3.right},
                {Direction.left, Vector3.left}
            };
        

        public static Vector3 DirToVec3(Direction dir)
        {
            return dirToVec3[dir];
        }

        public static float AngleBtwDirections(Direction dirA, Direction dirB)
        {
            return Vector3.SignedAngle(dirToVec3[dirA], dirToVec3[dirB], Vector3.back);
        }

    }
}