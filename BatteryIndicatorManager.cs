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
        public static Sprite replacementSelector;

        void Awake()
        {
            GUIManager = GetComponent<uGUI_QuickSlots>();
            target = Inventory.main.quickSlots;
        }
        public void UpdateAll()
        {
            for (int i = 0; i < target.binding.Length; ++i)
            {
                if (target.binding[i] != null) UpdateSlot(i);
            }
        }
        private void AdjustSelector(int slotID)
        {
            if (replacementSelector == null) return;
            if (!enabled) return;
            InventoryItem item = target.binding[slotID];
            if (item != null)
            {
                EnergyMixin e = item.item.GetComponent<EnergyMixin>();
                if (e!= null && replacementSelector != null)
                {
                    GUIManager.selector.sprite = replacementSelector;
                } else
                {

                    GUIManager.selector.sprite = GUIManager.spriteSelected;
                }
            }
            else
            {

                GUIManager.selector.sprite = GUIManager.spriteSelected;
            }
        }

        public void UpdateSlot(int slotID)
        {
            if (!enabled) return;
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
            {
                UpdateSlot(target.activeSlot);
                AdjustSelector(target.activeSlot);
            }
        }
        void OnEnable()
        {
            UpdateAll();
        }
    }
}
