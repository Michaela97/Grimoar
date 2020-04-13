using System;
using UnityEngine;
using UnityEngine.UI;

namespace HealthBar
{
    public class PlayerHealthBar : MonoBehaviour
    {
        public Slider slider;

        private void Start()
        {
            slider = GameObject.Find("HealthBar").GetComponent<Slider>();
        }

        public void SetMaxHealth(int health)
        {
            if (slider != null)
            {
                slider.maxValue = health;
                slider.value = health;
            }

        }

        public void SetHealth(int health)
        {
            if (slider != null)
            {
                slider.value = health;
            }

        }
    }
}