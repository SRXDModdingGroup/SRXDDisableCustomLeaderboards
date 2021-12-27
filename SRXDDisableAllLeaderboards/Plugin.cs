using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace SRXDDisableAllLeaderboards
{
    [BepInPlugin("org.srxdmoddinggroup.disableallleaderboards", "disableallleaderboards", "1.0.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo($"All Leaderboards are Disabled!");
            Harmony.CreateAndPatchAll(typeof(Patches));
        }
        private class Patches
        {
            [HarmonyPatch(typeof(TrackInfo), MethodType.Constructor)]
            [HarmonyPatch(typeof(TrackInfoMetadata), MethodType.Constructor)]
            [HarmonyPostfix]
            private static void patch(ref TrackInfo __instance)
            {
                __instance.allowCustomLeaderboardCreation = false;
            }

            [HarmonyPatch(typeof(LeaderboardSubmissionHelper), nameof(LeaderboardSubmissionHelper.SubmitLeaderboard))]
            [HarmonyPrefix]
            private static bool patch1()
            {
                return false;
            }
        }
    }
}