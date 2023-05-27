using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Robotics.ROSTCPConnector;
using XrRover.Utilities.Ros;

namespace XrRover.UI.Controller
{
    public class IpNumPadController : MonoBehaviour
    {

        private TextMeshPro ipContent;

        // Start is called before the first frame update
        void Start()
        {
            ipContent = GameObject.Find("NumberContent").GetComponent<TextMeshPro>();

            // subscribe event
            AppEvents.current.onIpNumpad0Trigger += Append0toIP;
            AppEvents.current.onIpNumpad1Trigger += Append1toIP;
            AppEvents.current.onIpNumpad2Trigger += Append2toIP;
            AppEvents.current.onIpNumpad3Trigger += Append3toIP;
            AppEvents.current.onIpNumpad4Trigger += Append4toIP;
            AppEvents.current.onIpNumpad5Trigger += Append5toIP;
            AppEvents.current.onIpNumpad6Trigger += Append6toIP;
            AppEvents.current.onIpNumpad7Trigger += Append7toIP;
            AppEvents.current.onIpNumpad8Trigger += Append8toIP;
            AppEvents.current.onIpNumpad9Trigger += Append9toIP;
            AppEvents.current.onIpNumpadEmptyTrigger += EmptyIpContent;
            AppEvents.current.onIpNumpadFreshTrigger += FreshRosConnection;
            AppEvents.current.onIpNumpadDotTrigger += AppendDottoIP;
            AppEvents.current.onIpNumpadBackspaceTrigger += BackspaceIpContent;

        }
        /// <summary>
        /// 更新 IP panel 中的顯示文字
        /// </summary>
        public void Append0toIP()
        {
            ipContent.text += 0;
            CheckContentValid();
        }
        public void Append1toIP()
        {
            ipContent.text += 1;
            CheckContentValid();
        }
        public void Append2toIP()
        {
            ipContent.text += 2;
            CheckContentValid();
        }
        public void Append3toIP()
        {
            ipContent.text += 3;
            CheckContentValid();
        }
        public void Append4toIP()
        {
            ipContent.text += 4;
            CheckContentValid();
        }
        public void Append5toIP()
        {
            ipContent.text += 5;
            CheckContentValid();
        }
        public void Append6toIP()
        {
            ipContent.text += 6;
            CheckContentValid();
        }
        public void Append7toIP()
        {
            ipContent.text += 7;
            CheckContentValid();
        }
        public void Append8toIP()
        {
            ipContent.text += 8;
            CheckContentValid();
        }
        public void Append9toIP()
        {
            ipContent.text += 9;
            CheckContentValid();
        }
        public void EmptyIpContent()
        {
            ipContent.text = "";
        }
        public void FreshRosConnection()
        {
            ROSConnection rosConnection = GameObject.Find("ROSConnectionPrefab").GetComponent<ROSConnection>();
            RosIpHandler.Disconnect2Ros(rosConnection);
            RosIpHandler.StartConnect2Ros(rosConnection);
        }
        public void AppendDottoIP()
        {
            ipContent.text += ".";
            CheckContentValid();
        }
        public void BackspaceIpContent()
        {
            ipContent.text = ipContent.text.Substring(0, ipContent.text.Length - 1);
        }
        public void CheckContentValid()
        {
            // check ip length: 192.168.168.168 has 15 (char)
            if (ipContent.text.Length > 15)
                ipContent.text = "";
        }
    }
}
