using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XrRover
{
    public class UnityRosTf
    {
        public Vector3 position;
        public Quaternion rotation;

        public UnityRosTf(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }
}
