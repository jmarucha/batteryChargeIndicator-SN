using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace batteryMod
{
    internal class ItemSelectorManager :MonoBehaviour
    {
        uGUI_ItemSelector itemSelector;
        void Awake()
        {
            itemSelector = GetComponent<uGUI_ItemSelector>();
        }
        public void RenderIndicator()
        {
            for (int i = 0; i < itemSelector.items.Count; ++i)
            {
                Battery batteryComponent = itemSelector.items[i].item.GetComponent<Battery>();
                if (batteryComponent != null)
                {
                    uGUI_ItemIcon icon = itemSelector.icons[i + 1];
                    IBatteryIndicator batteryIndicator = null;
                    switch (QMod.config.style)
                    {
                        case Config.Style.Default:
                            batteryIndicator = icon.gameObject.AddComponent<BatteryIndicatorDefault>();
                            break;
                        case Config.Style.Text:
                            batteryIndicator = icon.gameObject.AddComponent<BatteryIndicatorText>();
                            break;
                        case Config.Style.BZ:
                            batteryIndicator = icon.gameObject.AddComponent<BatteryIndicatorBZ>();
                            break;
                    }
                    batteryIndicator.SetPercentage(batteryComponent.charge / batteryComponent.capacity);
                }
            }
        }
    }
}
