using Locations;
using Naninovel;

[CommandAlias("changeLocation")]
public class ChangeLocationCommand : Command
{
    [RequiredParameter]
    [ParameterAlias("locationId")]
    public StringParameter LocationId;

    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        LocationService locationService = Engine.GetService<LocationService>();
        locationService.ChangeLocation(LocationId);
            
        return UniTask.CompletedTask;
    }
}