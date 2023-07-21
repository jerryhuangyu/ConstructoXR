using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

namespace XrRover
{
    public class ZoneCubeHandler : MonoBehaviour, IMixedRealityPointerHandler
    {
        [SerializeField] private string areaName = "";

        public void OnPointerClicked(MixedRealityPointerEventData eventData)
        {
            var client = GameObject.Find("BimScheduleCheckClient").GetComponent<Ros.BimScheduleCheckClient>();
            if (client != null)
            {
                client.CheckSchedule(areaName);
            }
        }

        public void OnPointerDown(MixedRealityPointerEventData eventData)
        {
            
        }

        public void OnPointerDragged(MixedRealityPointerEventData eventData)
        {
            
        }

        public void OnPointerUp(MixedRealityPointerEventData eventData)
        {
            
        }

    }
}
