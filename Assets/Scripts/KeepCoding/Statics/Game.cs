using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace KModkit
{
    /// <summary>
    /// Allows access into the game's internal code. Written by Emik.
    /// </summary>
    /// <remarks>
    /// You should avoid calling this class from the Editor as it uses the game assembly as a dependancy.
    /// </remarks>
    public static class Game
    {
        /// <summary>
        /// Determines how the mod is stored.
        /// </summary>
        public enum ModSourceEnum
        {
            /// <value>
            /// The mod is invalid.
            /// </value>
            Invalid,

            /// <value>
            /// The mod is stored within the local mods folder.
            /// </value>
            Local,

            /// <value>
            /// The mod is stored within the workshop folder.
            /// </value>
            SteamWorkshop
        }

        /// <summary>
        /// Allows access to methods relating mod paths.
        /// </summary>
        public static class ModManager
        {
            private static readonly object Manager;
            private static readonly Type ManagerType;
            private static Dictionary<string, MethodInfo> MethodCache = new Dictionary<string, MethodInfo>();
            
            static ModManager()
            {
                if (Application.isEditor)
                    return;
                Type ModManagerType = ReflectionHelper.FindGameType("ModManager");
                if (ModManagerType == null)
                    return;
                Manager = ModManagerType.GetField("Instance", ReflectionHelper.AllFlags).GetValue(null);
                ManagerType = ModManagerType;
            }

            private static List<string> InvokeMethod(string MethodName, int? arg)
            {
                if (Manager == null)
                    return null;
                object[] args = arg == null ? new object[] { } : new object[] {(int) arg};
                if (MethodCache.ContainsKey(MethodName))
                    return (List<string>) MethodCache[MethodName].Invoke(Manager, args);
                MethodInfo method = ManagerType.GetMethod(MethodName, ReflectionHelper.AllFlags);
                if (method == null)
                    return null;
                MethodCache.Add(MethodName, method);
                return (List<string>)method.Invoke(Manager, args);
            }

            /// <value>
            /// Gets all of the disabled mod paths.
            /// </value>
            public static List<string> GetDisabledModPaths()
            {
                return InvokeMethod("GetDisabledModPaths", null);
            }

            /// <value>
            /// Gets all of the mod paths within the <see cref="ModSourceEnum"/> constraint.
            /// </value>
            public static List<string> GetAllModPathsFromSource(ModSourceEnum source)
            {
                return InvokeMethod("GetAllModPathsFromSource", (int) source);
            }

            /// <value>
            /// Gets all of the enabled mod paths within the <see cref="ModSourceEnum"/> constraint.
            /// </value>
            public static List<string> GetEnabledModPaths(ModSourceEnum source)
            {
                return InvokeMethod("GetEnabledModPaths", (int) source);
            }
        }

        /// <summary>
        /// Allows access into the player settings from the game. Do not use this class in the unity editor. Written by Emik.
        /// </summary>
        public static class PlayerSettings
        {
            private static readonly object SettingsObject;
            private static readonly Type SettingsType;
            private static Dictionary<string, FieldInfo> SettingsCache = new Dictionary<string, FieldInfo>();

            static PlayerSettings()
            {
                if (Application.isEditor) return;
                Type SettingsManager = ReflectionHelper.FindGameType("PlayerSettingsManager");
                if (SettingsManager == null) return;
                object Instance = SettingsManager.GetProperty("Instance", ReflectionHelper.AllFlags).GetValue(null, null);
                object settings = SettingsManager.GetProperty("PlayerSettings", ReflectionHelper.AllFlags)
                    .GetValue(Instance, null);
                SettingsObject = settings;
                SettingsType = settings.GetType();
            }

            private static object GetSetting(string FieldName)
            {
                if (SettingsObject == null)
                    return null;
                if (SettingsCache.ContainsKey(FieldName))
                    return SettingsCache[FieldName].GetValue(SettingsObject);
                FieldInfo field = SettingsType.GetField(FieldName, ReflectionHelper.AllFlags);
                if (field == null)
                    return null;
                SettingsCache.Add(FieldName, field);
                return field.GetValue(SettingsObject);
            }

            private static T? GetSetting<T>(string FieldName) where T: struct
            {
                return (T) GetSetting(FieldName);
            }

            /// <value>
            /// Determines if vertical tilting is flipped or not.
            /// </value>
            public static bool? InvertTiltControls
            {
                get
                {
                    return GetSetting<bool>("InvertTiltControls");
                }
            }

            /// <value>
            /// Determines if the option to lock the mouse to the window is enabled.
            /// </value>
            public static bool? LockMouseToWindow
            {
                get
                {
                    return GetSetting<bool>("LockMouseToWindow");
                }
            }

            /// <value>
            /// Determines if the option to show the leaderboards from the pamphlet.
            /// </value>
            public static bool? ShowLeaderBoards
            {
                get
                {
                    return GetSetting<bool>("ShowLeaderBoards");
                }
            }

            /// <value>
            /// Determines if the option to show the rotation of the User Interface is enabled.
            /// </value>
            public static bool? ShowRotationUI
            {
                get
                {
                    return GetSetting<bool>("ShowRotationUI");
                }
            }

            /// <value>
            /// Determines if the option to show scanlines is enabled.
            /// </value>
            public static bool? ShowScanline
            {
                get
                {
                    return GetSetting<bool>("ShowScanline");
                }
            }

            /// <value>
            /// Determines if the option to skip the title screen is enabled.
            /// </value>
            public static bool? SkipTitleScreen
            {
                get
                {
                    return GetSetting<bool>("SkipTitleScreen");
                }
            }

            /// <value>
            /// Determines if the VR or regular controllers vibrate.
            /// </value>
            public static bool? RumbleEnabled
            {
                get
                {
                    return GetSetting<bool>("RumbleEnabled");
                }
            }

            /// <value>
            /// Determines if the touchpad controls are inverted.
            /// </value>
            public static bool? TouchpadInvert
            {
                get
                {
                    return GetSetting<bool>("TouchpadInvert");
                }
            }

            /// <value>
            /// Determines if the option to always use mods is enabled.
            /// </value>
            public static bool? UseModsAlways
            {
                get
                {
                    return GetSetting<bool>("UserModsAlways");
                }
            }

            /// <value>
            /// Determines if the option to use parallel/simultaneous mod loading is enabled.
            /// </value>
            public static bool? UseParallelModLoading
            {
                get
                {
                    return GetSetting<bool>("UseParallelModLoading");
                }
            }

            /// <value>
            /// Determines if VR mode is requested.
            /// </value>
            public static bool? VRModeRequested
            {
                get
                {
                    return GetSetting<bool>("VRModeRequested");
                }
            }

            /// <value>
            /// The intensity of anti-aliasing currently on the game. Ranges 0 to 8.
            /// </value>
            public static int? AntiAliasing
            {
                get
                {
                    return GetSetting<int>("AntiAliasing");
                }
            }

            /// <value>
            /// The current music volume from the dossier menu. Ranges 0 to 100.
            /// </value>
            public static int? MusicVolume
            {
                get
                {
                    return GetSetting<int>("MusicVolume");
                }
            }

            /// <value>
            /// The current sound effects volume from the dosssier menu. Ranges 0 to 100.
            /// </value>
            public static int? SFXVolume
            {
                get
                {
                    return GetSetting<int>("SFXVolume");
                }
            }

            /// <value>
            /// Determines if VSync is on or off.
            /// </value>
            public static int? VSync
            {
                get
                {
                    return GetSetting<int>("VSync");
                }
            }

            /// <value>
            /// The current language code.
            /// </value>
            public static string LanguageCode
            {
                get
                {
                    return (string) GetSetting("LanguageCode");
                }
            }
        }
    }
}
