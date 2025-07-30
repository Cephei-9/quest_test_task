using Locations;
using Naninovel;

[CommandAlias("hideLocation")]
public class HideLocationCommand : Command
{
    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        LocationService locationService = Engine.GetService<LocationService>();
        locationService.HideLocation();
            
        return UniTask.CompletedTask;
    }
}