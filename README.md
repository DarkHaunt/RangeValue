# RangeValue Unity
Data type that can easy handle range in Unity inspector

![Demo Gif](https://github.com/DarkHaunt/RangeValue/assets/91742392/22c28040-210a-4b8a-b5f0-ce36af453e07)

# Installation
1. **In Unity select menu Window -> Package Manager;**
2. **Select "Add package from git URL";**
3. **Insert https://github.com/DarkHaunt/RangeValue.git#package and wait for end;**
4. **You can use Demo scene to quick try it by yourself;**

# Usage
This type defined in UnityRangeValue namespace:

```CS
using UnityRangeValue;

// ---

[SerializeField] private RangeValue _range;

// ---

_range.Rand();
```
