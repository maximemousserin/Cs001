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
  [Node]
  public IButton LoadGameButton { get; set; } = default!;
  #endregion Nodes

  #region Signals
  [Signal]
  public delegate void NewGameEventHandler();
  [Signal]
  public delegate void LoadGameEventHandler();
  #endregion Signals

  public void OnReady() {
    NewGameButton.Pressed += OnNewGamePressed;
    LoadGameButton.Pressed += OnLoadGamePressed;
  }

  public void OnExitTree() {
    NewGameButton.Pressed -= OnNewGamePressed;
    LoadGameButton.Pressed -= OnLoadGamePressed;
  }

  public void OnNewGamePressed() => EmitSignal(SignalName.NewGame);
  public void OnLoadGamePressed() => EmitSignal(SignalName.LoadGame);
}
