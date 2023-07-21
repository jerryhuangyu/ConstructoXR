using System;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class AppEvents : MonoBehaviour
{
    public static AppEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onTriggerIpPanel;
    public event Action onTriggerWorldRotatePanel;
    public event Action onTriggerRosCommandPanel;
    public event Action onTriggerRosControlPanel;
    public event Action<SliderEventData> onWorldRotateYawSliderTrigger;
    public event Action<SliderEventData> onWorldRotateDirXSliderTrigger;
    public event Action<SliderEventData> onWorldRotateDirZSliderTrigger;

    public event Action onIpNumpad0Trigger;
    public event Action onIpNumpad1Trigger;
    public event Action onIpNumpad2Trigger;
    public event Action onIpNumpad3Trigger;
    public event Action onIpNumpad4Trigger;
    public event Action onIpNumpad5Trigger;
    public event Action onIpNumpad6Trigger;
    public event Action onIpNumpad7Trigger;
    public event Action onIpNumpad8Trigger;
    public event Action onIpNumpad9Trigger;
    public event Action onIpNumpadEmptyTrigger;
    public event Action onIpNumpadFreshTrigger;
    public event Action onIpNumpadDotTrigger;
    public event Action onIpNumpadBackspaceTrigger;
    public event Action onIpNumpadCloseTrigger;

    public event Action onRobotControlForwardTrigger;
    public event Action onRobotControlBackwardTrigger;
    public event Action onRobotControlRightTrigger;
    public event Action onRobotControlLeftTrigger;
    public event Action onRobotControlClockwiseTrigger;
    public event Action onRobotControlCounterClockwiseTrigger;
    public event Action onRobotControlStopTrigger;

    // function panel relative trigger
    public event Action onPlaceNavGoalTrigger;
    public event Action onPublishNavGoalTrigger;
    public event Action onDrawMapTrigger;
    public event Action onPublishUpdateMapTrigger;
    public event Action onMapVisualOptionTrigger;
    public event Action onRobotVisualOptionTrigger;

    public void RobotControlForwardTrigger()
    {
        if (onRobotControlForwardTrigger != null)
        {
            onRobotControlForwardTrigger();
        }
    }
    public void RobotControlBackwardTrigger()
    {
        if (onRobotControlBackwardTrigger != null)
        {
            onRobotControlBackwardTrigger();
        }
    }
    public void RobotControlRightTrigger()
    {
        if (onRobotControlRightTrigger != null)
        {
            onRobotControlRightTrigger();
        }
    }
    public void RobotControlLeftTrigger()
    {
        if (onRobotControlLeftTrigger != null)
        {
            onRobotControlLeftTrigger();
        }
    }
    public void RobotControlClockwiseTrigger()
    {
        if (onRobotControlClockwiseTrigger != null)
        {
            onRobotControlClockwiseTrigger();
        }
    }
    public void RobotControlCounterClockwiseTrigger()
    {
        if (onRobotControlCounterClockwiseTrigger != null)
        {
            onRobotControlCounterClockwiseTrigger();
        }
    }
    public void RobotControlStopTrigger()
    {
        if (onRobotControlStopTrigger != null)
        {
            onRobotControlStopTrigger();
        }
    }
    public void TriggerIpPanel()
    {
        if (onTriggerIpPanel != null)
        {
            onTriggerIpPanel();
        }
    }
    public void TriggerWorldRotatePanel()
    {
        if (onTriggerWorldRotatePanel != null)
        {
            onTriggerWorldRotatePanel();
        }
    }
    public void TriggerRosCommandPanel()
    {
        if (onTriggerRosCommandPanel != null)
        {
            onTriggerRosCommandPanel();
        }
    }
    public void TriggerRosControlPanel()
    {
        if (onTriggerRosControlPanel != null)
        {
            onTriggerRosControlPanel();
        }
    }
    public void WorldRotateYawTrigger(SliderEventData eventData)
    {
        if (onWorldRotateYawSliderTrigger != null)
        {
            onWorldRotateYawSliderTrigger(eventData);
        }
    }
    public void WorldRotateDirXSliderTrigger(SliderEventData eventData)
    {
        if (onWorldRotateDirXSliderTrigger != null)
        {
            onWorldRotateDirXSliderTrigger(eventData);
        }
    }
    public void WorldRotateDirZSliderTrigger(SliderEventData eventData)
    {
        if (onWorldRotateDirZSliderTrigger != null)
        {
            onWorldRotateDirZSliderTrigger(eventData);
        }
    }
    public void IpNumpad0Trigger()
    {
        if (onIpNumpad0Trigger != null)
        {

            onIpNumpad0Trigger();
        }
    }
    public void IpNumpad1Trigger()
    {
        if (onIpNumpad1Trigger != null)
        {

            onIpNumpad1Trigger();
        }
    }
    public void IpNumpad2Trigger()
    {
        if (onIpNumpad2Trigger != null)
        {

            onIpNumpad2Trigger();
        }
    }
    public void IpNumpad3Trigger()
    {
        if (onIpNumpad3Trigger != null)
        {

            onIpNumpad3Trigger();
        }
    }
    public void IpNumpad4Trigger()
    {
        if (onIpNumpad4Trigger != null)
        {

            onIpNumpad4Trigger();
        }
    }
    public void IpNumpad5Trigger()
    {
        if (onIpNumpad5Trigger != null)
        {

            onIpNumpad5Trigger();
        }
    }
    public void IpNumpad6Trigger()
    {
        if (onIpNumpad6Trigger != null)
        {

            onIpNumpad6Trigger();
        }
    }
    public void IpNumpad7Trigger()
    {
        if (onIpNumpad7Trigger != null)
        {

            onIpNumpad7Trigger();
        }
    }
    public void IpNumpad8Trigger()
    {
        if (onIpNumpad8Trigger != null)
        {

            onIpNumpad8Trigger();
        }
    }
    public void IpNumpad9Trigger()
    {
        if (onIpNumpad9Trigger != null)
        {

            onIpNumpad9Trigger();
        }
    }
    public void IpNumpadEmptyTrigger()
    {
        if (onIpNumpadEmptyTrigger != null)
        {

            onIpNumpadEmptyTrigger();
        }
    }
    public void IpNumpadFreshTrigger()
    {
        if (onIpNumpadFreshTrigger != null)
        {
            onIpNumpadFreshTrigger();
        }
    }
    public void IpNumpadDotTrigger()
    {
        if (onIpNumpadDotTrigger != null)
        {
            onIpNumpadDotTrigger();
        }
    }
    public void IpNumpadBackspaceTrigger()
    {
        if (onIpNumpadBackspaceTrigger != null)
        {
            onIpNumpadBackspaceTrigger();
        }
    }
    public void IpNumpadCloseTrigger()
    {
        if (onIpNumpadCloseTrigger != null)
        {
            onIpNumpadCloseTrigger();
        }
    }
    public void PlaceNavGoalTrigger()
    {
        if (onPlaceNavGoalTrigger != null)
        {
            onPlaceNavGoalTrigger();
        }
    }
    public void PublishNavGoalTrigger()
    {
        if (onPublishNavGoalTrigger != null)
        {
            onPublishNavGoalTrigger();
        }
    }
    public void DrawMapTrigger()
    {
        if (onDrawMapTrigger != null)
        {
            onDrawMapTrigger();
        }
    }
    public void PublishUpdateMapTrigger()
    {
        if (onPublishUpdateMapTrigger != null)
        {
            onPublishUpdateMapTrigger();
        }
    }
    public void MapVisualOptionTrigger()
    {
        if (onMapVisualOptionTrigger != null)
        {
            onMapVisualOptionTrigger();
        }
    }
    
    public void RobotVisualOptionTrigger()
    {
        if (onRobotVisualOptionTrigger != null)
        {
            onRobotVisualOptionTrigger();
        }
    }

}
