# Configuration

**Key:** `style`
**Values:** `Default`, `Text`
**Description:** Choose from the large variety (two) of vibrant styles for battery display. `Default` is an annulous around icon, `Text` just shows percentage above (where second one was made with debugging in mind, no idea why I still keep it).

**Key:** `coloring`
**Values:** `object[{"threshold": float, "color": color}]`
**Description:** Colors used on indicator. If `useGradient` is false, the values act as threshold to change color (i.e. having `{"threshold": 0.20, "color": #FF0000}` will make battery go red if the charge is under 20%). If `useGradient` is true, these are gradient keys. Make sure to keep a value for `"threshold": 1.0`, otherwise funny things happen. Colors are given in HTML format.

**Key:** `useGradient`
**Values:** `bool`
**Description:** To have smooth transition from high-charge to low-charge color, or to change colors below some threshold. Choose wisely.