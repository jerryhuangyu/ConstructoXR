using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XrRover
{
    public class GlobalState : MonoBehaviour
    {
        /// <summary>
        /// �޲z�Ҧ����쪺���A�A�Ҧp�\��O�_�Q�ҥ�
        /// </summary>
        public static bool isPlaceNavGoalRef;
        public static bool isDrawMapEnble;

        public static Vector3 originPosition;
        public static Vector3 afterFitPosition;

        public static Quaternion originQuaternion;
        public static Quaternion afterFitQuaternion;
        public static float resolution;

        public static Vector3 mapOriginEuler;
        public static float totalRotateAngle = 0.0f;

        public static bool needMapShow = true;
        public static bool needRobotShow = false;

        void Start()
        {
            isPlaceNavGoalRef = false;
            isDrawMapEnble = false;

            AppEvents.current.onPlaceNavGoalTrigger += SwitchStatePlaceNavGoal;
            AppEvents.current.onDrawMapTrigger += SwitchStateDrawMap;
        }

        private void SwitchStatePlaceNavGoal()
        {
            isPlaceNavGoalRef = !isPlaceNavGoalRef;
        }
        private void SwitchStateDrawMap()
        {
            isDrawMapEnble = !isDrawMapEnble;
        }

        public UnityRosTf GetUnityRosTransform()
        {
            Vector3 tfPosition = afterFitPosition - originPosition;
            Quaternion tfQuaternion = afterFitQuaternion = originQuaternion;
            UnityRosTf tf = new UnityRosTf(tfPosition, tfQuaternion);
            return tf;
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("origin:" + originPosition + "after" + afterFitPosition);
            }
        }
    }
}
