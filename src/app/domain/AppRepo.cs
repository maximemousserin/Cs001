namespace Cs001;

using System;

/// <summary>
///   Pure application game logic repository — shared between view-specific logic
///   blocks.
/// </summary>
public class AppRepo : IAppRepo {
  public event Action? SplashScreenSkipped;
  public event Action? MainMenuEntered;
  public event Action? GameEntered;
  public event Action<PostGameAction>? GameExited;

  private bool _disposedValue;

  public void SkipSplashScreen() => SplashScreenSkipped?.Invoke();

  public void OnMainMenuEntered() => MainMenuEntered?.Invoke();

  public void OnEnterGame() => GameEntered?.Invoke();
  public void OnExitGame(PostGameAction action) => GameExited?.Invoke(action);

  #region Internals

  protected void Dispose(bool disposing) {
    if (!_disposedValue) {
      if (disposing) {
        // Dispose managed objects.
        SplashScreenSkipped = null;
        MainMenuEntered = null;
        GameEntered = null;
        GameExited = null;
      }

      _disposedValue = true;
    }
  }

  public void Dispose() {
    Dispose(disposing: true);
    GC.SuppressFinalize(this);
  }

  #endregion Internals
}
