using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XrRover
{
    public class RobotVisualHandler : MonoBehaviour
    {
        [SerializeField] private GameObject robot;
        [SerializeField] private GameObject arrowRef;

        private GameObject instantiateRobot;

        private void Start()
        {
            AppEvents.current.onRobotVisualOptionTrigger += ToggleRobotVisualOption;
        }

        private void ToggleRobotVisualOption()
        {
            GlobalState.needRobotShow = !GlobalState.needRobotShow;
            if (GlobalState.needRobotShow)
            {
                instantiateRobot = Instantiate(robot, arrowRef.transform.position, arrowRef.transform.rotation * Quaternion.Euler(0, -90, 0));
            } else
            {
                Destroy(instantiateRobot);
            }
        }
    }
}
