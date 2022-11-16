using EstimateResolve.DataAccess;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using (var context = new EstimateResolveDbContext())
{
    var dataSeedingService = new DataSeedingService(context);
    dataSeedingService.SeedDatabase();
}

/*var repository = new UnitOfMeasurementRepository();
var unitOfMeasurements = await repository.GetAll();

await repository.Update(1, new UnitOfMeasurement
{
    Id = 1, 
    Name = "м2"
});

var stringResult = unitOfMeasurements
    .Select(u => $"id: {u.Id} name: {u.Name}");

Console.WriteLine(string.Join("\n", stringResult));
Console.ReadKey();
*/


//var repository = new MaterialRepository();
