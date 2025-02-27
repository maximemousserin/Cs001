namespace Cs001;

using System;

/// <summary>
///   Pure application game logic repository shared between view-specific logic
///   blocks.
/// </summary>
public interface IAppRepo : IDisposable {
  /// <summary>
  ///   Event invoked when the game is about to start.
  /// </summary>
  public event Action? GameEntered;

  /// <summary>
  ///   Event invoked when the game is about to end.
  /// </summary>
  public event Action<PostGameAction>? GameExited;

  /// <summary>Event invoked when the splash screen is skipped.</summary>
  public event Action? SplashScreenSkipped;

  /// <summary>Event invoked when the main menu is entered.</summary>
  public event Action? MainMenuEntered;

  /// <summary>Inform the app that the game should be shown.</summary>
  public void OnEnterGame();

  /// <summary>Inform the app that the game should be exited.</summary>
  /// <param name="action">Action to take following the end of the game.</param>
  public void OnExitGame(PostGameAction action);

  /// <summary>Tells the app that the main menu was entered.</summary>
  public void OnMainMenuEntered();

  /// <summary>Skips the splash screen.</summary>
  public void SkipSplashScreen();
}
