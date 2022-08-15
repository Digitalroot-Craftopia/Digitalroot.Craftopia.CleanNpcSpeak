using BepInEx;
using BepInEx.Configuration;
using Digitalroot.Valheim.Common;
using HarmonyLib;
using JetBrains.Annotations;
using System;
using System.IO;
using System.Reflection;

namespace Digitalroot.Craftopia.CleanNpcSpeech
{
  [BepInPlugin(Guid, Name, Version)]
  public partial class Main : BaseUnityPlugin, ITraceableLogging
  {
    private Harmony _harmony;

    [UsedImplicitly]
    public static ConfigEntry<int> NexusId;

    public static Main Instance;
    private const string CacheName = "chainloader";

    #region Implementation of ITraceableLogging

    /// <inheritdoc />
    public string Source => Namespace;

    /// <inheritdoc />
    public bool EnableTrace { get; }

    #endregion

    public Main()
    {
      try
      {
        #if DEBUG
        EnableTrace = true;
        #else
        EnableTrace = false;
        #endif
        Instance = this;
        NexusId = Config.Bind("General", "NexusID", 116, new ConfigDescription("Nexus mod ID for updates", null, new ConfigurationManagerAttributes { Browsable = false, ReadOnly = true }));
        Log.RegisterSource(Instance);
        Log.Trace(Instance, $"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod()?.Name}()");
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    [UsedImplicitly]
    private void Awake()
    {
      try
      {
        Log.Trace(Instance, $"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod()?.Name}()");
        _harmony = Harmony.CreateAndPatchAll(typeof(Main).Assembly, Guid);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    [UsedImplicitly]
    private void OnDestroy()
    {
      try
      {
        Log.Trace(Instance, $"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod()?.Name}()");
        _harmony?.UnpatchSelf();
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private DirectoryInfo AssemblyDirectory
    {
      get
      {
        string codeBase = Assembly.GetExecutingAssembly().CodeBase;
        UriBuilder uri = new(codeBase);
        var fileInfo = new FileInfo(Uri.UnescapeDataString(uri.Path));
        return fileInfo.Directory;
      }
    }

    public string OnSoNpc_GetIdleMessage(ref string s)
    {
      return s.Replace("bitch", "****")
              .Replace("Bitch", "****");
    }
  }
}
