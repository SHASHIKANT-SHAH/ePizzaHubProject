using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Respositories.Interfaces;
using ePizzaHub.Services.Interfaces;


namespace ePizzaHub.Services.Implementations
{
    public class ItemService : Service<Item>, IItemService
    {
        IRepository<Item> _itemRepo;
        public ItemService(IRepository<Item> itemRepo) : base(itemRepo)
        {
        }

        public IEnumerable<ItemModel> GetItems()
        {
            return _itemRepo.GetAll().OrderBy(item => item.CategoryId).ThenBy(item => item.ItemType).Select(i => new ItemModel
            {
                Id= i.Id,
                Name= i.Name,
                CategoryId= i.CategoryId,
                Description= i.Description,
                ImageUrl= i.ImageUrl,
                ItemTypeId= i.ItemTypeId,  
                UnitPrice= i.UnitPrice,
            });
        }
    }
}
