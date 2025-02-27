namespace Cs001;

using System;
using Chickensoft.Collections;

/// <summary>
///   Game repository — stores pure game logic that's not directly related to the
///   game node's overall view.
/// </summary>
public class GameRepo : IGameRepo {
  public IAutoProp<bool> IsMouseCaptured => _isMouseCaptured;
  private readonly AutoProp<bool> _isMouseCaptured;
  public IAutoProp<bool> IsPaused => _isPaused;
  private readonly AutoProp<bool> _isPaused;
  public event Action<GameOverReason>? Ended;
  private bool _disposedValue;

  public GameRepo() {
    _isMouseCaptured = new AutoProp<bool>(false);
    _isPaused = new AutoProp<bool>(false);
  }

  internal GameRepo(
    AutoProp<bool> isMouseCaptured,
    AutoProp<bool> isPaused
  ) {
    _isMouseCaptured = isMouseCaptured;
    _isPaused = isPaused;
  }

  public void SetIsMouseCaptured(bool isMouseCaptured) =>
    _isMouseCaptured.OnNext(isMouseCaptured);

  public void OnGameEnded(GameOverReason reason) {
    _isMouseCaptured.OnNext(false);
    Pause();
    Ended?.Invoke(reason);
  }

  public void Pause() {
    _isMouseCaptured.OnNext(false);
    _isPaused.OnNext(true);
  }

  public void Resume() {
    _isMouseCaptured.OnNext(true);
    _isPaused.OnNext(false);
  }

  #region Internals

  protected void Dispose(bool disposing) {
    if (!_disposedValue) {
      if (disposing) {
        // Dispose managed objects.
        _isMouseCaptured.OnCompleted();
        _isMouseCaptured.Dispose();
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
