namespace Cs001;

using System;
using Chickensoft.Collections;

public interface IGameRepo : IDisposable {
  /// <summary>Event invoked when the game ends.</summary>
  public event Action<GameOverReason>? Ended;

  /// <summary>Mouse captured status.</summary>
  public IAutoProp<bool> IsMouseCaptured { get; }

  /// <summary>Pause status.</summary>
  public IAutoProp<bool> IsPaused { get; }

  /// <summary>Inform the game that the game ended.</summary>
  /// <param name="reason">Game over reason.</param>
  public void OnGameEnded(GameOverReason reason);

  /// <summary>Pauses the game and releases the mouse.</summary>
  public void Pause();

  /// <summary>Resumes the game and recaptures the mouse.</summary>
  public void Resume();

  /// <summary>Changes whether the mouse is captured or not.</summary>
  /// <param name="isMouseCaptured">
  ///   Whether or not the mouse is captured.
  /// </param>
  public void SetIsMouseCaptured(bool isMouseCaptured);
}
