using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

namespace XrRover.UI
{
    public class Slider
    {
        static public void ShowValueOnUpdated(SliderEventData eventData, TextMeshPro textValueYaw)
        {
            if (textValueYaw != null)
            {
                float degree = eventData.NewValue - 0.5f;
                textValueYaw.text = $"{degree:F1}";
            }
        }
    }
}
