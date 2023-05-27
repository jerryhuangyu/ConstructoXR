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
        /// 發布以箭頭參考物件為基準的移動目標指令
        /// </summary>
        private void PublishNavGoal()
        {
            // 確認功能是否被啟用
            if (GlobalState.isPlaceNavGoalRef)
            {
                // calculate nav target for move_base
                var refRotation = navGoalRef.transform.rotation * Quaternion.Euler(0, 270, 0); // 更正模型箭頭指向非 forward，旋轉 270 deg
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
        /// 還原因校正地圖所造成的旋轉與偏移
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public (Vector3, Quaternion) RevertFitRevise(Vector3 position, Quaternion rotation)
        {
            // 利用 ref 物件，還原校正的(1)旋轉與(2)旋轉造成的平移之影響
            GameObject refObj = Instantiate(pointRef, position, rotation);
            refObj.transform.RotateAround(GlobalState.afterFitPosition, Vector3.up, -GlobalState.totalRotateAngle);
            Quaternion reviceRotation = refObj.transform.rotation;
            // 還原校正的平移影響
            Vector3 revicePosition = refObj.transform.position - (GlobalState.afterFitPosition - GlobalState.originPosition);
            return (revicePosition, reviceRotation);
        }
    }
}