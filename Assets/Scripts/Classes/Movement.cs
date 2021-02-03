
using UnityEngine;

namespace Move
{
    public class Movement
    {
        public static Vector3 GetDir(float yRot)
        {
            Quaternion quatRot = Quaternion.Euler(new Vector3(0f, yRot, 0f));
            return (quatRot * Vector3.forward).normalized;
        }

        public static float GetSpeed(float deltaVel, float maxVel)
        {
            return Mathf.Abs(deltaVel * maxVel);
        }
    }


}