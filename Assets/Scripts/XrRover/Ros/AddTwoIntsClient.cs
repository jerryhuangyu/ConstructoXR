using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Resources;

namespace XrRover.Ros
{
    public class AddTwoIntsClient : MonoBehaviour
    {
        ROSConnection ros;
        public string serviceName = "add_two_ints";

        float awaitingResponseUntilTimestamp = -1;

        // Start is called before the first frame update
        void Start()
        {
            ros = ROSConnection.GetOrCreateInstance();
            ros.RegisterRosService<AddTwoIntsRequest, AddTwoIntsResponse>(serviceName);
        }

        // Update is called once per frame
        void Update()
        {
         
        }

        public void AddTwoIntsTrigger()
        {
            int a = 2;
            int b = 4;
            AddTwoIntsRequest addTwoIntsRequest = new AddTwoIntsRequest(a, b);

            ros.SendServiceMessage<AddTwoIntsResponse>(serviceName, addTwoIntsRequest, Callback_Destination);
            
            if (Time.time > awaitingResponseUntilTimestamp)
            {
                awaitingResponseUntilTimestamp = Time.time + 1.0f;
            }
        }

        void Callback_Destination(AddTwoIntsResponse response)
        {
            awaitingResponseUntilTimestamp = -1;
            Debug.Log("Answer is: " + response.sum);
        }
    }
}
