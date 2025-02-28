namespace Cs001;

using Chickensoft.AutoInject;
using Chickensoft.GodotNodeInterfaces;
using Chickensoft.Introspection;
using Godot;

[Meta(typeof(IAutoNode))]
public partial class Menu : Control, IMenu {
  public override void _Notification(int what) => this.Notify(what);

  #region Nodes
  [Node]
  public IButton NewGameButton { get; set; } = default!;
  #endregion Nodes

  #region Signals
  [Signal]
  public delegate void NewGameEventHandler();
  #endregion Signals

  public void OnReady() {
    NewGameButton.Pressed += OnNewGamePressed;
  }

  public void OnExitTree() {
    NewGameButton.Pressed -= OnNewGamePressed;
  }

  public void OnNewGamePressed() => EmitSignal(SignalName.NewGame);
}
