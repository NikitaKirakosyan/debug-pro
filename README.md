# Debug Pro
![Unity](https://img.shields.io/badge/unity-6000.0%2B-blue.svg)
![License](https://img.shields.io/badge/license-MIT-green.svg)

A utility that improves the flow for working with debugging in Unity.

## Installation options
1) Copy the Assets/debug-pro folder into your project.
2) Download and import `.unitypackage` from [release](https://github.com/NikitaKirakosyan/debug-pro/releases/latest) into your project.

## ✨ Features
* Prefixes - in the form of a caller, custom or default DebugPro _**(adjustable by settings)**_
* Caller member name - automatically set log call source by method name
* Editor only - can be disabled for release or production builds _**(adjustable by code)**_
* Colorized - only the prefix or the entire text can be colored _**(adjustable by settings)**_
* Color theme - automatically calculates text color and caches it for better performance. But you can also specify the color manually
* Settings window - allows you to specify the default prefix, color saturation, color value, and full colorized option. _**(Top menu -> Window/Debug Pro Settings)**_
* Thread-safe code - uses smart color caching to avoid multithreading issues

## 🖼️ Showcase
### _Built-in logs & DebugPro logs_
![image](https://github.com/user-attachments/assets/e2fa06f0-e09f-470b-92ef-566fdfda4a5d)

### _Full colorized texts_
![image](https://github.com/user-attachments/assets/42182ef5-394e-40ae-a948-cf722a218181)

### _DebugPro settings_
![image](https://github.com/user-attachments/assets/e76b134e-4234-4f29-9629-413ec961c02f)

### _Examples of use_
![image](https://github.com/user-attachments/assets/42222864-2bee-49e5-9571-e3d12bbba2c7)

## 🧪 Demos
### See the Demo folder for working examples:
* Assets/debug-pro/Examples/Scripts
* Assets/debug-pro/Examples/Scenes/Demo.unity

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
