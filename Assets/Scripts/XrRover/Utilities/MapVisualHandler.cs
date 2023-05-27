using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XrRover.Utilities;

namespace XrRover
{
    public class MapVisualHandler : MonoBehaviour
    {
        void Start()
        {
            AppEvents.current.onMapVisualOptionTrigger += ToggleMapVisualOption;
        }

        public void ToggleMapVisualOption()
        {
            GlobalState.needMapShow = !GlobalState.needMapShow;
            GameObject mapObject = Map.GetMapObject();
            MeshRenderer mapRender = mapObject.GetComponent<MeshRenderer>();
            mapRender.enabled = GlobalState.needMapShow;
        }
    }
}
