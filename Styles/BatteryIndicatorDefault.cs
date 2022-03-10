using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;


namespace batteryMod
{

    internal class BatteryIndicatorDefault : MonoBehaviour, IBatteryIndicator
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
            container.transform.localEulerAngles = new Vector3(0f, 0f, 202.5f);
            container.layer = this.gameObject.layer;
            container.transform.localPosition = new Vector3(0.55f, -0.55f);

            GameObject bg_object = new GameObject("BatteryIndicatorBackground");
            bg_object.transform.SetParent(container.transform, false);
            bg_object.layer = this.gameObject.layer;
            bg = bg_object.AddComponent<Image>();
            bg.sprite = bgSprite;
            bg.rectTransform.offsetMin = new Vector2(-32, -32);
            bg.rectTransform.offsetMax = new Vector2(32, 32);

            GameObject fg_object = new GameObject("BatteryIndicatorForeground");
            fg_object.transform.SetParent(container.transform, false);
            fg_object.layer = this.gameObject.layer;
            fg = fg_object.AddComponent<Image>();
            fg.sprite = fgSprite;
            fg.rectTransform.offsetMin = new Vector2(-32, -32);
            fg.rectTransform.offsetMax = new Vector2(32, 32);

            fg.type = Image.Type.Filled;
            fg.fillMethod = Image.FillMethod.Radial360;
            fg.fillClockwise = true;
            fg.fillOrigin = (int)Image.Origin360.Top;

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
                fg.fillAmount = (float)val * (7f / 8f) + 1f / 8f;
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
