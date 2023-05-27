using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using TMPro;
using XrRover.UI;
using XrRover.Utilities;

public class WorldRotatePanelController : MonoBehaviour
{
    [SerializeField] private TextMeshPro yawValueText;
    [SerializeField] private TextMeshPro dirXValueText;
    [SerializeField] private TextMeshPro dirZValueText;
    // Start is called before the first frame update
    void Start()
    {
        AppEvents.current.onWorldRotateYawSliderTrigger += OnWorldRotateYawSliderTrigger;
        AppEvents.current.onWorldRotateDirXSliderTrigger += OnWorldRotateDirXSliderTrigger;
        AppEvents.current.onWorldRotateDirZSliderTrigger += OnWorldRotateDirZSliderTrigger;
    }

    public void OnWorldRotateYawSliderTrigger(SliderEventData eventData)
    {
        //Debug.Log($"Yaw: {eventData.NewValue}");
        Slider.ShowValueOnUpdated(eventData, yawValueText);
        Map.RotateWorldMap(eventData);
    }
    public void OnWorldRotateDirXSliderTrigger(SliderEventData eventData)
    {
        //Debug.Log($"Dir X: {eventData.NewValue}");
        Slider.ShowValueOnUpdated(eventData, dirXValueText);
        Map.TranslateWorldMap(eventData, XrRover.MapDir.DirX);
    }
    public void OnWorldRotateDirZSliderTrigger(SliderEventData eventData)
    {
        //Debug.Log($"Dir Z: {eventData.NewValue}");
        Slider.ShowValueOnUpdated(eventData, dirZValueText);
        Map.TranslateWorldMap(eventData, XrRover.MapDir.DirZ);
    }
}
