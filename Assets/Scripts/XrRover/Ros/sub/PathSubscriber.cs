using RosMessageTypes.Nav;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using UnityEngine;

namespace XrRover.Ros.Sub
{
    /// <summary>
    /// 訂閱一個包含路徑消息的 ROS 主題，
    /// 通過使用 LineRenderer 組件和參考點，
    /// 創建一條線以可視化接收到的路徑。
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class PathSubscriber : MonoBehaviour
    {
        LineRenderer PlanLR;

        public GameObject pointRef;
        ROSConnection ros;
        [SerializeField]
        private string TopicName = "";
        private bool isTime2UpdatePlan = true; // Flag to control plan updates
        private float y_displace = (float)-1.4; // Y-axis displacement value

        void Start()
        {
            // Get the LineRenderer component attached to this GameObject
            PlanLR = GetComponent<LineRenderer>();

            // Subscribe to the specified ROS topic and specify the callback function
            ros = ROSConnection.GetOrCreateInstance();
            ros.Subscribe<PathMsg>(TopicName, VisualizePath);

            // Invoke the ToggleSwitch4ControlHZ function after a delay of 0.5 seconds
            Invoke(nameof(ToggleSwitch4ControlHZ), 0.5f);
        }

        /// <summary>
        /// Callback function for visualizing the received path from ros topic
        /// </summary>
        /// <param name="planPath"></param>
        private void VisualizePath(PathMsg planPath)
        {
            if (isTime2UpdatePlan)
            {
                // Toggle the flag to prevent frequent updates
                isTime2UpdatePlan = !isTime2UpdatePlan;

                // Get an array of poses from the received PathMsg
                RosMessageTypes.Geometry.PoseStampedMsg[] points = planPath.poses;

                // Setup LineRenderer component (length and positions)
                PlanLR.positionCount = points.Length-1;
                for (int i = 0; i < points.Length-1; i++)
                {
                    Vector3 position = points[i].pose.position.From<FLU>() + GlobalState.afterFitPosition - GlobalState.originPosition;

                    // Create a reference object (pointRef prefab) to calculate the set position for LineRenderer component
                    var showPosition = new Vector3(position.x, position.y + y_displace, position.z);
                    var refObj = CreatePointRef(showPosition);
                    refObj.transform.RotateAround(GlobalState.afterFitPosition, Vector3.up, GlobalState.totalRotateAngle);
                    PlanLR.SetPosition(i, refObj.transform.position);
                    Destroy(refObj);
                }
            }
        }

        /// <summary>
        /// Switch function to control the update rate of the plan
        /// </summary>
        private void ToggleSwitch4ControlHZ()
        {
            isTime2UpdatePlan = !isTime2UpdatePlan;

            // Invoke the function again after a delay of 0.5 seconds
            Invoke(nameof(ToggleSwitch4ControlHZ), 0.5f);
        }

        /// <summary>
        /// Create a point reference object at the specified position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private GameObject CreatePointRef(Vector3 position)
        {
            Quaternion rotation = Quaternion.identity;
            GameObject refObj = Instantiate(pointRef, position, rotation);
            return refObj;
        }
    }
}
