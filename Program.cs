using EstimateResolve.DataAccess;

using (var context = new EstimateResolveDbContext())
{
    context.Database.EnsureCreated();
    context.SaveChanges();
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
