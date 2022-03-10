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
        internal class PatchCloseAction
        {
            [HarmonyPostfix]
            public static void Postfix(uGUI_QuickSlots __instance)
            {
                __instance.gameObject.AddComponent<BatteryIndicatorManager>();
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
                    }
                }
            }
        }
    }
}
