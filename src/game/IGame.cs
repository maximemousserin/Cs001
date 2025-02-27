namespace Cs001;

using Chickensoft.AutoInject;
using Chickensoft.Collections;
using Chickensoft.GodotNodeInterfaces;
using Chickensoft.SaveFileBuilder;

public interface IGame : INode2D, IProvide<IGameRepo>, IProvide<EntityTable>, IProvide<ISaveChunk<GameData>> {
  // void LoadExistingGame();
  // event Game.SaveFileLoadedEventHandler? SaveFileLoaded;
}
