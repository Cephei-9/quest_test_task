using Inventory;
using Naninovel;

[CommandAlias("removeItem")]
public class RemoveItemCommand : Command
{
    [RequiredParameter]
    [ParameterAlias("itemId")]
    public StringParameter ItemId;

    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        InventoryService inventoryService = Engine.GetService<InventoryService>();
        inventoryService.RemoveItem(ItemId);
        return UniTask.CompletedTask;
    }
}