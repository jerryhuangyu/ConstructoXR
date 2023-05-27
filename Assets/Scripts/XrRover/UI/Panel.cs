using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

namespace XrRover.UI
{
    public class Panel
    {
        static public bool Toggle(bool isToggle, GameObject panel, GameObject btn)
        {
            isToggle = !isToggle;
            btn.transform.Find("BackPlateToggleState").gameObject.SetActive(isToggle);
            panel.SetActive(isToggle);
            //panel.GetComponent<RadialView>().enabled = isToggle;

            //Debug.Log("toggle panel function work");
            return isToggle;
        }
    }
}
