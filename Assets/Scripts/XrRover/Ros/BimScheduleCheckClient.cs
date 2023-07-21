using UnityEngine;
using RosMessageTypes.Resources;
using Unity.Robotics.ROSTCPConnector;

namespace XrRover.Ros
{
    public class BimScheduleCheckClient : MonoBehaviour
    {
        ROSConnection ros;
        [SerializeField] private string serviceName = "check_bim_schedule";
        [SerializeField] private GameObject playerCamera;
        [SerializeField] private GameObject ZoneOccupyInfo;
        [SerializeField] private GameObject ZoneUnoccupyInfo;

        void Start()
        {
            ros = ROSConnection.GetOrCreateInstance();
            ros.RegisterRosService<CheckScheduleRequest, CheckScheduleResponse>(serviceName);
        }

        public void CheckScheduleTrigger()
        {
            //string targetZone = "Computer area";
            string targetZone = "Kitchen Area";
            CheckScheduleRequest checkScheduleRequest = new CheckScheduleRequest(targetZone);
            ros.SendServiceMessage<CheckScheduleResponse>(serviceName, checkScheduleRequest, CallbackShowOccupyStatus);
        }

        public void CheckSchedule(string targetZone)
        {
            CheckScheduleRequest checkScheduleRequest = new CheckScheduleRequest(targetZone);
            ros.SendServiceMessage<CheckScheduleResponse>(serviceName, checkScheduleRequest, CallbackShowOccupyStatus);
        }

        void CallbackShowOccupyStatus(CheckScheduleResponse response)
        {
            //Debug.Log("show occupy status: " + response.occupy);
            var show = response.occupy ? ZoneOccupyInfo : ZoneUnoccupyInfo;
            var showObj = Instantiate(show, playerCamera.transform.position + playerCamera.transform.forward, playerCamera.transform.rotation);
            Destroy(showObj, 3f);
        }
    }
}
