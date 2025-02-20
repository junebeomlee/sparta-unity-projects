using System;
using UnityEngine;

namespace Scene.World
{
    public class UIManager: MonoBehaviour
    {
        private static UIManager _instance;
        public static UIManager Instance => _instance;

        public GameObject informationText;

        private void Awake()
        {
            _instance = this;
        }

        public void ShowInformationText(string text)
        {
            informationText.SetActive(true);
        }

        public void RemoveInformationText()
        {
            informationText.SetActive(false);
        }
    }
}