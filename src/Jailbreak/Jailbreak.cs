using System.Collections.Immutable;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Capabilities;
using Jailbreak.Public;
using Jailbreak.Public.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Utils;

namespace Jailbreak;

/// <summary>
///   The classic Jail gamemode, ported to Counter-Strike 2.
/// </summary>
public class Jailbreak : BasePlugin {
  private readonly IServiceProvider provider;
  private IReadOnlyList<IPluginBehavior>? extensions;
  private IServiceScope? scope;

  /// <summary>
  ///   The Jailbreak plugin.
  /// </summary>
  /// <param name="provider"></param>
  public Jailbreak(IServiceProvider provider) { this.provider = provider; }

  /// <inheritdoc />
  public override string ModuleName => "Jailbreak";

  /// <inheritdoc />
  public override string ModuleVersion
    => $"{GitVersionInformation.SemVer} ({GitVersionInformation.ShortSha})";

  /// <inheritdoc />
  public override string ModuleAuthor => "EdgeGamers Development";

  /// <inheritdoc />
  public override void Load(bool hotReload) {
    RegisterListener<Listeners.OnServerPrecacheResources>(manifest => {
      manifest.AddResource("particles/explosions_fx/explosion_c4_500.vpcf");
      manifest.AddResource("soundevents/soundevents_jb.vsndevts");
      manifest.AddResource("sounds/explosion.vsnd");
      manifest.AddResource("sounds/jihad.vsnd");
      manifest.AddResource(
        "models/props/de_dust/hr_dust/dust_soccerball/dust_soccer_ball001.vmdl");
    });

    //  Load Managers
    Logger.LogInformation("[Jailbreak] Loading...");

    scope = provider.CreateScope();
    extensions = scope.ServiceProvider.GetServices<IPluginBehavior>()
      .ToImmutableList();

    Logger.LogInformation("[Jailbreak] Found {@BehaviorCount} behaviors.",
      extensions.Count);

    foreach (var extension in extensions) {
      //  Register all event handlers on the extension object
      RegisterAllAttributes(extension);

      //  Tell the extension to start it's magic
      extension.Start(this, hotReload);

      Logger.LogInformation("[Jailbreak] Loaded behavior {@Behavior}",
        extension.GetType().FullName);
    }

    //  Expose the scope to other plugins
    Capabilities.RegisterPluginCapability(API.Provider, () => {
      if (scope == null)
        throw new InvalidOperationException(
          "Jailbreak does not have a running scope! Is the jailbreak plugin loaded?");

      return scope.ServiceProvider;
    });

    base.Load(hotReload);
  }

  /// <inheritdoc />
  public override void Unload(bool hotReload) {
    Logger.LogInformation("[Jailbreak] Shutting down...");

    if (extensions != null)
      foreach (var extension in extensions)
        extension.Dispose();

    //  Dispose of original extensions scope
    //  When loading again we will get a new scope to avoid leaking state.
    scope?.Dispose();
    scope = null;

    base.Unload(hotReload);
  }

  [GameEventHandler]
  public HookResult OnPlayerSpawn(EventPlayerSpawn @event, GameEventInfo info)
  {
      var player = @event.Userid;

      if (player == null || !player.IsValid || player.IsBot)
      {
          return HookResult.Continue;
      }

      AddTimer(0.1f, () =>
      {
          if (player.IsValid && player.PlayerPawn.IsValid)
          {
              player.RemoveWeapons();
              player.GiveNamedItem("weapon_knife");

              var pawn = player.PlayerPawn.Value;
              if (pawn != null)
              {
                  pawn.ArmorValue = 0;
              }
          }
      });

      return HookResult.Continue;
  }
}