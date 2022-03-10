using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace batteryMod
{
    internal class ColorHelper
    {
        static Gradient gradient;
        public static Color GetColor(float val)
        {
            if (QMod.config.useGradient)
            {
                return GetColorFromGradient(val);
            } else
            {
                return GetSolidColor(val);
            }
        }
        public static Color GetSolidColor(float val)
        {
            return (from c in QMod.config.coloring
                    where c.threshold >= val
                    orderby c.threshold ascending
                    select c.color).FirstOrDefault();
        }
        public static Color GetColorFromGradient(float val)
        {
            if (gradient == null)
            {
                SetUpGradient();
            }
            return gradient.Evaluate(val);
        }
        public static void SetUpGradient()
        {
            gradient = new Gradient();
            gradient.SetKeys(
                (from keys in QMod.config.coloring select new GradientColorKey(keys.color, keys.threshold)).ToArray(),
                (from keys in QMod.config.coloring select new GradientAlphaKey(1.0f, keys.threshold)).ToArray()
                );
        }
    }
}
