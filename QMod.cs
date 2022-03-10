using System.Reflection;
using HarmonyLib;
using QModManager.API.ModLoading;
using Logger = QModManager.Utility.Logger;
using UnityEngine;
using SMLHelper.V2.Utility;
using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
using System.Collections.Generic;
using Oculus.Newtonsoft.Json;
using System.IO;
using System;

namespace batteryMod
{
    public class Config : ConfigFile
    {
        public enum Style
        {
            Default,
            Text
        };
        public Style style = Style.Default;
        public struct ColorConfig
        {
            public float threshold;
            public Color color;
            public ColorConfig(float threshold, Color color) {
                this.threshold = threshold;
                this.color = color;
            }
        }
        public ColorConfig[] coloring =
        {
            new ColorConfig(0.15f, Color.red),
            new ColorConfig(0.30f, Color.yellow),
            new ColorConfig(1.00f, Color.green)
        };
        public bool useGradient = false;
    }
    public class ColorConverter : JsonConverter
    {
        public override bool CanConvert(System.Type objectType)
        {
            return objectType == typeof(Color);
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Color32 c = (Color32)(Color)value;

            writer.WriteValue($"#{c.r:X2}{c.g:X2}{c.b:X2}");
        }

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            string s = (string)reader.Value;
            s = s.TrimStart('#');
            int r = int.Parse(s.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int g = int.Parse(s.Substring(2, 4), System.Globalization.NumberStyles.HexNumber);
            int b = int.Parse(s.Substring(4, 6), System.Globalization.NumberStyles.HexNumber);
            return new Color(r / 255f, g / 255f, b / 255f);
        }

    }

    [QModCore]
    public static class QMod
    {
        public static Config config = new Config();
        [QModPatch]
        public static void Patch()
        {
            config.LoadWithConverters(true, new JsonConverter[] { new ColorConverter(), new Oculus.Newtonsoft.Json.Converters.KeyValuePairConverter()} );
            var assembly = Assembly.GetExecutingAssembly();
            var modName = ($"wegesmalec_{assembly.GetName().Name}");
            Logger.Log(Logger.Level.Info, $"Patching {modName}");
            Harmony harmony = new Harmony(modName);
            harmony.PatchAll(assembly);
            Logger.Log(Logger.Level.Info, "Patched successfully!");

            string assetPath = Path.Combine(Path.GetDirectoryName(assembly.Location), "assets");

            BatteryIndicatorDefault.fgSprite = Sprite.Create(ImageUtils.LoadTextureFromFile(Path.Combine(assetPath,"default_indicator.png")),
                new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f));
            BatteryIndicatorDefault.bgSprite = Sprite.Create(ImageUtils.LoadTextureFromFile(Path.Combine(assetPath,"default_indicator_bg.png")),
                new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f));
        }
    }
}