namespace Cs001;

using Chickensoft.AutoInject;
using Chickensoft.GodotNodeInterfaces;

public interface IApp : ICanvasLayer, IProvide<IAppRepo> {
}
