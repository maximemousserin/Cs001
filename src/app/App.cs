namespace Cs001;

using Chickensoft.AutoInject;
using Chickensoft.Introspection;
using Godot;

[Meta(typeof(IAutoNode))]
public partial class App : CanvasLayer, IApp {
  public override void _Notification(int what) => this.Notify(what);

  #region Constants

  public const string GAME_SCENE_PATH = "res://src/game/Game.tscn";

  #endregion Constants

  #region External

  public IGame Game { get; set; } = default!;
  public IInstantiator Instantiator { get; set; } = default!;

  #endregion External

  #region Provisions

  IAppRepo IProvide<IAppRepo>.Value() => AppRepo;

  #endregion Provisions

  #region State

  public IAppRepo AppRepo { get; set; } = default!;
  public IAppLogic AppLogic { get; set; } = default!;

  public AppLogic.IBinding AppBinding { get; set; } = default!;

  #endregion State

  #region Nodes

  [Node] public IMenu Menu { get; set; } = default!;
  [Node] public IColorRect BlankScreen { get; set; } = default!;
  [Node] public IAnimationPlayer AnimationPlayer { get; set; } = default!;
  [Node] public ISplash Splash { get; set; } = default!;

  #endregion Nodes

  public void Initialize() {
    Instantiator = new Instantiator(GetTree());
    AppRepo = new AppRepo();
    AppLogic = new AppLogic();
    AppLogic.Set(AppRepo);
    AppLogic.Set(new AppLogic.Data());

    // Listen for various menu signals. Each of these just translate to an input
    // for the overall app's state machine.
    Menu.NewGame += OnNewGame;
    Menu.LoadGame += OnLoadGame;

    AnimationPlayer.AnimationFinished += OnAnimationFinished;

    this.Provide();
  }

  public void OnReady() {
    AppBinding = AppLogic.Bind();

    AppBinding
      .Handle((in AppLogic.Output.ShowSplashScreen _) => {
        HideMenus();
        BlankScreen.Hide();
        Splash.Show();
      })
      .Handle((in AppLogic.Output.HideSplashScreen _) => {
        BlankScreen.Show();
        FadeToBlack();
      })
      .Handle((in AppLogic.Output.RemoveExistingGame _) => {
        Game.QueueFree();
        Game = default!;
      })
      .Handle((in AppLogic.Output.SetupGameScene _) => {
        Game = Instantiator.LoadAndInstantiate<Game>(GAME_SCENE_PATH);

        Instantiator.SceneTree.Paused = false;
      })
      .Handle((in AppLogic.Output.ShowMainMenu _) => {
        // Load everything while we're showing a black screen, then fade in.
        HideMenus();
        Menu.Show();
        Game.Show();

        FadeInFromBlack();
      })
      .Handle((in AppLogic.Output.FadeToBlack _) => FadeToBlack())
      .Handle((in AppLogic.Output.ShowGame _) => {
        HideMenus();
        FadeInFromBlack();
      })
      .Handle((in AppLogic.Output.HideGame _) => FadeToBlack());

    // Enter the first state to kick off the binding side effects.
    AppLogic.Start();
  }

  public void OnNewGame() => AppLogic.Input(new AppLogic.Input.NewGame());

  public void OnLoadGame() => AppLogic.Input(new AppLogic.Input.LoadGame());

  public void OnAnimationFinished(StringName animation) {
    // There's only two animations :)
    // We don't care what state we're in — we just tell the current state what's
    // happened and it will do the right thing.

    if (animation == "fade_in") {
      AppLogic.Input(new AppLogic.Input.FadeInFinished());
      BlankScreen.Hide();
      return;
    }

    AppLogic.Input(new AppLogic.Input.FadeOutFinished());
  }

  public void FadeInFromBlack() {
    BlankScreen.Show();
    AnimationPlayer.Play("fade_in");
  }

  public void FadeToBlack() {
    BlankScreen.Show();
    AnimationPlayer.Play("fade_out");
  }

  public void HideMenus() {
    Splash.Hide();
    Menu.Hide();
  }

  public void OnExitTree() {
    // Cleanup things we own.
    AppLogic.Stop();
    AppBinding.Dispose();
    AppRepo.Dispose();

    Menu.NewGame -= OnNewGame;

    AnimationPlayer.AnimationFinished -= OnAnimationFinished;
  }
}
