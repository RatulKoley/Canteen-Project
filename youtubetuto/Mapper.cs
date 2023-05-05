using AutoMapper;

namespace youtubetuto
{
	public class Mapper : Profile
	{
		public Mapper()
		{
			CreateMap<ItemDTO, Item>().ReverseMap();
			CreateMap<StockDTO, Stock>().ReverseMap();
			CreateMap<UnitDTO, Unit>().ReverseMap();
			CreateMap<PurchaseDTO, Purchase>().ReverseMap();
			CreateMap<SupplyDTO, Supply>().ReverseMap();
			CreateMap<FoodMenuDTO, FoodMenu>().ReverseMap();
			CreateMap<FoodMapppingDTO, FoodMapping>().ReverseMap();
			CreateMap<KitchenFoodDTO, KitchenFood>().ReverseMap();
			CreateMap<SalesDTO, Sales>().ReverseMap();
		}
	}
}
