using Digitalroot.Valheim.Common;
using HarmonyLib;
using JetBrains.Annotations;
using System;
using System.Reflection;

namespace Digitalroot.Craftopia.CleanNpcSpeech
{
  [UsedImplicitly]
  public class Patch
  {
    [HarmonyPatch(typeof(Oc.SoNpc), nameof(Oc.SoNpc.GetIdleMessage))]
    // ReSharper disable once InconsistentNaming
    public class PatchSoNpc_GetIdleMessage
    {
      [UsedImplicitly]
      [HarmonyPostfix]
      [HarmonyPriority(Priority.Normal)]
      // ReSharper disable once InconsistentNaming
      public static void Postfix(ref string __result)
      {
        try
        {
          Log.Trace(Main.Instance, $"{Main.Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");

          __result = Main.Instance.OnSoNpc_GetIdleMessage(ref __result);
        }
        catch (Exception e)
        {
          Log.Error(Main.Instance, e);
        }
      }
    }
  }
}
