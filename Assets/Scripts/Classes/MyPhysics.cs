using UnityEngine;

namespace Move
{
    public class MyPhysics
    {
        public static float GetYPosDelta(float curVel, float time)
        {
            return curVel * time;
        }

        public static float GetNewYVelDelta(float time)
        {
            return 0.5f * Physics.gravity.y * time;
        }

    }
}