namespace Cs001;

using Chickensoft.Introspection;

public partial class PlayerLogic {
  public partial record State {
    [Meta]
    public abstract partial record Alive : State, IGet<Input.Killed> {
      public Transition On(in Input.Killed input) => throw new System.NotImplementedException();
    }
  }
}
