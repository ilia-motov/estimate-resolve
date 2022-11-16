using EstimateResolve.Entities;
using EstimateResolve.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EstimateResolve.DataAccess
{
    public interface IDataSeedingService
    {
        void SeedDatabase();
    }

    public class DataSeedingService : IDataSeedingService
    {
        private readonly EstimateResolveDbContext _context;

        public DataSeedingService(EstimateResolveDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public void SeedDatabase()
        {
            if (!_context.Database.EnsureCreated())
                return;

            var clients = new List<Client>
            {
                new Client {Id = 1, Name = "Спар"},
                new Client {Id = 2, Name = "ФизКульт"},
                new Client {Id = 3, Name = "Режим"},
                new Client {Id = 4, Name = "Магнит"}
            };

            _context.Clients.AddRange(clients);
            _context.SaveChanges();

            var unitOfMeasurements = new List<UnitOfMeasurement>
            {
                new UnitOfMeasurement {Id = 1, Name = "м2"},
                new UnitOfMeasurement {Id = 2, Name = "м3"},
                new UnitOfMeasurement {Id = 3, Name = "мп"},
                new UnitOfMeasurement {Id = 4, Name = "шт"},
                new UnitOfMeasurement {Id = 5, Name = "кг"},
                new UnitOfMeasurement {Id = 6, Name = "чел/день"},
                new UnitOfMeasurement {Id = 7, Name = "маш/смен"}
            };

            _context.UnitOfMeasurements.AddRange(unitOfMeasurements);
            _context.SaveChanges();

            var constructionObjects = new List<ConstructionObject>
            {
                new ConstructionObject
                {
                    Id = 1,
                    ClientId = 1,
                    Name = "Продуктовый супермаркет на 2000 м2 г.Москва, Ленинский проспект 101/1"
                },
                new ConstructionObject
                {
                    Id = 2,ClientId = 3,
                    Name = "Реконструкция спортивного клуба г.Нижний Новгород, пр.Героев д.27"
                },
            };

            _context.ConstructionObjects.AddRange(constructionObjects);
            _context.SaveChanges();

            var contracts = new List<Contract>
            {
                new Contract { Id = 1, Name = "1477 от 22.05.21", ClientId = 1, ConstructionObjectId = 1 },
                new Contract { Id = 2, Name = "1477 от 22.05.21", ClientId = 3, ConstructionObjectId = 2 },
            };

            _context.Contracts.AddRange(contracts);
            _context.SaveChanges();

            var estimates = new List<Estimate>
            {
                new Estimate
                {
                    Id = 1,
                    Number = 1,
                    Name = "Отделочные работы по торговому залу",
                    DevelopmentDate = new DateTime(2021, 05, 22),
                    ConstructionObjectId = 1,
                    ClientId = 1,
                    ContractId = 1
                },
                new Estimate
                {
                    Id = 2,
                    Number = 1,
                    Name = "Работы по входной группе",
                    DevelopmentDate = new DateTime(2021, 01, 12),
                    ConstructionObjectId = 2,
                    ClientId = 3,
                    ContractId = 2
                },
                new Estimate
                {
                    Id = 3,
                    Number = 2,
                    Name = "Отделка санузлов",
                    DevelopmentDate = new DateTime(2022, 02, 02),
                    ConstructionObjectId = 2,
                    ClientId = 3,
                    ContractId = 2
                },
            };

            _context.Estimates.AddRange(estimates);
            _context.SaveChanges();

            var companyServices = new List<CompanyService>
            {
                new CompanyService
                {
                    Id = 1,
                    Name = "Штукатурка гипсовыми смесями вручную",
                    UnitOfMeasurementId = 1,
                    Price = 250m,
                },
                new CompanyService
                {
                    Id = 2,
                    Name = "Штукатурка цементными смесями вручную",
                    UnitOfMeasurementId = 1,
                    Price = 250m,
                },
                new CompanyService
                {
                    Id = 3,
                    Name = "Штукатурка смесями вручную (мп)",
                    UnitOfMeasurementId = 3,
                    Price = 180m,
                },
                new CompanyService
                {
                    Id = 4,
                    Name = "Шпатлевка потолка",
                    UnitOfMeasurementId = 1,
                    Price = 180m,
                },
                new CompanyService
                {
                    Id = 5,
                    Name = "Шпатлевка стен",
                    UnitOfMeasurementId = 1,
                    Price = 150m,
                },
                new CompanyService
                {
                    Id = 6,
                    Name = "Шпатлевка (мп)",
                    UnitOfMeasurementId = 3,
                    Price = 150m,
                },
                new CompanyService
                {
                    Id = 7,
                    Name = "Краска стен в 2 слоя",
                    UnitOfMeasurementId = 1,
                    Price = 120m,
                },
                new CompanyService
                {
                    Id = 8,
                    Name = "Краска потолка в 2 слоя",
                    UnitOfMeasurementId = 1,
                    Price = 150m,
                },
                new CompanyService
                {
                    Id = 9,
                    Name = "Краска в 2 слоя (мп)",
                    UnitOfMeasurementId = 3,
                    Price = 150m,
                },
                new CompanyService
                {
                    Id = 10,
                    Name = "Укладка напольного керамогранита S плитки до 0,35м2",
                    UnitOfMeasurementId = 1,
                    Price = 800m,
                },
                new CompanyService
                {
                    Id = 11,
                    Name = "Укладка напольного керамогранита S плитки свыше 0,35м2",
                    UnitOfMeasurementId = 1,
                    Price = 1200m,
                },
                new CompanyService
                {
                    Id = 12,
                    Name = "Укладка настенной плитки S плитки до 0,35м2",
                    UnitOfMeasurementId = 1,
                    Price = 700m,
                },
                new CompanyService
                {
                    Id = 13,
                    Name = "Укладка настенной плитки S плитки свыше 0,35м2",
                    UnitOfMeasurementId = 1,
                    Price = 1300m,
                },
                new CompanyService
                {
                    Id = 14,
                    Name = "Грунтовка поверхности",
                    UnitOfMeasurementId = 1,
                    Price = 20m,
                },
                new CompanyService
                {
                    Id = 15,
                    Name = "Грунтовка поверхности Бетоноконтактом",
                    UnitOfMeasurementId = 1,
                    Price = 30m,
                },
                new CompanyService
                {
                    Id = 16,
                    Name = "Укладка калошницы",
                    UnitOfMeasurementId = 3,
                    Price = 80m,
                },
                new CompanyService
                {
                    Id = 17,
                    Name = "Монтаж перегородок из ГКЛ на мк в 2 слоя с утеплителем шаг стоек 400мм",
                    UnitOfMeasurementId = 1,
                    Price = 600m,
                },
                new CompanyService
                {
                    Id = 18,
                    Name = "Монтаж перегородок из ГКЛ/ГКЛв на мк в 2 слоя с утеплителем шаг стоек 600мм",
                    UnitOfMeasurementId = 1,
                    Price = 500m,
                },
                new CompanyService
                {
                    Id = 19,
                    Name = "Монтаж перегородок из ГВЛ/ГВЛв на мк в 2 слоя с утеплителем шаг стоек 400мм",
                    UnitOfMeasurementId = 1,
                    Price = 800m,
                },
                new CompanyService
                {
                    Id = 20,
                    Name = "Монтаж перегородок из ГВЛ/ГВЛв на мк в 2 слоя с утеплителем шаг стоек 600мм",
                    UnitOfMeasurementId = 1,
                    Price = 650m,
                },
                new CompanyService
                {
                    Id = 21,
                    Name = "Монтаж стальных конструкций",
                    UnitOfMeasurementId = 5,
                    Price = 50m,
                },
                new CompanyService
                {
                    Id = 22,
                    Name = "Армирование конструкций",
                    UnitOfMeasurementId = 5,
                    Price = 30m,
                },
                new CompanyService
                {
                    Id = 23,
                    Name = "Бетонирование объемных конструкций",
                    UnitOfMeasurementId = 2,
                    Price = 3000m,
                },
                new CompanyService
                {
                    Id = 24,
                    Name = "Бетонирование погонных конструкций",
                    UnitOfMeasurementId = 3,
                    Price = 1700m,
                },
                new CompanyService
                {
                    Id = 25,
                    Name = "Работа по окладу",
                    UnitOfMeasurementId = 6,
                    Price = 2500m,
                },
                new CompanyService
                {
                    Id = 26,
                    Name = "Разгрузка материалов",
                    UnitOfMeasurementId = 5,
                    Price = 1.6m,
                },
                new CompanyService
                {
                    Id = 27,
                    Name = "Подъем материалов",
                    UnitOfMeasurementId = 5,
                    Price = 1.2m,
                },
                new CompanyService
                {
                    Id = 28,
                    Name = "Вывоз мусора",
                    UnitOfMeasurementId = 2,
                    Price = 100m,
                },
                new CompanyService
                {
                    Id = 29,
                    Name = "Монтаж раковины с подключением",
                    UnitOfMeasurementId = 4,
                    Price = 2000m,
                },
                new CompanyService
                {
                    Id = 30,
                    Name = "Монтаж унитаза с подключением",
                    UnitOfMeasurementId = 4,
                    Price = 3000m,
                },
                new CompanyService
                {
                    Id = 31,
                    Name = "Аренда вышки туры",
                    UnitOfMeasurementId = 7,
                    Price = 550m,
                },
            };

            _context.CompanyServices.AddRange(companyServices);
            _context.SaveChanges();

            var materials = new List<Material>
            {
                new Material
                {
                    Id= 1,
                    Name = "Штукатурка гипсовая Бергауф",
                    UnitsRev = "кг",
                    Price = 4.0m
                },
                new Material
                {
                    Id= 2,
                    Name = "Штукатурка цементная Бергауф",
                    UnitsRev = "кг",
                    Price = 2.7m
                },
                new Material
                {
                    Id= 3,
                    Name = "Профиль штукатурный маячковый 10мм 3мп",
                    UnitsRev = "мп",
                    Price = 12.0m
                },
                new Material
                {
                    Id= 4,
                    Name = "Шпатлевка Бергауф",
                    UnitsRev = "кг",
                    Price = 19.0m
                },
                new Material
                {
                    Id= 5,
                    Name = "Краска вододисперсионная  Маршал Маэстро колерованная",
                    UnitsRev = "кг",
                    Price = 162.80m
                },
                new Material
                {
                    Id= 6,
                    Name = "Грунтовка Бергауф Тифенгрунт",
                    UnitsRev = "л",
                    Price = 46.0m
                },
                new Material
                {
                    Id= 7,
                    Name = "Грунтовка ЕК200",
                    UnitsRev = "л",
                    Price = 52.0m
                },
                new Material
                {
                    Id= 8,
                    Name = "Грунтовка Бетоноконтакт",
                    UnitsRev = "кг",
                    Price = 120.0m
                },
                new Material
                {
                    Id= 9,
                    Name = "Плиточный клей Бергауф",
                    UnitsRev = "кг",
                    Price = 12.0m
                },
                new Material
                {
                    Id= 10,
                    Name = "Плиточный клей Церезит",
                    UnitsRev = "кг",
                    Price = 15.0m
                },
                new Material
                {
                    Id= 11,
                    Name = "Затирка Церезит СЕ33 (серая)",
                    UnitsRev = "кг",
                    Price = 110.0m
                },
                new Material
                {
                    Id= 12,
                    Name = "Крестики для плитки (150шт)",
                    UnitsRev = "уп",
                    Price = 37.0m
                },
                new Material
                {
                    Id= 13,
                    Name = "Керамогранит 300х300 Шахтинская",
                    UnitsRev = "м2",
                    Price = 890.0m
                },
                new Material
                {
                    Id= 14,
                    Name = "Керамогранит 600х600 Эстима",
                    UnitsRev = "м2",
                    Price = 1200.0m
                },
                new Material
                {
                    Id= 15,
                    Name = "Керамическая плитка 200х300",
                    UnitsRev = "м2",
                    Price = 650.0m
                },
                new Material
                {
                    Id= 16,
                    Name = "ПН 50х40 (3м) Кнауф 0,6мм",
                    UnitsRev = "м",
                    Price = 111.0m
                },
                new Material
                {
                    Id= 17,
                    Name = "ПС 50х50 (3м)Кнауф 0,6мм",
                    UnitsRev = "м",
                    Price = 114.0m
                },
                new Material
                {
                    Id= 18,
                    Name = "Лента звукоизол. Дихтунгсбанд (70мм х 30м)",
                    UnitsRev = "м",
                    Price = 7.70m
                },
                new Material
                {
                    Id= 19,
                    Name = "Дюбель 6/40 гриб с шурупом (штучно)",
                    UnitsRev = "шт",
                    Price = 0.85m
                },
                new Material
                {
                    Id= 20,
                    Name = "Утеплитель Изовер  т 50 мм",
                    UnitsRev = "м2",
                    Price = 88m
                },
                new Material
                {
                    Id= 21,
                    Name = "ГКЛВ (1,2*2,5) 12,5 мм",
                    UnitsRev = "м2",
                    Price = 135m
                },
                new Material
                {
                    Id= 22,
                    Name = "ГКЛ (1,2*2,5) 12,5 мм",
                    UnitsRev = "м2",
                    Price = 127m
                },
                new Material
                {
                    Id= 23,
                    Name = "Саморезы для ГВЛ 25 мм",
                    UnitsRev = "шт",
                    Price = 0.33m
                },
                new Material
                {
                    Id= 24,
                    Name = "Саморезы для ГВЛ 35  мм",
                    UnitsRev = "шт",
                    Price = 0.76m
                },
                new Material
                {
                    Id= 25,
                    Name = "Шпатлевка Фугенфюллер",
                    UnitsRev = "кг",
                    Price = 25m
                },
                new Material
                {
                    Id= 26,
                    Name = "Лента разделительная 50мм",
                    UnitsRev = "м",
                    Price = 5.50m
                },
                new Material
                {
                    Id= 27,
                    Name = "Лента армирующая",
                    UnitsRev = "м",
                    Price = 2.80m
                },
                new Material
                {
                    Id= 28,
                    Name = "ГВЛВ (1,2*2,5) 12,5 мм",
                    UnitsRev = "м2",
                    Price = 215m
                },
                new Material
                {
                    Id= 29,
                    Name = "ГВЛ (1,2*2,5) 12,5 мм",
                    UnitsRev = "м2",
                    Price = 203m
                },
                new Material
                {
                    Id= 30,
                    Name = "Электроды",
                    UnitsRev = "кг",
                    Price = 215m
                },
                new Material
                {
                    Id= 31,
                    Name = "Стальные конструкции",
                    UnitsRev = "кг",
                    Price = 75.60m
                },
                new Material
                {
                    Id= 32,
                    Name = "Арматура",
                    UnitsRev = "кг",
                    Price = 72.0m
                },
                new Material
                {
                    Id= 33,
                    Name = "Бетон В20",
                    UnitsRev = "м3",
                    Price = 4700.0m
                },
                new Material
                {
                    Id= 34,
                    Name = "Бетон В25",
                    UnitsRev = "м3",
                    Price = 5100.0m
                },
                new Material
                {
                    Id= 35,
                    Name = "Грунтовка ГФ-021 серая",
                    UnitsRev = "кг",
                    Price = 163.0m
                },
                new Material
                {
                    Id= 36,
                    Name = "Эмаль ПФ-115 серая",
                    UnitsRev = "кг",
                    Price = 262.0m
                },

            };

            _context.Materials.AddRange(materials);
            _context.SaveChanges();

            var estimateWorks = new List<EstimateWork>
            {
                new EstimateWork
                {
                    Id = 1,
                    EstimateId = 1,
                    CompanyServiceId = 1,
                    Value = 120.5f,
                    Remark = "толщиной до 30мм в осях 1-6/А-В",
                    Price = 250.0m,
                    Amount = 30125.0m
                },
                new EstimateWork
                {
                    Id = 2,
                    EstimateId = 1,
                    CompanyServiceId = 14,
                    Value = 120.5f,
                    Remark = "в осях 1-6/А-В",
                    Price = 20.0m,
                    Amount = 2410.0m
                },
                new EstimateWork
                {
                    Id = 3,
                    EstimateId = 1,
                    CompanyServiceId = 5,
                    Value = 120.5f,
                    Remark = "в 2 слоя в осях 1-6/А-В",
                    Price = 150.0m,
                    Amount = 18075.0m
                },
                new EstimateWork
                {
                    Id = 4,
                    EstimateId = 1,
                    CompanyServiceId = 14,
                    Value = 120.5f,
                    Remark = "в осях 1-6/А-В",
                    Price = 20.0m,
                    Amount = 2410.0m
                },
                new EstimateWork
                {
                    Id = 5,
                    EstimateId = 1,
                    CompanyServiceId = 7,
                    Value = 120.5f,
                    Remark = "в 2 слоя в осях 1-6/А-В\r\n",
                    Price = 120.0m,
                    Amount = 14460.0m
                },
                new EstimateWork
                {
                    Id = 6,
                    EstimateId = 1,
                    CompanyServiceId = 14,
                    Value = 42.7f,
                    Remark = "в осях 1-6/А-В",
                    Price = 20.0m,
                    Amount = 854.0m
                },
                new EstimateWork
                {
                    Id = 7,
                    EstimateId = 1,
                    CompanyServiceId = 8,
                    Value = 42.7f,
                    Remark = "в 2 слоя в осях 1-6/А-В",
                    Price = 150.0m,
                    Amount = 6405.0m
                },
                new EstimateWork
                {
                    Id = 8,
                    EstimateId = 1,
                    CompanyServiceId = 26,
                    Value = 7053.0f,
                    Remark = " ",
                    Price = 1.6m,
                    Amount = 11284.8m
                },
                new EstimateWork
                {
                    Id = 9,
                    EstimateId = 1,
                    CompanyServiceId = 27,
                    Value = 7053.0f,
                    Remark = " ",
                    Price = 1.2m,
                    Amount = 8463.60m
                },
                new EstimateWork
                {
                    Id = 10,
                    EstimateId = 2,
                    CompanyServiceId = 21,
                    Value = 722.43f,
                    Remark = "Входная группа в осях 8-9/А",
                    Price = 50.0m,
                    Amount = 36121.50m
                },
                new EstimateWork
                {
                    Id = 11,
                    EstimateId = 2,
                    CompanyServiceId = 7,
                    Value = 14.93f,
                    Remark = "Входная группа в осях 8-9/А",
                    Price = 120.0m,
                    Amount = 1791.60m
                },
                new EstimateWork
                {
                    Id = 12,
                    EstimateId = 2,
                    CompanyServiceId = 31,
                    Value = 3.0f,
                    Remark = " ",
                    Price = 550.0m,
                    Amount = 1650.0m
                },
                new EstimateWork
                {
                    Id = 13,
                    EstimateId = 2,
                    CompanyServiceId = 23,
                    Value = 2.78f,
                    Remark = "Входная группа в осях 8-9/А",
                    Price = 3000.0m,
                    Amount = 8340.0m
                },
                new EstimateWork
                {
                    Id = 14,
                    EstimateId = 2,
                    CompanyServiceId = 26,
                    Value = 722.0f,
                    Remark = " ",
                    Price = 1.60m,
                    Amount = 1155.20m
                },
                new EstimateWork
                {
                    Id = 15,
                    EstimateId = 2,
                    CompanyServiceId = 27,
                    Value = 722.0f,
                    Remark = " ",
                    Price = 1.20m,
                    Amount = 866.40m
                },
                new EstimateWork
                {
                    Id = 16,
                    EstimateId = 3,
                    CompanyServiceId = 17,
                    Value = 18.0f,
                    Remark = "санузел в осях 4-5/Г-Д",
                    Price = 600.0m,
                    Amount = 10800.0m
                },
                new EstimateWork
                {
                    Id = 17,
                    EstimateId = 3,
                    CompanyServiceId = 14,
                    Value = 45.0f,
                    Remark = "санузел в осях 4-5/Г-Д",
                    Price = 20.0m,
                    Amount = 900.0m
                },
                new EstimateWork
                {
                    Id = 18,
                    EstimateId = 3,
                    CompanyServiceId = 11,
                    Value = 45.0f,
                    Remark = "санузел в осях 4-5/Г-Д",
                    Price = 1200.0m,
                    Amount = 54000.0m
                },
                new EstimateWork
                {
                    Id = 19,
                    EstimateId = 3,
                    CompanyServiceId = 14,
                    Value = 98.0f,
                    Remark = "санузел в осях 4-5/Г-Д",
                    Price = 20.0m,
                    Amount = 1960.0m
                },
                new EstimateWork
                {
                    Id = 20,
                    EstimateId = 3,
                    CompanyServiceId = 12,
                    Value = 98.0f,
                    Remark = "санузел в осях 4-5/Г-Д",
                    Price = 700.0m,
                    Amount = 68600.0m
                },
                new EstimateWork
                {
                    Id = 21,
                    EstimateId = 3,
                    CompanyServiceId = 29,
                    Value = 2.0f,
                    Remark = "санузел в осях 4-5/Г-Д",
                    Price = 2000.0m,
                    Amount = 4000.0m
                },
                new EstimateWork
                {
                    Id = 22,
                    EstimateId = 3,
                    CompanyServiceId = 30,
                    Value = 4.0f,
                    Remark = "санузел в осях 4-5/Г-Д",
                    Price = 3000.0m,
                    Amount = 12000.0m
                },
                new EstimateWork
                {
                    Id = 23,
                    EstimateId = 3,
                    CompanyServiceId = 26,
                    Value = 2900.0f,
                    Remark = " ",
                    Price = 1.6m,
                    Amount = 4640.0m
                },
                new EstimateWork
                {
                    Id = 24,
                    EstimateId = 3,
                    CompanyServiceId = 27,
                    Value = 2900.0f,
                    Remark = " ",
                    Price = 1.2m,
                    Amount = 3480.0m
                },
            };

            _context.EstimateWorks.AddRange(estimateWorks);
            _context.SaveChanges();

            var estimateMaterials = new List<EstimateMaterial>
            {
                new EstimateMaterial
                {
                    Id = 1,
                    EstimateId = 1,
                    EstimateWorkId = 1,
                    MaterialId = 1,
                    Consumption = 40.0f,
                    ValueWorking = 120.5f,
                    Quantity = 4820.0f,
                    Price = 4.0m,
                    Amount = 192800.0m
                },
                new EstimateMaterial
                {
                    Id = 2,
                    EstimateId = 1,
                    EstimateWorkId = 1,
                    MaterialId = 3,
                    Consumption = 0.5f,
                    ValueWorking = 120.5f,
                    Quantity = 60.25f,
                    Price = 12.0m,
                    Amount = 723.0m
                },
                new EstimateMaterial
                {
                    Id = 3,
                    EstimateId = 1,
                    EstimateWorkId = 2,
                    MaterialId = 6,
                    Consumption = 0.08f,
                    ValueWorking = 120.5f,
                    Quantity = 9.64f,
                    Price = 46.0m,
                    Amount = 443.44m
                },
                new EstimateMaterial
                {
                    Id = 4,
                    EstimateId = 1,
                    EstimateWorkId = 3,
                    MaterialId = 4,
                    Consumption = 2.4f,
                    ValueWorking = 120.5f,
                    Quantity = 289.2f,
                    Price = 19.0m,
                    Amount = 5494.80m
                },
                new EstimateMaterial
                {
                    Id = 5,
                    EstimateId = 1,
                    EstimateWorkId = 4,
                    MaterialId = 6,
                    Consumption = 0.08f,
                    ValueWorking = 125.50f,
                    Quantity = 9.64f,
                    Price = 46.0m,
                    Amount = 443.44m
                },
                new EstimateMaterial
                {
                    Id = 6,
                    EstimateId = 1,
                    EstimateWorkId = 5,
                    MaterialId = 5,
                    Consumption = 0.28f,
                    ValueWorking = 120.5f,
                    Quantity = 33.74f,
                    Price = 162.8m,
                    Amount = 5492.87m
                },
                new EstimateMaterial
                {
                    Id = 7,
                    EstimateId = 1,
                    EstimateWorkId = 6,
                    MaterialId = 6,
                    Consumption = 0.08f,
                    ValueWorking = 42.7f,
                    Quantity = 3.42f,
                    Price = 46.0m,
                    Amount = 157.14m
                },
                new EstimateMaterial
                {
                    Id = 8,
                    EstimateId = 1,
                    EstimateWorkId = 7,
                    MaterialId = 5,
                    Consumption = 0.28f,
                    ValueWorking = 42.7f,
                    Quantity = 11.96f,
                    Price = 162.8m,
                    Amount = 1946.44m
                },
                new EstimateMaterial
                {
                    Id = 9,
                    EstimateId = 2,
                    EstimateWorkId = 10,
                    MaterialId = 31,
                    Consumption = 1.0f,
                    ValueWorking = 722.43f,
                    Quantity = 722.43f,
                    Price = 75.6m,
                    Amount = 56615.71m
                },
                new EstimateMaterial
                {
                    Id = 10,
                    EstimateId = 2,
                    EstimateWorkId = 11,
                    MaterialId = 35,
                    Consumption = 0.12f,
                    ValueWorking = 14.93f,
                    Quantity = 1.79f,
                    Price = 163.0m,
                    Amount = 292.03m
                },
                new EstimateMaterial
                {
                    Id = 11,
                    EstimateId = 2,
                    EstimateWorkId = 11,
                    MaterialId = 36,
                    Consumption = 0.5f,
                    ValueWorking = 14.93f,
                    Quantity = 7.47f,
                    Price = 262.0m,
                    Amount = 1957.14m
                },
                new EstimateMaterial
                {
                    Id = 12,
                    EstimateId = 2,
                    EstimateWorkId = 13,
                    MaterialId = 33,
                    Consumption = 1.05f,
                    ValueWorking = 2.78f,
                    Quantity = 2.92f,
                    Price = 4700.0m,
                    Amount = 13719.3m
                },
                new EstimateMaterial
                {
                    Id = 13,
                    EstimateId = 3,
                    EstimateWorkId = 16,
                    MaterialId = 16,
                    Consumption = 1.57f,
                    ValueWorking = 18.0f,
                    Quantity = 28.27f,
                    Price = 111.0m,
                    Amount = 3137.66m
                },
                new EstimateMaterial
                {
                    Id = 14,
                    EstimateId = 3,
                    EstimateWorkId = 16,
                    MaterialId = 17,
                    Consumption = 4.23f,
                    ValueWorking = 18.0f,
                    Quantity = 76.19f,
                    Price = 114.0m,
                    Amount = 86857.71m
                },
                new EstimateMaterial
                {
                    Id = 15,
                    EstimateId = 3,
                    EstimateWorkId = 16,
                    MaterialId = 18,
                    Consumption = 2.62f,
                    ValueWorking = 18.0f,
                    Quantity = 47.17f,
                    Price = 7.70m,
                    Amount = 363.24m
                },
                new EstimateMaterial
                {
                    Id = 16,
                    EstimateId = 3,
                    EstimateWorkId = 16,
                    MaterialId = 19,
                    Consumption = 3.52f,
                    ValueWorking = 18.0f,
                    Quantity = 63.27f,
                    Price = 0.85m,
                    Amount = 53.78m
                },
                new EstimateMaterial
                {
                    Id = 17,
                    EstimateId = 3,
                    EstimateWorkId = 16,
                    MaterialId = 20,
                    Consumption = 1.07f,
                    ValueWorking = 18.0f,
                    Quantity = 19.28f,
                    Price = 88.0m,
                    Amount = 1696.78m
                },
                new EstimateMaterial
                {
                    Id = 18,
                    EstimateId = 3,
                    EstimateWorkId = 16,
                    MaterialId = 21,
                    Consumption = 4.45f,
                    ValueWorking = 18.0f,
                    Quantity = 80.12f,
                    Price = 135.0m,
                    Amount = 10816.42m
                },
                new EstimateMaterial
                {
                    Id = 19,
                    EstimateId = 3,
                    EstimateWorkId = 16,
                    MaterialId = 23,
                    Consumption = 18.76f,
                    ValueWorking = 18.0f,
                    Quantity = 337.71f,
                    Price = 0.33m,
                    Amount = 111.44m
                },
                new EstimateMaterial
                {
                    Id = 20,
                    EstimateId = 3,
                    EstimateWorkId = 16,
                    MaterialId = 24,
                    Consumption = 36.74f,
                    ValueWorking = 18.0f,
                    Quantity = 661.38f,
                    Price = 0.76m,
                    Amount = 502.65m
                },
                new EstimateMaterial
                {
                    Id = 21,
                    EstimateId = 3,
                    EstimateWorkId = 16,
                    MaterialId = 25,
                    Consumption = 1.58f,
                    ValueWorking = 18.0f,
                    Quantity = 28.45f,
                    Price = 25.0m,
                    Amount = 711.36m
                },
                new EstimateMaterial
                {
                    Id = 22,
                    EstimateId = 3,
                    EstimateWorkId = 16,
                    MaterialId = 26,
                    Consumption = 0.92f,
                    ValueWorking = 18.0f,
                    Quantity = 16.47f,
                    Price = 5.50m,
                    Amount = 90.60m
                },
                new EstimateMaterial
                {
                    Id = 23,
                    EstimateId = 3,
                    EstimateWorkId = 16,
                    MaterialId = 27,
                    Consumption = 1.58f,
                    ValueWorking = 18.0f,
                    Quantity = 28.45f,
                    Price = 2.80m,
                    Amount = 76.67m
                },
                new EstimateMaterial
                {
                    Id = 24,
                    EstimateId = 3,
                    EstimateWorkId = 17,
                    MaterialId = 6,
                    Consumption = 0.08f,
                    ValueWorking = 45.0f,
                    Quantity = 3.60f,
                    Price = 46.0m,
                    Amount = 165.60m
                },
                new EstimateMaterial
                {
                    Id = 25,
                    EstimateId = 3,
                    EstimateWorkId = 18,
                    MaterialId = 9,
                    Consumption = 5.70f,
                    ValueWorking = 45.0f,
                    Quantity = 256.50f,
                    Price = 12.0m,
                    Amount = 3078.0m
                },
                new EstimateMaterial
                {
                    Id = 26,
                    EstimateId = 3,
                    EstimateWorkId = 18,
                    MaterialId = 11,
                    Consumption = 0.30f,
                    ValueWorking = 45.0f,
                    Quantity = 13.50f,
                    Price = 110.0m,
                    Amount = 1485.0m
                },
                new EstimateMaterial
                {
                    Id = 27,
                    EstimateId = 3,
                    EstimateWorkId = 18,
                    MaterialId = 14,
                    Consumption = 1.05f,
                    ValueWorking = 45.0f,
                    Quantity = 47.25f,
                    Price = 1200.0m,
                    Amount = 56700.0m
                },
                new EstimateMaterial
                {
                    Id = 28,
                    EstimateId = 3,
                    EstimateWorkId = 18,
                    MaterialId = 12,
                    Consumption = 0.30f,
                    ValueWorking = 45.0f,
                    Quantity = 13.50f,
                    Price = 37.0m,
                    Amount = 499.50m
                },
                new EstimateMaterial
                {
                    Id = 29,
                    EstimateId = 3,
                    EstimateWorkId = 19,
                    MaterialId = 6,
                    Consumption = 0.08f,
                    ValueWorking = 98.0f,
                    Quantity = 7.84f,
                    Price = 46.0m,
                    Amount = 360.64m
                },
                new EstimateMaterial
                {
                    Id = 30,
                    EstimateId = 3,
                    EstimateWorkId = 20,
                    MaterialId = 9,
                    Consumption = 5.70f,
                    ValueWorking = 98.0f,
                    Quantity = 558.60f,
                    Price = 12.0m,
                    Amount = 6703.20m
                },
                new EstimateMaterial
                {
                    Id = 31,
                    EstimateId = 3,
                    EstimateWorkId = 20,
                    MaterialId = 11,
                    Consumption = 0.30f,
                    ValueWorking = 98.0f,
                    Quantity = 29.40f,
                    Price = 110.0m,
                    Amount = 3234.0m
                },
                new EstimateMaterial
                {
                    Id = 32,
                    EstimateId = 3,
                    EstimateWorkId = 20,
                    MaterialId = 15,
                    Consumption = 1.05f,
                    ValueWorking = 98.0f,
                    Quantity = 102.90f,
                    Price = 650.0m,
                    Amount = 66885.0m
                },
                new EstimateMaterial
                {
                    Id = 33,
                    EstimateId = 3,
                    EstimateWorkId = 20,
                    MaterialId = 12,
                    Consumption = 0.3f,
                    ValueWorking = 98.0f,
                    Quantity = 29.40f,
                    Price = 37.0m,
                    Amount = 1087.80m
                },
            };

            _context.EstimateMaterials.AddRange(estimateMaterials);
            _context.SaveChanges();
        }
    }
}
