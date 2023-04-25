using AutoMapper;

namespace youtubetuto
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<ItemDTO, Item>();
            CreateMap<StockDTO, Stock>();
            CreateMap<UnitDTO, Unit>();
            CreateMap<PurchaseDTO, Purchase>();
            CreateMap<SupplyDTO, Supply>();
            CreateMap<FoodMenuDTO, FoodMenu>();
            CreateMap<FoodMapppingDTO, FoodMapping>();
            CreateMap<KitchenFoodDTO, KitchenFood>();
            CreateMap<SalesDTO, Sales>();
        }
    }
}
