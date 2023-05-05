using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using youtubetuto;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<DataContext>(test =>
{
	test.UseSqlServer(builder.Configuration.GetConnectionString("dbcon"));
	test.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddCors(_ =>
{
	_.AddPolicy("AllowOrigin", builder =>
	{
		builder.AllowAnyHeader()
			.AllowAnyMethod()
			.SetIsOriginAllowedToAllowWildcardSubdomains()
			.SetIsOriginAllowed(origin => true)
			.AllowCredentials();
	});
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors("AllowOrigin");
app.UseRouting();


app.MapGet("/Unit", async (DataContext con, IMapper imap) =>
{
	var UnitData = await con.Unit.Include(test => test.Item).ToListAsync();
	return Results.Ok(imap.Map<List<UnitDTO>>(UnitData));
});

app.MapGet("/Item", async (DataContext con, IMapper imap) =>
{
	var ItemData = await con.Item.Include(test => test.Stock).Include(test => test.Purchase)
.Include(test => test.FoodMapping).ToListAsync();
	return Results.Ok(imap.Map<List<ItemDTO>>(ItemData));
});

app.MapGet("/Stock", async (DataContext con, IMapper imap) =>
{
	var StockData = await con.Stock.Include(test => test.Item).ToListAsync();
	return Results.Ok(imap.Map<List<StockDTO>>(StockData));
});

app.MapGet("/Purchase", async (DataContext con, IMapper imap) =>
{
	var PurchaseData = await con.Purchase.Include(test => test.Item)
.Include(test => test.Supply).ToListAsync();
	return Results.Ok(imap.Map<List<PurchaseDTO>>(PurchaseData));
});

app.MapGet("/Supply", async (DataContext con, IMapper imap) =>
{
	var SupplyData = await con.Supply.Include(test => test.Purchase).ToListAsync();
	return Results.Ok(imap.Map<List<SupplyDTO>>(SupplyData));
});

app.MapGet("/FoodMenu", async (DataContext con, IMapper imap) =>
{
	var FoodMenuData = await con.FoodMenu.Include(test => test.FoodMapping).Include(test => test.KitchenFood).ToListAsync();
	return Results.Ok(imap.Map<List<FoodMenuDTO>>(FoodMenuData));
});

app.MapGet("/FoodMapping", async (DataContext con, IMapper imap) =>
{
	var FoodMappingData = await con.FoodMapping.Include(test => test.FoodMenu).ToListAsync();
	return Results.Ok(imap.Map<List<FoodMapppingDTO>>(FoodMappingData));
});

app.MapGet("/KitchenFood", async (DataContext con, IMapper imap) =>
{
	var KitchenFoodData = await con.KitchenFood.Include(test => test.Sales).ToListAsync();
	return Results.Ok(imap.Map<List<KitchenFoodDTO>>(KitchenFoodData));
});

app.MapGet("/Sales", async (DataContext con, IMapper imap) =>
{
	var SalesData = await con.Sales.Include(test => test.KitchenFood).ToListAsync();
	return Results.Ok(imap.Map<List<SalesDTO>>(SalesData));
});

//app.MapGet("/Unit", async (DataContext con) =>
//await con.Unit.Include(test => test.Item).ToListAsync());

//app.MapGet("/Item", async (DataContext con) =>
//await con.Item.Include(test => test.Stock).Include(test => test.Purchase)
//.Include(test => test.FoodMapping).ToListAsync());

//app.MapGet("/Stock", async (DataContext con) =>
//await con.Stock.Include(test => test.Item).ToListAsync());

//app.MapGet("/Purchase", async (DataContext con) =>
//await con.Purchase.Include(test => test.Item)
//.Include(test => test.Supply).ToListAsync());

//app.MapGet("/Supply", async (DataContext con) =>
//await con.Supply.Include(test => test.Purchase).ToListAsync());

//app.MapGet("/FoodMenu", async (DataContext con) =>
//await con.FoodMenu.Include(test => test.FoodMapping).Include(test => test.KitchenFood)
//.ToListAsync());

//app.MapGet("/FoodMapping", async (DataContext con) =>
//await con.FoodMapping.Include(test => test.FoodMenu).ToListAsync());

//app.MapGet("/KitchenFood", async (DataContext con) =>
//await con.KitchenFood.Include(test => test.Sales).ToListAsync());

//app.MapGet("/Sales", async (DataContext con) =>
//await con.Sales.Include(test => test.KitchenFood).ToListAsync());


app.MapGet("/Unit/{id:int}", async (DataContext con, IMapper imap, int id) =>
{
	var UnitData = await con.Unit.Include(test => test.Item).Where(test => test.ID == id).ToListAsync();
	return Results.Ok(imap.Map<List<UnitDTO>>(UnitData));
});

app.MapGet("/Unit/{name}", async (DataContext con, IMapper imap, string name) =>
{
	var UnitData = await con.Unit.Include(test => test.Item).Where(test => test.UnitName == name).ToListAsync();
	return Results.Ok(imap.Map<List<UnitDTO>>(UnitData));
});

app.MapGet("/Item/{id:int}", async (DataContext con, IMapper imap, int id) =>
{
	var ItemData = await con.Item.Include(test => test.Stock).Include(test => test.Purchase).Include(test => test.FoodMapping)
.Where(test => test.ItemCode == id).ToListAsync();
	return Results.Ok(imap.Map<List<ItemDTO>>(ItemData));
});

app.MapGet("/Item/{name}", async (DataContext con, IMapper imap, string name) =>
{
	var ItemData = await con.Item.Include(test => test.Stock).Include(test => test.Purchase).Include(test => test.FoodMapping)
.Where(test => test.ItemName == name).ToListAsync();
	return Results.Ok(imap.Map<List<ItemDTO>>(ItemData));
});


app.MapGet("/Stock/{id:int}", async (DataContext con, IMapper imap, int id) =>
{
	var StockData = await con.Stock.Include(test => test.Item).Where(test => test.StockID == id).ToListAsync();
	return Results.Ok(imap.Map<List<StockDTO>>(StockData));
});
//app.MapGet("/Stock/Item/{id:int}", async (DataContext con, IMapper imap, int id) =>
//{
//	var StockData = await con.Stock.Include(test => test.Item).Where(test => test.ItemId == id).ToListAsync();
//	return Results.Ok(imap.Map<List<StockDTO>>(StockData));
//});

app.MapGet("/Stock/Item/{name}", async (DataContext con, IMapper imap, string name) =>
{
	var itemname = await con.Item.Where(test => test.ItemName == name).FirstOrDefaultAsync();
	if (itemname != null)
	{
		var StockData = await con.Stock.Include(test => test.Item).Where(test => test.ItemId == itemname.ItemCode).ToListAsync();
		return Results.Ok(imap.Map<List<StockDTO>>(StockData));
	}
	else
		return Results.NotFound("Item Not Found");
});


app.MapGet("/Purchase/{id:int}", async (DataContext con, IMapper imap, int id) =>
{
	var PurchaseData = await con.Purchase.Include(test => test.Item).Include(test => test.Supply)
.Where(test => test.PurchaseNo == id).ToListAsync();
	return Results.Ok(imap.Map<List<PurchaseDTO>>(PurchaseData));
});

app.MapGet("/Purchase/Item/{name}", async (DataContext con, IMapper imap, string name) =>
{
	var itemname = await con.Item.Where(test => test.ItemName == name).FirstOrDefaultAsync();
	if (itemname != null)
	{
		var PurchaseData = await con.Purchase.Include(test => test.Item).Include(test => test.Supply)
	.Where(test => test.ItemId == itemname.ItemCode).ToListAsync();
		return Results.Ok(imap.Map<List<PurchaseDTO>>(PurchaseData));
	}
	else
		return Results.NotFound("Item Not Found");
});

app.MapGet("/Purchase/Supplier/{name}", async (DataContext con, IMapper imap, string name) =>
{
	var suppliername = await con.Supply.Where(test => test.SupplierName == name).FirstOrDefaultAsync();
	if (suppliername != null)
	{
		var PurchaseData = await con.Purchase.Include(test => test.Item).Include(test => test.Supply)
	.Where(test => test.SupplyId == suppliername.SupplyID).ToListAsync();
		return Results.Ok(imap.Map<List<PurchaseDTO>>(PurchaseData));
	}
	else
		return Results.NotFound("Supplier Not Found");
});

app.MapGet("/Supply/{id:int}", async (DataContext con, IMapper imap, int id) =>
{
	var SupplyData = await con.Supply.Include(test => test.Purchase)
.Where(test => test.SupplyID == id).ToListAsync();
	return Results.Ok(imap.Map<List<SupplyDTO>>(SupplyData));
});

app.MapGet("/Supply/{name}", async (DataContext con, IMapper imap, string name) =>
{
	var SupplyData = await con.Supply.Include(test => test.Purchase)
.Where(test => test.SupplierName == name).ToListAsync();
	return Results.Ok(imap.Map<List<SupplyDTO>>(SupplyData));
});

app.MapGet("/FoodMenu/{id:int}", async (DataContext con, IMapper imap, int id) =>
{
	var FoodMenuData = await con.FoodMenu.Include(test => test.FoodMapping).Include(test => test.KitchenFood).Where(test => test.FoodID == id).ToListAsync();
	return Results.Ok(imap.Map<List<FoodMenuDTO>>(FoodMenuData));
});

app.MapGet("/FoodMenu/{name}", async (DataContext con, IMapper imap, string name) =>
{
	var FoodMenuData = await con.FoodMenu.Include(test => test.FoodMapping).Include(test => test.KitchenFood)
.Where(test => test.FoodName == name).ToListAsync();
	return Results.Ok(imap.Map<List<FoodMenuDTO>>(FoodMenuData));
});


app.MapGet("/FoodMapping/{id:int}", async (DataContext con, IMapper imap, int id) =>
{
	var FoodMappingData = await con.FoodMapping.Include(test => test.FoodMenu)
.Where(test => test.MappingID == id).Where(test => test.Active == true).ToListAsync();
	return Results.Ok(imap.Map<List<FoodMapppingDTO>>(FoodMappingData));
});

app.MapGet("/FoodMapping/Food/{name}", async (DataContext con, IMapper imap, string name) =>
{
	var foodname = await con.FoodMenu.Where(test => test.FoodName == name).FirstOrDefaultAsync();
	if (foodname != null)
	{
		var FoodMappingData = await con.FoodMapping.Include(test => test.FoodMenu)
	.Where(test => test.FoodID == foodname.FoodID).Where(test => test.Active == true).ToListAsync();
		return Results.Ok(imap.Map<List<FoodMapppingDTO>>(FoodMappingData));
	}
	else
		return Results.NotFound("Food Item Not Found");
});

app.MapGet("/FoodMapping/Item/{name}", async (DataContext con, IMapper imap, string name) =>
{
	var itemname = await con.Item.Where(test => test.ItemName == name).FirstOrDefaultAsync();
	if (itemname != null)
	{
		var FoodMappingData = await con.FoodMapping.Include(test => test.FoodMenu)
	.Where(test => test.ItemId == itemname.ItemCode).Where(test => test.Active == true).ToListAsync();
		return Results.Ok(imap.Map<List<FoodMapppingDTO>>(FoodMappingData));
	}
	else
		return Results.NotFound("Item Not Found");
});

app.MapGet("/KitchenFood/{id:int}", async (DataContext con, IMapper imap, int id) =>
{
	var KitchenFoodData = await con.KitchenFood.Include(test => test.Sales)
.Where(test => test.KitchenFoodID == id).ToListAsync();
	return Results.Ok(imap.Map<List<KitchenFoodDTO>>(KitchenFoodData));
});

app.MapGet("/KitchenFood/Food/{name}", async (DataContext con, IMapper imap, string name) =>
{
	var foodname = await con.FoodMenu.Where(test => test.FoodName == name).FirstOrDefaultAsync();
	if (foodname != null)
	{
		var KitchenFoodData = await con.KitchenFood.Include(test => test.Sales)
	.Where(test => test.FoodID == foodname.FoodID).ToListAsync();
		return Results.Ok(imap.Map<List<KitchenFoodDTO>>(KitchenFoodData));
	}
	else
		return Results.NotFound("Food Item Not Found");
});

app.MapGet("/Sales/{id:int}", async (DataContext con, IMapper imap, int id) =>
{
	var SalesData = await con.Sales.Include(test => test.KitchenFood)
.Where(test => test.SalesID == id).ToListAsync();
	return Results.Ok(imap.Map<List<SalesDTO>>(SalesData));
});

app.MapGet("/Sales/CustomerName/{name}", async (DataContext con, IMapper imap, string name) =>
{
	var SalesData = await con.Sales.Include(test => test.KitchenFood)
.Where(test => test.CustomerName == name).ToListAsync();
	return Results.Ok(imap.Map<List<SalesDTO>>(SalesData));
});

app.MapGet("/Sales/CustomerType/{name}", async (DataContext con, IMapper imap, string name) =>
{
	var SalesData = await con.Sales.Include(test => test.KitchenFood)
.Where(test => test.CustomerType == name).ToListAsync();
	return Results.Ok(imap.Map<List<SalesDTO>>(SalesData));
});

//app.MapGet("/Sales/Food/{id:int}", async (DataContext con, IMapper imap, int id) =>
//{
//	var SalesData = await con.Sales.Include(test => test.KitchenFood)
//.Where(test => test.KitchenFoodID == id).ToListAsync();
//	return Results.Ok(imap.Map<List<SalesDTO>>(SalesData));
//});

app.MapGet("/Sales/Food/{name}", async (DataContext con, IMapper imap, string name) =>
{
	var foodname = await con.FoodMenu.Where(test => test.FoodName == name).FirstOrDefaultAsync();
	if (foodname != null)
	{
		var kitchenid = await con.KitchenFood
	.Where(test => test.FoodID == foodname.FoodID).FirstOrDefaultAsync();
		if (kitchenid != null)
		{
			var SalesData = await con.Sales.Include(test => test.KitchenFood)
	.Where(test => test.KitchenFoodID == kitchenid.KitchenFoodID).ToListAsync();
			return Results.Ok(imap.Map<List<SalesDTO>>(SalesData));
		}
		else
			return Results.NotFound("Kitchen Food Not Found");
	}
	else
		return Results.NotFound("Food Item Not Found");
});

//app.MapGet("/Item/{id:int}", async (DataContext con, int id) =>
//await con.Item.Include(test => test.Stock).Include(test => test.Purchase).Include(test => test.FoodMapping)
//.Where(test => test.ItemCode == id).ToListAsync());

//app.MapGet("/Item/{name}", async (DataContext con, string name) =>
//await con.Item.Include(test => test.Stock).Include(test => test.Purchase).Include(test => test.FoodMapping)
//.Where(test => test.ItemName == name).ToListAsync());

//app.MapGet("/Stock/{id}", async (DataContext con, int id) =>
//await con.Stock.Include(test => test.Item).Where(test => test.StockID == id).ToListAsync());

//app.MapGet("/Stock/Item/{id}", async (DataContext con, int id) =>
//await con.Stock.Include(test => test.Item).Where(test => test.ItemId == id).ToListAsync());

//app.MapGet("/Unit/{id:int}", async (DataContext con, int id) =>
//await con.Unit.Include(test => test.Item)
//.Where(test => test.ID == id).ToListAsync());

//app.MapGet("/Unit/{name}", async (DataContext con, string name) =>
//await con.Unit.Include(test => test.Item)
//.Where(test => test.UnitName == name).ToListAsync());

//app.MapGet("/Purchase/{id}", async (DataContext con, int id) =>
//await con.Purchase.Include(test => test.Item).Include(test => test.Supply)
//.Where(test => test.PurchaseNo == id).ToListAsync());

//app.MapGet("/Purchase/Item/{id}", async (DataContext con, int id) =>
//await con.Purchase.Include(test => test.Item).Include(test => test.Supply)
//.Where(test => test.ItemId == id).ToListAsync());

//app.MapGet("/Purchase/Supplier/{id}", async (DataContext con, int id) =>
//await con.Purchase.Include(test => test.Item).Include(test => test.Supply)
//.Where(test => test.SupplyId == id).ToListAsync());

//app.MapGet("/Supply/{id:int}", async (DataContext con, int id) =>
//await con.Supply.Include(test => test.Purchase)
//.Where(test => test.SupplyID == id).ToListAsync());

//app.MapGet("/Supply/{name}", async (DataContext con, string name) =>
//await con.Supply.Include(test => test.Purchase)
//.Where(test => test.SupplierName == name).ToListAsync());

//app.MapGet("/FoodMenu/{id:int}", async (DataContext con, int id) =>
//await con.FoodMenu.Include(test => test.FoodMapping).Include(test => test.KitchenFood)
//.Where(test => test.FoodID == id).ToListAsync());

//app.MapGet("/FoodMenu/{name}", async (DataContext con, string name) =>
//await con.FoodMenu.Include(test => test.FoodMapping).Include(test => test.KitchenFood)
//.Where(test => test.FoodName == name).ToListAsync());

//app.MapGet("/FoodMapping/Food/{id}", async (DataContext con, int id) =>
//await con.FoodMapping.Include(test => test.FoodMenu)
//.Where(test => test.FoodID == id).Where(test => test.Active == true).ToListAsync());

//app.MapGet("/FoodMapping/Item/{id}", async (DataContext con, int id) =>
//await con.FoodMapping.Include(test => test.FoodMenu)
//.Where(test => test.ItemId == id).Where(test => test.Active == true).ToListAsync());

//app.MapGet("/KitchenFood/{id}", async (DataContext con, int id) =>
//await con.KitchenFood.Include(test => test.Sales)
//.Where(test => test.KitchenFoodID == id).ToListAsync());

//app.MapGet("/KitchenFood/Food/{id}", async (DataContext con, int id) =>
//await con.KitchenFood.Include(test => test.Sales)
//.Where(test => test.FoodID == id).ToListAsync());

//app.MapGet("/Sales/{id}", async (DataContext con, int id) =>
//await con.Sales.Include(test => test.KitchenFood)
//.Where(test => test.SalesID == id).ToListAsync());

//app.MapGet("/Sales/CustomerName/{name}", async (DataContext con, string name) =>
//await con.Sales.Include(test => test.KitchenFood)
//.Where(test => test.CustomerName == name).ToListAsync());

//app.MapGet("/Sales/CustomerType/{id}", async (DataContext con, string name) =>
//await con.Sales.Include(test => test.KitchenFood)
//.Where(test => test.CustomerType == name).ToListAsync());

//app.MapGet("/Sales/Food/{id}", async (DataContext con, int id) =>
//await con.Sales.Include(test => test.KitchenFood)
//.Where(test => test.KitchenFoodID == id).ToListAsync());


app.MapPost("/Item", async (DataContext con, ItemDTO i1, IMapper imap) =>
{
	var unitid = await con.Unit.Where(test => test.UnitName == i1.UnitName).FirstOrDefaultAsync();
	if (unitid != null)
		i1.UnitId = unitid.ID;
	else
		return Results.NotFound("Unit Is Unavailable");


	var newitem = imap.Map<Item>(i1);
	con.Item.Add(newitem);
	await con.SaveChangesAsync();
	return Results.Created($"/Item/{newitem.ItemCode}", newitem);
});

app.MapPost("/Unit", async (DataContext con, UnitDTO U1, IMapper imap) =>
{
	var newunit = imap.Map<Unit>(U1);
	con.Unit.Add(newunit);
	await con.SaveChangesAsync();
	return Results.Created($"/Unit/{newunit.ID}", newunit);
});

app.MapPost("/Stock", async (DataContext con, StockDTO s1, IMapper imap) =>
{
	var itemid = await con.Item.Where(test => test.ItemName == s1.ItemName).FirstOrDefaultAsync();
	if (itemid != null)
		s1.ItemId = itemid.ItemCode;
	else
		return Results.NotFound("Item Is Unavailable");

	var result = await con.Stock.Where(test => test.ItemId == s1.ItemId).FirstOrDefaultAsync();
	if (result != null)
	{
		return Results.NotFound("Stock Item Already Exists, Use Update Call");
	}
	var newstock = imap.Map<Stock>(s1);
	con.Stock.Add(newstock);
	await con.SaveChangesAsync();
	return Results.Created($"/Stock/{newstock.StockID}", newstock);
});

app.MapPost("/Purchase", async (DataContext con, PurchaseDTO P1, IMapper imap) =>
{
	var itemid = await con.Item.Where(test => test.ItemName == P1.ItemName).FirstOrDefaultAsync();
	if (itemid != null)
		P1.ItemId = itemid.ItemCode;
	else
		return Results.NotFound("Item Is Unavailable");

	var supplyid = await con.Supply.Where(test => test.SupplierName == P1.suppliermame).FirstOrDefaultAsync();
	if (supplyid != null)
		P1.SupplyId = supplyid.SupplyID;
	else
		return Results.NotFound("Supplier Is Unavailable");
	P1.PurchasedValue = P1.Price * P1.Quantity;
	var newpurchase = imap.Map<Purchase>(P1);
	con.Purchase.Add(newpurchase);
	var checkstock = await con.Stock.Where(test => test.ItemId == newpurchase.ItemId).FirstOrDefaultAsync();
	if (checkstock == null)
	{
		var newstock = new Stock();
		newstock.ItemId = newpurchase.ItemId;
		newstock.Qunatity = newpurchase.Quantity;
		con.Stock.Add(newstock);
	}
	else
	{
		checkstock.Qunatity = checkstock.Qunatity + newpurchase.Quantity;
		con.Stock.Update(checkstock);
	}
	await con.SaveChangesAsync();
	return Results.Created($"/Purchase/{newpurchase.PurchaseNo}", newpurchase);
});

app.MapPost("/Supply", async (DataContext con, SupplyDTO s1, IMapper imap) =>
{
	var newsupplier = imap.Map<Supply>(s1);
	con.Supply.Add(newsupplier);
	await con.SaveChangesAsync();
	return Results.Created($"/Supply/{newsupplier.SupplyID}", newsupplier);
});

app.MapPost("/FoodMenu", async (DataContext con, FoodMenuDTO f1, IMapper imap) =>
{
	var newfood = imap.Map<FoodMenu>(f1);
	con.FoodMenu.Add(newfood);
	await con.SaveChangesAsync();
	return Results.Created($"/FoodMenu/{newfood.FoodID}", newfood);
});

app.MapPost("/FoodMapping", async (DataContext con, FoodMapppingDTO f1, IMapper imap) =>
{
	var itemid = await con.Item.Where(test => test.ItemName == f1.ItemName).FirstOrDefaultAsync();
	if (itemid != null)
		f1.ItemId = itemid.ItemCode;
	else
		return Results.NotFound("Item Is Unavailable");

	var foodid = await con.FoodMenu.Where(test => test.FoodName == f1.FoodName).FirstOrDefaultAsync();
	if (foodid != null)
		f1.FoodID = foodid.FoodID;
	else
		return Results.NotFound("Food Is Unavailable");


	var result = await con.FoodMenu.
				  Where(test => test.FoodID == f1.FoodID).FirstOrDefaultAsync();
	if (result == null)
		return Results.NotFound("Food Is not Available");
	var result2 = await con.Item.
				  Where(test => test.ItemCode == f1.ItemId).FirstOrDefaultAsync();
	if (result2 == null)
		return Results.NotFound("Item Is not Available");
	var newmapping = imap.Map<FoodMapping>(f1);
	con.FoodMapping.Add(newmapping);
	await con.SaveChangesAsync();
	return Results.Created($"/FoodMapping/{newmapping.MappingID}", newmapping);
});




app.MapPost("/KitchenFood", async (DataContext con, KitchenFoodDTO k1, IMapper imap) =>
{
	var foodid = await con.FoodMenu.Where(test => test.FoodName == k1.FoodName).FirstOrDefaultAsync();
	if (foodid != null)
		k1.FoodID = foodid.FoodID;
	else
		return Results.NotFound("Food Is Unavailable");

	var result = await con.KitchenFood.Where(test => test.FoodID == k1.FoodID).FirstOrDefaultAsync();
	if (result != null)
	{
		return Results.NotFound("Kitchen Food Already Exists, Use Update Call");
	}

	var newkitchenfood = imap.Map<KitchenFood>(k1);
	con.KitchenFood.Add(newkitchenfood);
	var findmapping = await con.FoodMapping.Where(test => test.FoodID == newkitchenfood.FoodID)
	.Where(test => test.Active == true).ToListAsync();

	foreach (var MapItem in findmapping)
	{
		double ForOneItemQuantity = 1;
		double ForKitchenItemQuantity = 1;
		if (MapItem.ItemQuantity != null && MapItem.FoodQuantity != null && newkitchenfood.QuantityPrepared != null)
		{
			ForOneItemQuantity = (double)(MapItem.ItemQuantity / MapItem.FoodQuantity);
			ForKitchenItemQuantity = (double)(ForOneItemQuantity * newkitchenfood.QuantityPrepared);
		}
		var updatestock = await con.Stock.Where(test => test.ItemId == MapItem.ItemId).FirstOrDefaultAsync();
		if (updatestock != null)
		{
			if (updatestock.Qunatity != null)
			{
				if (updatestock.Qunatity >= ForKitchenItemQuantity)
				{
					updatestock.Qunatity = updatestock.Qunatity - ForKitchenItemQuantity;
					con.Stock.Update(updatestock);
				}
				else if (updatestock.Qunatity < ForKitchenItemQuantity)
				{
					var itemname = await con.Item.Where(test => test.ItemCode == updatestock.ItemId).FirstOrDefaultAsync();
					if (itemname != null)
						return Results.NotFound("Stock Is Very Low For ItemName " + itemname.ItemName);
				}
			}
		}
	}
	await con.SaveChangesAsync();
	return Results.Created($"/KitchenFood/{newkitchenfood.KitchenFoodID}", newkitchenfood);
});


app.MapPost("/Sales", async (DataContext con, SalesDTO s1, IMapper imap) =>
{
	var foodid = await con.FoodMenu.Where(test => test.FoodName == s1.KitchenFoodName).FirstOrDefaultAsync();
	if (foodid != null)
	{
		var kitchenfoodid = await con.KitchenFood.Where(test => test.FoodID == foodid.FoodID).FirstOrDefaultAsync();
		if (kitchenfoodid != null)
			s1.KitchenFoodID = kitchenfoodid.KitchenFoodID;
		else
			return Results.NotFound("Kitchen Food Is Unavailable");
	}
	else
		return Results.NotFound("Food Is Unavailable");

	var kitchenfoodremove = await con.KitchenFood.Where(test => test.KitchenFoodID == s1.KitchenFoodID).FirstOrDefaultAsync();
	if (kitchenfoodremove != null)
	{
		if (kitchenfoodremove.QuantityPrepared < s1.Quantity)
		{
			var foodname = await con.FoodMenu.Where(test => test.FoodID == kitchenfoodremove.FoodID).FirstOrDefaultAsync();
			if (foodname != null)
				return Results.NotFound("The Food Item " + foodname.FoodName + "is not available enough");
		}
		kitchenfoodremove.QuantityPrepared = kitchenfoodremove.QuantityPrepared - s1.Quantity;
		con.KitchenFood.Update(kitchenfoodremove);
		var fooditemprice = await con.FoodMenu.Where(test => test.FoodID == kitchenfoodremove.FoodID).FirstOrDefaultAsync();
		if (fooditemprice != null)
		{
			if (s1.Price != null && s1.Quantity != null && fooditemprice.Price != null)
			{
				s1.Price = s1.Quantity * fooditemprice.Price;
			}
		}
	}
	var newsale = imap.Map<Sales>(s1);
	con.Sales.Add(newsale);
	await con.SaveChangesAsync();
	return Results.Created($"/Item/{newsale.SalesID}", newsale);
});





app.MapPut("/Item", async (DataContext con, ItemDTO i1, IMapper imap) =>
{
	var unitid = await con.Unit.Where(test => test.UnitName == i1.UnitName).FirstOrDefaultAsync();
	if (unitid != null)
		i1.UnitId = unitid.ID;
	else
		return Results.NotFound("Unit Is Unavailable");

	var updateitem = imap.Map<Item>(i1);
	con.Item.Update(updateitem);
	await con.SaveChangesAsync();
	return Results.Ok(updateitem);
});
app.MapPut("/Stock", async (DataContext con, StockDTO s1, IMapper imap) =>
{
	var itemid = await con.Item.Where(test => test.ItemName == s1.ItemName).FirstOrDefaultAsync();
	if (itemid != null)
		s1.ItemId = itemid.ItemCode;
	else
		return Results.NotFound("Item Is Unavailable");

	var updatestock = imap.Map<Stock>(s1);
	con.Stock.Update(updatestock);
	await con.SaveChangesAsync();
	return Results.Ok(updatestock);
});
app.MapPut("/Unit", async (DataContext con, UnitDTO u1, IMapper imap) =>
{
	var updateunit = imap.Map<Unit>(u1);
	con.Unit.Update(updateunit);
	await con.SaveChangesAsync();
	return Results.Ok(updateunit);
});

app.MapPut("/Purchase", async (DataContext con, PurchaseDTO p1, IMapper imap) =>
{
	var itemid = await con.Item.Where(test => test.ItemName == p1.ItemName).FirstOrDefaultAsync();
	if (itemid != null)
		p1.ItemId = itemid.ItemCode;
	else
		return Results.NotFound("Item Is Unavailable");

	var supplyid = await con.Supply.Where(test => test.SupplierName == p1.suppliermame).FirstOrDefaultAsync();
	if (supplyid != null)
		p1.SupplyId = supplyid.SupplyID;
	else
		return Results.NotFound("Supplier Is Unavailable");

	var oldpurquant = await con.Purchase.Where(test => test.PurchaseNo == p1.PurchaseNo).FirstOrDefaultAsync();
	if (oldpurquant != null)
	{
		if (oldpurquant.Quantity != p1.Quantity || oldpurquant.ItemId != p1.ItemId)
		{
			var checkstock = await con.Stock.Where(test => test.ItemId == p1.ItemId).FirstOrDefaultAsync();

			if (oldpurquant.Quantity > p1.Quantity)
			{

				double updatequant = ((double)(oldpurquant.Quantity - p1.Quantity));
				if (checkstock != null)
				{
					checkstock.Qunatity = checkstock.Qunatity - updatequant;
					con.Stock.Update(checkstock);
				}
			}
			else if (oldpurquant.Quantity < p1.Quantity)
			{
				double updatequant = ((double)(p1.Quantity - oldpurquant.Quantity));
				if (checkstock != null)
				{
					checkstock.Qunatity = checkstock.Qunatity + updatequant;
					con.Stock.Update(checkstock);
				}
			}
			else if (checkstock == null)
			{
				var newstock = new Stock();
				newstock.ItemId = p1.ItemId;
				newstock.Qunatity = p1.Quantity;
				con.Stock.Add(newstock);
			}
		}
	}
	p1.PurchasedValue = p1.Quantity * p1.Price;
	oldpurquant = imap.Map<Purchase>(p1);
	con.Purchase.Update(oldpurquant);
	await con.SaveChangesAsync();
	return Results.Ok(oldpurquant);
});
app.MapPut("/Supply", async (DataContext con, SupplyDTO s1, IMapper imap) =>
{
	var updateSupply = imap.Map<Supply>(s1);
	con.Supply.Update(updateSupply);
	await con.SaveChangesAsync();
	return Results.Ok(updateSupply);
});

app.MapPut("/FoodMenu", async (DataContext con, FoodMenuDTO f1, IMapper imap) =>
{
	var updatefood = imap.Map<FoodMenu>(f1);
	con.FoodMenu.Update(updatefood);
	await con.SaveChangesAsync();
	return Results.Ok(updatefood);
});

app.MapPut("/FoodMapping", async (DataContext con, FoodMapppingDTO f1, IMapper imap) =>
{
	var itemid = await con.Item.Where(test => test.ItemName == f1.ItemName).FirstOrDefaultAsync();
	if (itemid != null)
		f1.ItemId = itemid.ItemCode;
	else
		return Results.NotFound("Item Is Unavailable");

	var foodid = await con.FoodMenu.Where(test => test.FoodName == f1.FoodName).FirstOrDefaultAsync();
	if (foodid != null)
		f1.FoodID = foodid.FoodID;
	else
		return Results.NotFound("Food Is Unavailable");

	var updatemapping = imap.Map<FoodMapping>(f1);
	con.FoodMapping.Update(updatemapping);
	await con.SaveChangesAsync();
	return Results.Ok(updatemapping);
});

app.MapPut("/KitchenFood", async (DataContext con, KitchenFoodDTO k1, IMapper imap) =>
{
	var foodid = await con.FoodMenu.Where(test => test.FoodName == k1.FoodName).FirstOrDefaultAsync();
	if (foodid != null)
		k1.FoodID = foodid.FoodID;
	else
		return Results.NotFound("Food Is Unavailable");

	var oldkitchenfood = await con.KitchenFood.Where(test => test.KitchenFoodID == k1.KitchenFoodID).FirstOrDefaultAsync();
	if (oldkitchenfood != null)
	{
		if (oldkitchenfood.QuantityPrepared != k1.QuantityPrepared)
		{
			var findmapping = await con.FoodMapping.Where(test => test.FoodID == oldkitchenfood.FoodID)
	 .Where(test => test.Active == true).ToListAsync();
			foreach (var MapItem in findmapping)
			{
				double ForOneItemQuantity = 1;
				double ForKitchenOldItemQuantity = 1;
				double ForKitchenNewItemQuantity = 1;
				if (MapItem.ItemQuantity != null && MapItem.FoodQuantity != null && oldkitchenfood.QuantityPrepared != null && k1.QuantityPrepared != null)
				{
					ForOneItemQuantity = (double)(MapItem.ItemQuantity / MapItem.FoodQuantity);
					ForKitchenOldItemQuantity = (double)(ForOneItemQuantity * oldkitchenfood.QuantityPrepared);
					ForKitchenNewItemQuantity = (double)(ForOneItemQuantity * k1.QuantityPrepared);
				}
				var updatestock = await con.Stock.Where(test => test.ItemId == MapItem.ItemId).FirstOrDefaultAsync();
				if (updatestock != null)
				{
					if (updatestock.Qunatity != null)
					{
						updatestock.Qunatity = updatestock.Qunatity + ForKitchenOldItemQuantity;
						if (updatestock.Qunatity >= ForKitchenNewItemQuantity)
						{
							updatestock.Qunatity = updatestock.Qunatity - ForKitchenNewItemQuantity;
							con.Stock.Update(updatestock);
						}
						else if (updatestock.Qunatity < ForKitchenNewItemQuantity)
						{
							var itemname = await con.Item.Where(test => test.ItemCode == updatestock.ItemId).FirstOrDefaultAsync();
							if (itemname != null)
								return Results.NotFound("Stock Is Very Low For ItemName " + itemname.ItemName);
						}
					}
				}
			}
		}
	}
	var updatekitchenfood = imap.Map<KitchenFood>(k1);
	con.KitchenFood.Update(updatekitchenfood);
	await con.SaveChangesAsync();
	return Results.Ok(updatekitchenfood);
});

app.MapPut("/Sales", async (DataContext con, SalesDTO s1, IMapper imap) =>
{
	var foodid = await con.FoodMenu.Where(test => test.FoodName == s1.KitchenFoodName).FirstOrDefaultAsync();
	if (foodid != null)
	{
		var kitchenfoodid = await con.KitchenFood.Where(test => test.FoodID == foodid.FoodID).FirstOrDefaultAsync();
		if (kitchenfoodid != null)
			s1.KitchenFoodID = kitchenfoodid.KitchenFoodID;
		else
			return Results.NotFound("Kitchen Food Is Unavailable");
	}
	else
		return Results.NotFound("Food Is Unavailable");

	var oldsaleitem = await con.Sales.Where(test => test.SalesID == s1.SalesID).FirstOrDefaultAsync();
	if (oldsaleitem != null)
	{
		if (oldsaleitem.Quantity != s1.Quantity)
		{
			var kitchenfooditem = await con.KitchenFood.Where(test => test.KitchenFoodID == oldsaleitem.KitchenFoodID).FirstOrDefaultAsync();
			if (kitchenfooditem != null)
			{
				if (oldsaleitem.Quantity > s1.Quantity)
				{
					double d1 = (double)(oldsaleitem.Quantity - s1.Quantity);
					kitchenfooditem.QuantityPrepared = kitchenfooditem.QuantityPrepared + d1;
					con.KitchenFood.Update(kitchenfooditem);
				}
				else if (oldsaleitem.Quantity < s1.Quantity)
				{
					double d1 = (double)(s1.Quantity - oldsaleitem.Quantity);
					kitchenfooditem.QuantityPrepared = kitchenfooditem.QuantityPrepared - d1;
					con.KitchenFood.Update(kitchenfooditem);
				}

				var fooditemprice = await con.FoodMenu.Where(test => test.FoodID == kitchenfooditem.FoodID).FirstOrDefaultAsync();
				if (fooditemprice != null)
				{
					if (s1.Price != null && s1.Quantity != null && fooditemprice.Price != null)
					{
						s1.Price = s1.Quantity * fooditemprice.Price;
					}
				}
			}
		}
	}
	var updatesales = imap.Map<Sales>(s1);
	con.Sales.Update(updatesales);
	await con.SaveChangesAsync();
	return Results.Ok(updatesales);
});




app.MapDelete("/Item/HardDelete/{id:int}", async (DataContext con, int id) =>
{
	var result = await con.Item
	 .Where(test => test.ItemCode == id).FirstOrDefaultAsync();
	if (result == null)
		return Results.NotFound("Provide Valid ItemCode Pls");
	con.Item.Remove(result);
	var stockdel = await con.Stock.Where(test => test.ItemId == id).FirstOrDefaultAsync();
	if (stockdel != null)
		con.Stock.Remove(stockdel);
	var purchasedel = await con.Purchase.Where(test => test.ItemId == id).ToListAsync();
	foreach (var purchaseditem in purchasedel)
	{ con.Purchase.Remove(purchaseditem); }

	var mappingdel = await con.FoodMapping.Where(test => test.ItemId == id).ToListAsync();
	foreach (var mapitem in mappingdel)
	{
		con.FoodMapping.Remove(mapitem);
		var fooditemname = await con.FoodMenu.Where(test => test.FoodID == mapitem.FoodID).ToListAsync();
		foreach (var food in fooditemname)
		{
			var kitchenfood = await con.KitchenFood.Where(test => test.FoodID == food.FoodID).FirstOrDefaultAsync();
			if (kitchenfood != null)
			{
				con.KitchenFood.Remove(kitchenfood);
				var delsale = await con.Sales.Where(test => test.KitchenFoodID == kitchenfood.KitchenFoodID).ToListAsync();
				foreach (var sale in delsale)
				{
					con.Sales.Remove(sale);
				}
			}
		}
	}
	await con.SaveChangesAsync();

	return Results.NoContent();

});
app.MapDelete("/Stock/{id:int}", async (DataContext con, int id) =>
{
	var result = await con.Stock
	 .Where(test => test.StockID == id).FirstOrDefaultAsync();
	if (result == null)
		return Results.NotFound("Provide Valid Stock ID Pls");
	con.Stock.Remove(result);
	await con.SaveChangesAsync();
	return Results.NoContent();

});

app.MapDelete("/Supply/HardDelete/{id:int}", async (DataContext con, int id) =>
{
	var result = await con.Supply
	 .Where(test => test.SupplyID == id).FirstOrDefaultAsync();
	if (result == null)
		return Results.NotFound("Provide Valid Supply ID Pls");
	con.Supply.Remove(result);
	var purchasedel = await con.Purchase.Where(test => test.SupplyId == id).ToListAsync();
	foreach (var purchaseitem in purchasedel)
	{
		con.Purchase.Remove(purchaseitem);
		var Removestock = await con.Stock.Where(test => test.ItemId == purchaseitem.ItemId).FirstOrDefaultAsync();
		if (Removestock != null)
		{
			Removestock.Qunatity = Removestock.Qunatity - purchaseitem.Quantity;
			if (Removestock.Qunatity == 0)
			{
				con.Stock.Remove(Removestock);
			}
			else
			{
				con.Stock.Update(Removestock);
			}

		}
	}

	await con.SaveChangesAsync();
	return Results.NoContent();

});

app.MapDelete("/Purchase/{id:int}", async (DataContext con, int id) =>
{
	var result = await con.Purchase
	 .Where(test => test.PurchaseNo == id).FirstOrDefaultAsync();
	if (result == null)
		return Results.NotFound("Provide Valid Purchase No Pls");
	con.Purchase.Remove(result);
	var Removestock = await con.Stock.Where(test => test.ItemId == result.ItemId).FirstOrDefaultAsync();
	if (Removestock != null)
	{
		Removestock.Qunatity = Removestock.Qunatity - result.Quantity;
		if (Removestock.Qunatity == 0)
		{
			con.Stock.Remove(Removestock);
		}
		else
		{
			con.Stock.Update(Removestock);
		}

	}
	await con.SaveChangesAsync();
	return Results.NoContent();

});

app.MapDelete("/Unit/HardDelete/{id:int}", async (DataContext con, int id) =>
{
	var result = await con.Unit
	 .Where(test => test.ID == id).FirstOrDefaultAsync();
	if (result == null)
		return Results.NotFound("Provide Valid Unit ID Pls");
	con.Unit.Remove(result);
	var itemdel = await con.Item.Where(test => test.UnitId == id).ToListAsync();

	foreach (var item in itemdel)
	{
		con.Item.Remove(item);
		var stockdel = await con.Stock.Where(test => test.ItemId == item.ItemCode).FirstOrDefaultAsync();
		if (stockdel != null)
			con.Stock.Remove(stockdel);
		var purchasedel = await con.Purchase.Where(test => test.ItemId == item.ItemCode).ToListAsync();
		foreach (var purchaseditem in purchasedel)
		{ con.Purchase.Remove(purchaseditem); }

		var mappingdel = await con.FoodMapping.Where(test => test.ItemId == item.ItemCode).ToListAsync();
		foreach (var mapitem in mappingdel)
		{
			var fooditemname = await con.FoodMenu.Where(test => test.FoodID == mapitem.FoodID).ToListAsync();
			foreach (var food in fooditemname)
			{
				var kitchenfood = await con.KitchenFood.Where(test => test.FoodID == food.FoodID).FirstOrDefaultAsync();
				if (kitchenfood != null)
				{
					con.KitchenFood.Remove(kitchenfood);
					var delsale = await con.Sales.Where(test => test.KitchenFoodID == kitchenfood.KitchenFoodID).ToListAsync();
					foreach (var sale in delsale)
					{
						con.Sales.Remove(sale);
					}
				}
			}
		}
	}
	await con.SaveChangesAsync();
	return Results.NoContent();
});

app.MapDelete("/FoodMenu/HardDelete/{id:int}", async (DataContext con, int id) =>
{
	var result = await con.FoodMenu
	 .Where(test => test.FoodID == id).FirstOrDefaultAsync();
	if (result == null)
		return Results.NotFound("Provide Valid Food ID Pls");
	con.FoodMenu.Remove(result);
	var mappingdel = await con.FoodMapping.Where(test => test.FoodID == id).ToListAsync();
	foreach (var item in mappingdel)
	{
		con.FoodMapping.Remove(item);
	}
	var kitchenfoodel = await con.KitchenFood.Where(test => test.FoodID == id).FirstOrDefaultAsync();
	if (kitchenfoodel != null)
	{
		con.KitchenFood.Remove(kitchenfoodel);
		var findmapping = await con.FoodMapping.Where(test => test.FoodID == kitchenfoodel.FoodID)
				.Where(test => test.Active == true).ToListAsync();
		foreach (var MapItem in findmapping)
		{
			double ForOneItemQuantity = 1;
			double ForKitchenOldItemQuantity = 1;
			if (MapItem.ItemQuantity != null && MapItem.FoodQuantity != null && kitchenfoodel.QuantityPrepared != null)
			{
				ForOneItemQuantity = (double)(MapItem.ItemQuantity / MapItem.FoodQuantity);
				ForKitchenOldItemQuantity = (double)(ForOneItemQuantity * kitchenfoodel.QuantityPrepared);
			}
			var updatestock = await con.Stock.Where(test => test.ItemId == MapItem.ItemId).FirstOrDefaultAsync();
			if (updatestock != null)
			{
				if (updatestock.Qunatity != null)
				{
					updatestock.Qunatity = updatestock.Qunatity + ForKitchenOldItemQuantity;
					con.Stock.Update(updatestock);
				}
			}
		}
		var salesdel = await con.Sales.Where(test => test.KitchenFoodID == kitchenfoodel.KitchenFoodID).ToListAsync();
		foreach (var item in salesdel)
		{
			con.Sales.Remove(item);
		}
	}
	await con.SaveChangesAsync();
	return Results.NoContent();
});

app.MapDelete("/FoodMapping/{id:int}", async (DataContext con, int id) =>
{
	var result = await con.FoodMapping
	 .Where(test => test.MappingID == id).FirstOrDefaultAsync();
	if (result == null)
		return Results.NotFound("Provide Valid Food Mapping ID Pls");
	con.FoodMapping.Remove(result);
	await con.SaveChangesAsync();
	return Results.NoContent();

});

app.MapDelete("/KitchenFood/{id:int}", async (DataContext con, int id) =>
{
	var oldkitchenfood = await con.KitchenFood.Where(test => test.KitchenFoodID == id).FirstOrDefaultAsync();
	if (oldkitchenfood != null)
	{
		var findmapping = await con.FoodMapping.Where(test => test.FoodID == oldkitchenfood.FoodID)
				.Where(test => test.Active == true).ToListAsync();
		foreach (var MapItem in findmapping)
		{
			double ForOneItemQuantity = 1;
			double ForKitchenOldItemQuantity = 1;
			if (MapItem.ItemQuantity != null && MapItem.FoodQuantity != null && oldkitchenfood.QuantityPrepared != null)
			{
				ForOneItemQuantity = (double)(MapItem.ItemQuantity / MapItem.FoodQuantity);
				ForKitchenOldItemQuantity = (double)(ForOneItemQuantity * oldkitchenfood.QuantityPrepared);
			}
			var updatestock = await con.Stock.Where(test => test.ItemId == MapItem.ItemId).FirstOrDefaultAsync();
			if (updatestock != null)
			{
				if (updatestock.Qunatity != null)
				{
					updatestock.Qunatity = updatestock.Qunatity + ForKitchenOldItemQuantity;
					con.Stock.Update(updatestock);
				}
			}
		}
	}
	var result = await con.KitchenFood
	 .Where(test => test.KitchenFoodID == id).FirstOrDefaultAsync();
	if (result == null)
		return Results.NotFound("Provide Valid Kitchen Food ID Pls");
	con.KitchenFood.Remove(result);
	await con.SaveChangesAsync();
	return Results.NoContent();

});

app.MapDelete("/Sales/{id:int}", async (DataContext con, int id) =>
{
	var result = await con.Sales
	 .Where(test => test.SalesID == id).FirstOrDefaultAsync();
	if (result == null)
		return Results.NotFound("Provide Valid Sales ID Pls");
	con.Sales.Remove(result);
	var updatekitchenfood = await con.KitchenFood.Where(test => test.KitchenFoodID == result.KitchenFoodID).FirstOrDefaultAsync();
	if (updatekitchenfood != null)
	{
		updatekitchenfood.QuantityPrepared = updatekitchenfood.QuantityPrepared + result.Quantity;
		con.KitchenFood.Update(updatekitchenfood);
	}
	await con.SaveChangesAsync();
	return Results.NoContent();

});



app.MapDelete("/Item/{id:int}", async (DataContext con, int id) =>
{
	var result = await con.Item
	 .Where(test => test.ItemCode == id).FirstOrDefaultAsync();
	if (result == null)
		return Results.NotFound("Provide Valid Item ID Pls");
	result.IsActive = false;
	con.Item.Update(result);
	await con.SaveChangesAsync();
	return Results.Ok(result);

});

app.MapDelete("/Unit/{id:int}", async (DataContext con, int id) =>
{
	var result = await con.Unit
	 .Where(test => test.ID == id).FirstOrDefaultAsync();
	if (result == null)
		return Results.NotFound("Provide Valid Unit ID Pls");
	result.IsActive = false;
	con.Unit.Update(result);
	await con.SaveChangesAsync();
	return Results.Ok(result);

});

app.MapDelete("/Supply/{id:int}", async (DataContext con, int id) =>
{
	var result = await con.Supply
	 .Where(test => test.SupplyID == id).FirstOrDefaultAsync();
	if (result == null)
		return Results.NotFound("Provide Valid Supply ID Pls");
	result.IsActive = false;
	con.Supply.Update(result);
	await con.SaveChangesAsync();
	return Results.Ok(result);

});

app.MapDelete("/FoodMenu/{id:int}", async (DataContext con, int id) =>
{
	var result = await con.FoodMenu
	 .Where(test => test.FoodID == id).FirstOrDefaultAsync();
	if (result == null)
		return Results.NotFound("Provide Valid FoodMenu ID Pls");
	result.IsActive = false;
	con.FoodMenu.Update(result);
	await con.SaveChangesAsync();
	return Results.Ok(result);

});
app.Run();



public class Item
{
	public Item()
	{
		Purchase = new HashSet<Purchase>();
		FoodMapping = new HashSet<FoodMapping>();
	}
	[Key]
	public int ItemCode { get; set; }
	[StringLength(300, MinimumLength = 3)]
	public string ItemName { get; set; } = null!;
	[StringLength(int.MaxValue)]
	public string? Image { get; set; }
	public double ReorderLevel { get; set; }
	public bool IsActive { get; set; }
	[JsonIgnore]
	public virtual Unit? Unit { get; set; }
	public int? UnitId { get; set; }

	public virtual Stock? Stock { get; set; }
	public virtual ICollection<Purchase>? Purchase { get; set; }
	public virtual ICollection<FoodMapping>? FoodMapping { get; set; }
}

public class Supply
{
	public Supply()
	{
		Purchase = new HashSet<Purchase>();
	}
	public int SupplyID { get; set; }
	[StringLength(300, MinimumLength = 3)]
	public string SupplierName { get; set; } = null!;
	public bool IsActive { get; set; }
	public virtual ICollection<Purchase>? Purchase { get; set; }
}

public class Unit
{
	public Unit()
	{
		Item = new HashSet<Item>();
	}
	[Key]
	public int ID { get; set; }
	[StringLength(50, MinimumLength = 3)]
	public string UnitName { get; set; } = null!;
	public bool IsActive { get; set; }
	public virtual ICollection<Item>? Item { get; set; }

}

public class Stock
{
	[Key]
	public int StockID { get; set; }
	[JsonIgnore]
	public virtual Item? Item { get; set; }
	public int? ItemId { get; set; }
	public double? Qunatity { get; set; }

}
public class Purchase
{
	[Key]
	public int PurchaseNo { get; set; }
	public DateTime PurchasedDate { get; set; }
	[JsonIgnore]
	public virtual Item? Item { get; set; }
	public int? ItemId { get; set; }
	public double? Price { get; set; }
	public double? Quantity { get; set; }
	[JsonIgnore]
	public virtual Supply? Supply { get; set; }
	public int? SupplyId { get; set; }
	public double? PurchasedValue { get; set; }

}

public class FoodMenu
{
	public FoodMenu()
	{
		FoodMapping = new HashSet<FoodMapping>();
	}
	[Key]
	public int FoodID { get; set; }
	[StringLength(200, MinimumLength = 3)]
	public string FoodName { get; set; } = null!;
	public double? Price { get; set; }
	public bool IsActive { get; set; }
	public virtual ICollection<FoodMapping>? FoodMapping { get; set; }
	public virtual KitchenFood? KitchenFood { get; set; }
}

public class FoodMapping
{
	[Key]
	public int MappingID { get; set; }
	[JsonIgnore]
	public virtual FoodMenu? FoodMenu { get; set; }
	public int? FoodID { get; set; }
	public double? FoodQuantity { get; set; }
	[JsonIgnore]
	public virtual Item? Item { get; set; }
	public int? ItemId { get; set; }
	public double? ItemQuantity { get; set; }
	public bool Active { get; set; }

}

public class KitchenFood
{
	public KitchenFood()
	{
		Sales = new HashSet<Sales>();
	}
	[Key]
	public int KitchenFoodID { get; set; }
	[JsonIgnore]
	public virtual FoodMenu? FoodMenu { get; set; }
	public int? FoodID { get; set; }
	public double? QuantityPrepared { get; set; }
	public DateTime PreparedDate { get; set; }
	public virtual ICollection<Sales>? Sales { get; set; }
}

public class Sales
{
	[Key]
	public int SalesID { get; set; }
	[StringLength(200, MinimumLength = 3)]
	public string CustomerName { get; set; } = null!;
	public string? CustomerType { get; set; }
	[JsonIgnore]
	public virtual KitchenFood? KitchenFood { get; set; }
	public int? KitchenFoodID { get; set; }
	public double? Quantity { get; set; }
	public double? Price { get; set; }
	public double? Cash { get; set; } = 0;
	[StringLength(maximumLength: 4, MinimumLength = 4)]
	[RegularExpression("^[0 - 9]{4} $")]
	public int? CreditCardNo { get; set; } = 0000;
	public double? Credit { get; set; } = 0;
	public double? UPI { get; set; } = 0;


}


//DTO Classes
public class ItemDTO
{
	public int ItemCode { get; set; }
	[StringLength(300, MinimumLength = 3)]
	public string ItemName { get; set; } = null!;
	[StringLength(int.MaxValue)]
	public string? Image { get; set; }
	public double ReorderLevel { get; set; }
	public string? UnitName { get; set; }
	//[JsonIgnore]
	public int? UnitId { get; set; }
	public bool IsActive { get; set; } = true;
}
public class UnitDTO
{
	public int ID { get; set; }
	[StringLength(50, MinimumLength = 3)]
	public string UnitName { get; set; } = null!;
	public bool IsActive { get; set; } = true;
	//[JsonIgnore]
	public virtual ICollection<Item>? Item { get; set; }
}

public class StockDTO
{
	public int StockID { get; set; }
	public string? ItemName { get; set; }
	//[JsonIgnore]
	public int? ItemId { get; set; }
	public double? Qunatity { get; set; }
}

public class SupplyDTO
{
	public int SupplyID { get; set; }
	[StringLength(300, MinimumLength = 3)]
	public string SupplierName { get; set; } = null!;
	public bool IsActive { get; set; } = true;
}

public class PurchaseDTO
{
	public int PurchaseNo { get; set; }
	public DateTime PurchasedDate { get; set; }
	public string? ItemName { get; set; }
	//[JsonIgnore]
	public int? ItemId { get; set; }
	public double? Price { get; set; }
	public double? Quantity { get; set; }
	public string? suppliermame { get; set; }
	//[JsonIgnore]
	public int? SupplyId { get; set; }
	//[JsonIgnore]
	public double? PurchasedValue { get; set; }
}

public class FoodMenuDTO
{
	public int FoodID { get; set; }
	[StringLength(200, MinimumLength = 3)]
	public string FoodName { get; set; } = null!;
	public double? Price { get; set; }
	public bool IsActive { get; set; } = true;
	//[JsonIgnore]
	public virtual ICollection<FoodMapping>? FoodMapping { get; set; }
}

public class FoodMapppingDTO
{
	public int MappingID { get; set; }
	public string? FoodName { get; set; }
	//[JsonIgnore]
	public int? FoodID { get; set; }
	public double? FoodQuantity { get; set; }
	public string? ItemName { get; set; }
	//[JsonIgnore]
	public int? ItemId { get; set; }
	public double? ItemQuantity { get; set; }
	public bool Active { get; set; }
}

public class KitchenFoodDTO
{
	public int KitchenFoodID { get; set; }
	public string? FoodName { get; set; }
	//[JsonIgnore]
	public int? FoodID { get; set; }
	public double? QuantityPrepared { get; set; }
	public DateTime PreparedDate { get; set; }
	//[JsonIgnore]
	public virtual ICollection<Sales>? Sales { get; set; }
}
public class SalesDTO
{
	public int SalesID { get; set; }
	[StringLength(200, MinimumLength = 3)]
	public string CustomerName { get; set; } = null!;
	public string? CustomerType { get; set; }
	public string? KitchenFoodName { get; set; }
	//[JsonIgnore]
	public int? KitchenFoodID { get; set; }
	public double? Quantity { get; set; }
	//[JsonIgnore]
	public double? Price { get; set; } = 0;
	public double? Cash { get; set; } = 0;
	// [StringLength(4)]
	[StringLength(maximumLength: 4, MinimumLength = 4)]
	[RegularExpression("^[0-9]+$")]
	public int? CreditCardNo { get; set; } = 1234;
	public double? Credit { get; set; } = 0;
	public double? UPI { get; set; } = 0;
}


//public class FoodMapppingAddMoreDTO
//{
//	[JsonIgnore]
//	public int MappingID { get; set; }

//	public string? FoodName { get; set; }
//	[JsonIgnore]
//	public int? FoodID { get; set; }
//	[JsonIgnore]
//	public double? FoodQuantity { get; set; }
//	public string? ItemName { get; set; }
//	[JsonIgnore]
//	public int? ItemId { get; set; }
//	public double? ItemQuantity { get; set; }
//	[JsonIgnore]
//	public bool Active { get; set; }
//}








