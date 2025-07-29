using Inventory;
using Naninovel;

[CommandAlias("addItem")]
public class AddItemCommand : Command
{
    [RequiredParameter]
    [ParameterAlias("itemId")]
    public StringParameter ItemId;

    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        InventoryService inventoryService = Engine.GetService<InventoryService>();
        inventoryService.AddItem(ItemId);
        return UniTask.CompletedTask;
    }
}