using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using XrRover.UI;

public class MainPanelControl : MonoBehaviour
{
    [Header("Ip Setup")]
    [SerializeField] private GameObject ipPanel; // ip setup panel
    [SerializeField] private GameObject ipPanelButton; // btn for ip setup panel
    [Header("World Rotate")]
    [SerializeField] private GameObject worldRotatePanel; // world rotate panel
    [SerializeField] private GameObject worldRotatePanelButton; // btn for world rotate panel
    [Header("Ros Command")]
    [SerializeField] private GameObject RosCommandPanel; // ros command panel
    [SerializeField] private GameObject RosCommandPanelButton; // btn for ros command panel
    [Header("Ros Controller")]
    [SerializeField] private GameObject RobotControllerPanel; // ros controller panel
    [SerializeField] private GameObject RobotControllerPanelButton; // btn for ros controller panel


    static private bool isIpPanelOpen = false;
    private bool isWorldRotatePanelOpen = false;
    private bool isRosCommandPanelOpen = false;
    private bool isRosControllerPanelOpen = false;


    private void Start()
    {
        // init toggle btn state as false
        ipPanelButton.GetComponent<Interactable>().IsToggled = false;
        worldRotatePanelButton.GetComponent<Interactable>().IsToggled = false;
        RosCommandPanelButton.GetComponent<Interactable>().IsToggled = false;
        RobotControllerPanelButton.GetComponent<Interactable>().IsToggled = false;

        // subscribe event
        AppEvents.current.onTriggerIpPanel += TriggerRosIpSettingPanel;
        AppEvents.current.onTriggerWorldRotatePanel += TriggerWorldRotatePanel;
        AppEvents.current.onTriggerRosCommandPanel += TriggerRosCommandPanel;
        AppEvents.current.onTriggerRosControlPanel += TriggerRosControllerPanel;
    }

    public void TriggerRosIpSettingPanel()
    {
        isIpPanelOpen = Panel.Toggle(isIpPanelOpen, ipPanel, ipPanelButton);
    }

    public void TriggerWorldRotatePanel()
    {
        isWorldRotatePanelOpen = Panel.Toggle(isWorldRotatePanelOpen, worldRotatePanel, worldRotatePanelButton);
    }

    public void TriggerRosCommandPanel()
    {
        isRosCommandPanelOpen = Panel.Toggle(isRosCommandPanelOpen, RosCommandPanel, RosCommandPanelButton);
    }
    public void TriggerRosControllerPanel()
    {
        isRosControllerPanelOpen = Panel.Toggle(isRosControllerPanelOpen, RobotControllerPanel, RobotControllerPanelButton);
    }
}