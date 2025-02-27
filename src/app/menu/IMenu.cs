namespace Cs001;

using Chickensoft.GodotNodeInterfaces;

public interface IMenu : IControl {
  public event Menu.NewGameEventHandler NewGame;
  public event Menu.LoadGameEventHandler LoadGame;
}
