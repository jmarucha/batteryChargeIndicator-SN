using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

using Logger = QModManager.Utility.Logger;

namespace batteryMod
{

    internal class BatteryIndicatorBZ : MonoBehaviour, IBatteryIndicator
    {
        GameObject container;
        Image bg;
        Image fg;

        public static Sprite fgSprite;
        public static Sprite bgSprite;
        void Awake()
        {
            container = new GameObject("BatteryIndicator");
            container.transform.SetParent(this.transform, false);
            container.layer = this.gameObject.layer;
            container.transform.localPosition = Vector3.zero;
            container.transform.localEulerAngles = new Vector3(0,0,202.5f);

            GameObject bg_object = new GameObject("BatteryIndicatorBackground");
            bg_object.transform.SetParent(container.transform, false);
            bg_object.layer = this.gameObject.layer;
            bg = bg_object.AddComponent<Image>();
            bg.sprite = bgSprite;
            bg.rectTransform.offsetMin = new Vector2(-32f, -33f);
            bg.rectTransform.offsetMax = new Vector2(5f, 33f);

            GameObject fg_object = new GameObject("BatteryIndicatorForeground");
            fg_object.transform.SetParent(container.transform, false);
            fg_object.layer = this.gameObject.layer;
            fg = fg_object.AddComponent<Image>();
            fg.sprite = fgSprite;
            fg.rectTransform.offsetMin = new Vector2(-32f, -33f);
            fg.rectTransform.offsetMax = new Vector2(5f, 33f);

            fg.type = Image.Type.Filled;
            fg.fillMethod = Image.FillMethod.Radial180;
            fg.fillClockwise = false;
            fg.fillOrigin = (int)Image.Origin180.Right;

            container.SetActive(false);
        }

        public void SetPercentage(float? val)
        {
            if (val == null)
            {
                container.SetActive(false);
            }
            else
            {
                fg.fillAmount = (float)val*133.0f/180f;
                fg.color = ColorHelper.GetColor((float)val);
                container.SetActive(true);

            }
        }
        public void Hide()
        {
            container.SetActive(false);
        }
    }
}
