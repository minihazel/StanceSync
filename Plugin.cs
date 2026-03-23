using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using hazelify.StanceSync.Patches;
using UnityEngine;

namespace hazelify.StanceSync
{
    [BepInPlugin("hazelify.StanceSync", "StanceSync", "1.0.0")]
    [BepInIncompatibility("com.janky.hollywoodcam")]
    // good to know, if you have StanceSync installed, HollywoodCam will not work

    public class Plugin : BaseUnityPlugin
    {
        // big ups to the legend Janky for this one, couldn't have done it without him <3
        public static ManualLogSource LogSource;
        public static ConfigEntry<bool> enableSync;
        public static ConfigEntry<bool> enableResetSync;

        private void Awake()
        {
            LogSource = Logger;
            initConfig();

            // enable patches, without whom this mod is useless
            new PlayerCameraControllerPatchPrefix().Enable();
            new OnLeanPatchPostfix().Enable();

            // log that the mod has loaded
            LogSource.LogInfo($"StanceSync loaded!");
        }

        private void initConfig()
        {
            // bool config binds for the F12 BepInEx menu

            // core mod feature, enabling lean swap sync [toggle]
            enableSync = Config.Bind(
                "Configuration",
                "Enable syncing",
                true,
                "If leaning left, should the character automatically shoulder swap?"
            );

            // reset lean sync for when you stop leaning [toggle]
            enableResetSync = Config.Bind(
                "Configuration",
                "Enable reset sync",
                true,
                "If no longer leaning left, should the character automatically reset the shoulder swap?"
            );
        }
    }
}
