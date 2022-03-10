using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace batteryMod
{
    internal class BatteryIndicatorManager : MonoBehaviour
    {
        uGUI_QuickSlots GUIManager;
        QuickSlots target;
        void Awake()
        {
            GUIManager = GetComponent<uGUI_QuickSlots>();
            target = GUIManager.target as QuickSlots;
        }

        public void UpdateSlot(int slotID)
        {
            InventoryItem item = target.binding[slotID];
            IBatteryIndicator batteryIndicator = GUIManager.icons[slotID].GetComponent<IBatteryIndicator>();
            if (item == null)
            {
                batteryIndicator.Hide();
            }
            else
            {
                EnergyMixin e = item.item.GetComponent<EnergyMixin>();
                if (e == null) batteryIndicator.SetPercentage(null);
                else
                {
                    if (e.capacity == 0) e.RestoreBattery(); // a little nudge
                    batteryIndicator.SetPercentage(e.charge / e.capacity);
                }
            }
        }
        void Update()
        {
            if (target.activeSlot != -1)
                UpdateSlot(target.activeSlot);
        }
    }
}
