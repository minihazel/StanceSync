using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using hazelify.StanceSync.Patches;
using UnityEngine;

namespace hazelify.StanceSync
{
    [BepInPlugin( /* internal name: */"hazelify.StanceSync", /* F12 name: */ "hazelify.StanceSync", "1.0.1")]
    [BepInIncompatibility("com.janky.hollywoodcam")]
    // good to know, if you have StanceSync installed, HollywoodCam will not work

    public class Plugin : BaseUnityPlugin
    {
        // big ups to the legend Janky for this one, couldn't have done it without him <3
        public static ManualLogSource LogSource;
        public static ConfigEntry<bool> enableSync;
        public static ConfigEntry<bool> enableResetSync;
        public static ConfigEntry<bool> enableSyncWithOptic;

        private void Awake()
        {
            LogSource = Logger;
            initConfig();

            // enable patches, without whom this mod is useless
            new PlayerCameraControllerPatchPrefix().Enable();
            new OnLeanPatchPostfix().Enable();

            // log that the mod has loaded
            LogSource.LogInfo($"hazelify.StanceSync loaded!");
        }

        private void initConfig()
        {
            // bool config binds for the F12 BepInEx menu

            // core mod feature, enabling lean swap sync [toggle]
            enableSync = Config.Bind(
                "Core",
                "Sync leaning with shoulder swapping?",
                true,
                new ConfigDescription("If leaning left, should the character automatically shoulder swap?",
                null,
                new ConfigurationManagerAttributes { Order = 3 }));

            // whether to sync leaning when a magnified optic is equipped
            enableSyncWithOptic = Config.Bind(
                "Core",
                "Sync leaning while optic is equipped?",
                true,
                new ConfigDescription("If a magnified optic is equipped, should leaning be synchronized at all?",
                null,
                new ConfigurationManagerAttributes { Order = 2 }));

            // reset lean sync for when you stop leaning [toggle]
            enableResetSync = Config.Bind(
                "Core",
                "Sync leaning reset?",
                true,
                new ConfigDescription("If no longer leaning left, should the character automatically reset the shoulder swap?",
                null,
                new ConfigurationManagerAttributes { Order = 1 }));
        }
    }
}
