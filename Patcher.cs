using HarmonyLib;
using System;
using UnityEngine;
using UnityEngine.UI;
using Logger = QModManager.Utility.Logger;
using System.Linq;

namespace batteryMod
{
    public class Patcher
    {
        [HarmonyPatch(typeof(uGUI_QuickSlots))]
        [HarmonyPatch(nameof(uGUI_QuickSlots.Init))]
        internal class PatchGuiInit
        {
            [HarmonyPostfix]
            public static void Postfix(uGUI_QuickSlots __instance, IQuickSlots newTarget)
            {
                if (__instance.gameObject.GetComponent<BatteryIndicatorManager>() == null)
                {
                    __instance.gameObject.AddComponent<BatteryIndicatorManager>();
                }
                __instance.gameObject.GetComponent<BatteryIndicatorManager>().enabled = (newTarget == Inventory.main.quickSlots);
            }
        }
        [HarmonyPatch(typeof(uGUI_QuickSlots))]
        [HarmonyPatch(nameof(uGUI_QuickSlots.OnBind))]
        internal class OnBindAction
        {
            [HarmonyPostfix]
            public static void Postfix(uGUI_QuickSlots __instance, int slotID)
            {
                __instance.GetComponent<BatteryIndicatorManager>().UpdateSlot(slotID);
            }
        }
        [HarmonyPatch(typeof(uGUI_ItemIcon))]
        [HarmonyPatch(nameof(uGUI_ItemIcon.Init))]
        internal class PatchItemIcon
        {
            [HarmonyPostfix]
            public static void Postfix(uGUI_ItemIcon __instance)
            {
                if (__instance.GetComponentInParent<uGUI_QuickSlots>() != null)
                {
                    switch (QMod.config.style) {
                        case Config.Style.Default:
                            __instance.gameObject.AddComponent<BatteryIndicatorDefault>();
                            break;
                        case Config.Style.Text:
                            __instance.gameObject.AddComponent<BatteryIndicatorText>();
                            break;
                        case Config.Style.BZ:
                            __instance.gameObject.AddComponent<BatteryIndicatorBZ>();
                            break;
                    }
                }
            }
        }
        [HarmonyPatch(typeof(uGUI_ItemSelector))]
        [HarmonyPatch(nameof(uGUI_ItemSelector.Awake))]
        internal class PatchItemSelector
        {
            public static void Postfix(uGUI_ItemSelector __instance)
            {
                __instance.gameObject.AddComponent<ItemSelectorManager>();
            }
        }
        [HarmonyPatch(typeof(uGUI_ItemSelector))]
        [HarmonyPatch(nameof(uGUI_ItemSelector.CreateIcons))]
        internal class PatchItemSelectorCreation
        {
            public static void Postfix(uGUI_ItemSelector __instance)
            {
                if (QMod.config.showOnBatterySelector)
                    __instance.gameObject.GetComponent<ItemSelectorManager>().RenderIndicator();
            }
        }
    }
}
