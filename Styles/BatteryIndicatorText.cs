using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

namespace batteryMod 
{
    internal class BatteryIndicatorText : MonoBehaviour, IBatteryIndicator
    {
        Text text;
        void Awake()
        {
            GameObject gameObject = new GameObject("BatteryIndicator");
            gameObject.transform.SetParent(this.transform, false);
            gameObject.layer = this.gameObject.layer;

            text = gameObject.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource(typeof(UnityEngine.Font), "Arial.ttf") as Font;
            text.text = "N/A";
            text.rectTransform.offsetMin = new Vector2(-29f, -49f);
            text.rectTransform.offsetMax = new Vector2(29f, 49f);
            text.alignment = TextAnchor.UpperCenter;
            text.gameObject.SetActive(false);
        }

        public void SetPercentage(Nullable<float> val)
        {
            if (val == null)
            {
                text.gameObject.SetActive(false);
            }
            else
            {
                text.gameObject.SetActive(true);
                text.text = $"{val * 100f:F0}%";
            }
        }
        public void Hide()
        {
            text.gameObject.SetActive(false);
        }
    }
}
