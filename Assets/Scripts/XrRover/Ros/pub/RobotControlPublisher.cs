using System.Collections;
using System.Collections.Generic;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;
using UnityEngine;

namespace XrRover.Ros.Pub
{
    public class RobotControlPublisher : MonoBehaviour
    {
        [SerializeField] private string topicName = "cmd_vel";
        private ROSConnection ros;

        void Start()
        {
            AppEvents.current.onRobotControlForwardTrigger += PublishForward;
            AppEvents.current.onRobotControlBackwardTrigger += PublishBackward;
            AppEvents.current.onRobotControlRightTrigger += PublishRight;
            AppEvents.current.onRobotControlLeftTrigger += PublishLeft;
            AppEvents.current.onRobotControlClockwiseTrigger += PublishC;
            AppEvents.current.onRobotControlCounterClockwiseTrigger += PublishCC;
            AppEvents.current.onRobotControlStopTrigger += PublishStop;

            ros = ROSConnection.GetOrCreateInstance();
            ros.RegisterPublisher<TwistMsg>(topicName);
        }

        private void PublishForward()
        {
            TwistMsg twistMsg = SetTwistAction(0.2, 0, 0);
            ros.Publish(topicName, twistMsg);
        }
        private void PublishBackward()
        {
            TwistMsg twistMsg = SetTwistAction(-0.2, 0, 0);
            ros.Publish(topicName, twistMsg);
        }
        private void PublishRight()
        {
            TwistMsg twistMsg = SetTwistAction(0, 0.2, 0);
            ros.Publish(topicName, twistMsg);
        }
        private void PublishLeft()
        {
            TwistMsg twistMsg = SetTwistAction(0, -0.2, 0);
            ros.Publish(topicName, twistMsg);
        }
        private void PublishC()
        {
            TwistMsg twistMsg = SetTwistAction(0, 0, 0.4);
            ros.Publish(topicName, twistMsg);
        }
        private void PublishCC()
        {
            TwistMsg twistMsg = SetTwistAction(0, 0, -0.4);
            ros.Publish(topicName, twistMsg);
        }
        private void PublishStop()
        {
            TwistMsg twistMsg = SetTwistAction(0, 0, 0);
            ros.Publish(topicName, twistMsg);
        }

        private TwistMsg SetTwistAction(double lx, double ly, double az)
        {
            TwistMsg twistMsg = new TwistMsg();
            twistMsg.angular.x = 0;
            twistMsg.angular.y = 0;
            twistMsg.angular.z = az;
            twistMsg.linear.x = lx;
            twistMsg.linear.y = ly;
            twistMsg.linear.z = 0;
            return twistMsg;
        }
    }
}
