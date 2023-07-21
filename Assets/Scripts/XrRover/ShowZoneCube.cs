using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XrRover
{
    public class ShowZoneCube : MonoBehaviour
    {
        [SerializeField] private GameObject xrlab;
        [SerializeField] private GameObject kitchenarea;
        [SerializeField] private GameObject computerarea;
        private bool isVisual = false;

        private void Start()
        {
            xrlab.SetActive(false);
            kitchenarea.SetActive(false);
            computerarea.SetActive(false);
        }

        public void ToggleZoneCube()
        {
            isVisual = !isVisual;
            xrlab.SetActive(isVisual);
            kitchenarea.SetActive(isVisual);
            computerarea.SetActive(isVisual);
        }

        public void AlignZoneCube()
        {
            var rosmap = GameObject.Find("RosMap");
            if (rosmap != null)
            {
                Transform parent = rosmap.transform;
                Transform child_xrlab = xrlab.transform;
                Transform child_kitchen = kitchenarea.transform;
                Transform child_computer = computerarea.transform;
                child_xrlab.SetParent(parent);
                child_kitchen.SetParent(parent);
                child_computer.SetParent(parent);
            }
        }
    }
}
