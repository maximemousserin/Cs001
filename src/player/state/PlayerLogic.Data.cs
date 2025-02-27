namespace Cs001;

using Chickensoft.Introspection;

public partial class PlayerLogic {
  /// <summary>Data shared between states.</summary>
  [Meta, Id("player_logic_data")]
  public partial record Data {
  }
}
