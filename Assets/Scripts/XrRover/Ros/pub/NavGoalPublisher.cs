using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;
using RosMessageTypes.BuiltinInterfaces;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;

/// <summary>
/// 
/// </summary>

namespace XrRover.Ros
{
    public class NavGoalPublisher : MonoBehaviour
    {
        ROSConnection ros;
        public string topicName = "/move_base_simple/goal";
        public GameObject navGoalRef;
        public GameObject pointRef;

        void Start()
        {
            AppEvents.current.onPublishNavGoalTrigger += PublishNavGoal;

            // start the ROS connection
            ros = ROSConnection.GetOrCreateInstance();
            ros.RegisterPublisher<PoseStampedMsg>(topicName);
        }

        /// <summary>
        /// �o���H�b�Y�ѦҪ��󬰰�Ǫ����ʥؼЫ��O
        /// </summary>
        private void PublishNavGoal()
        {
            // �T�{�\��O�_�Q�ҥ�
            if (GlobalState.isPlaceNavGoalRef)
            {
                // calculate nav target for move_base
                var refRotation = navGoalRef.transform.rotation * Quaternion.Euler(0, 270, 0); // �󥿼ҫ��b�Y���V�D forward�A���� 270 deg
                var refPosition = navGoalRef.transform.position;
                var revertTransform = RevertFitRevise(refPosition, refRotation);
                var navPosition = revertTransform.Item1;
                var navRotation = revertTransform.Item2;

                // set publish topic's msg
                PoseStampedMsg navGoal = new PoseStampedMsg();
                navGoal.header.frame_id = "map";
                navGoal.pose.position = navPosition.To<FLU>();
                navGoal.pose.orientation = navRotation.To<FLU>();

                // send the msg to move_base
                ros.Publish(topicName, navGoal);
            }
        }

        /// <summary>
        /// �٭�]�ե��a�ϩҳy��������P����
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public (Vector3, Quaternion) RevertFitRevise(Vector3 position, Quaternion rotation)
        {
            // �Q�� ref ����A�٭�ե���(1)����P(2)����y�����������v�T
            GameObject refObj = Instantiate(pointRef, position, rotation);
            refObj.transform.RotateAround(GlobalState.afterFitPosition, Vector3.up, -GlobalState.totalRotateAngle);
            Quaternion reviceRotation = refObj.transform.rotation;
            // �٭�ե��������v�T
            Vector3 revicePosition = refObj.transform.position - (GlobalState.afterFitPosition - GlobalState.originPosition);
            return (revicePosition, reviceRotation);
        }
    }
}