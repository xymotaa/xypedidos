using ListasCompras.Models;

namespace ListasCompras.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        // Já tem dados, não faz nada
        if (context.Categorias.Any()) return;

        // Categorias
        var categorias = new List<Categoria>
        {
            new() { Nome = "Capinha", RequerModelo = true },
            new() { Nome = "Película", RequerModelo = true },
            new() { Nome = "Fone de Ouvido", RequerModelo = false },
            new() { Nome = "Caixinha de Som", RequerModelo = false },
            new() { Nome = "Pendrive", RequerModelo = false },
            new() { Nome = "Cabo", RequerModelo = false },
            new() { Nome = "Carregador", RequerModelo = false },
        };
        context.Categorias.AddRange(categorias);

        // Marcas
        var samsung = new MarcaCelular { Nome = "Samsung" };
        var apple = new MarcaCelular { Nome = "Apple" };
        var motorola = new MarcaCelular { Nome = "Motorola" };
        var xiaomi = new MarcaCelular { Nome = "Xiaomi" };

        context.MarcasCelular.AddRange(samsung, apple, motorola, xiaomi);

        // Modelos Samsung
        var modelosSamsung = new List<ModeloCelular>
        {
            new() { Nome = "Galaxy A05", MarcaCelular = samsung },
            new() { Nome = "Galaxy A05s", MarcaCelular = samsung },
            new() { Nome = "Galaxy A12", MarcaCelular = samsung },
            new() { Nome = "Galaxy A13", MarcaCelular = samsung },
            new() { Nome = "Galaxy A14", MarcaCelular = samsung },
            new() { Nome = "Galaxy A15", MarcaCelular = samsung },
            new() { Nome = "Galaxy A23", MarcaCelular = samsung },
            new() { Nome = "Galaxy A24", MarcaCelular = samsung },
            new() { Nome = "Galaxy A34", MarcaCelular = samsung },
            new() { Nome = "Galaxy A54", MarcaCelular = samsung },
            new() { Nome = "Galaxy S23", MarcaCelular = samsung },
            new() { Nome = "Galaxy S24", MarcaCelular = samsung },
        };

        // Modelos Apple
        var modelosApple = new List<ModeloCelular>
        {
            new() { Nome = "iPhone 11", MarcaCelular = apple },
            new() { Nome = "iPhone 12", MarcaCelular = apple },
            new() { Nome = "iPhone 13", MarcaCelular = apple },
            new() { Nome = "iPhone 14", MarcaCelular = apple },
            new() { Nome = "iPhone 15", MarcaCelular = apple },
            new() { Nome = "iPhone 15 Pro", MarcaCelular = apple },
        };

        // Modelos Motorola
        var modelosMotorola = new List<ModeloCelular>
        {
            new() { Nome = "Moto G14", MarcaCelular = motorola },
            new() { Nome = "Moto G24", MarcaCelular = motorola },
            new() { Nome = "Moto G34", MarcaCelular = motorola },
            new() { Nome = "Moto G54", MarcaCelular = motorola },
            new() { Nome = "Moto G84", MarcaCelular = motorola },
            new() { Nome = "Moto E13", MarcaCelular = motorola },
            new() { Nome = "Edge 40", MarcaCelular = motorola },
        };

        // Modelos Xiaomi
        var modelosXiaomi = new List<ModeloCelular>
        {
            new() { Nome = "Redmi 12", MarcaCelular = xiaomi },
            new() { Nome = "Redmi 13", MarcaCelular = xiaomi },
            new() { Nome = "Redmi Note 12", MarcaCelular = xiaomi },
            new() { Nome = "Redmi Note 13", MarcaCelular = xiaomi },
            new() { Nome = "Poco X5", MarcaCelular = xiaomi },
        };

        context.ModelosCelular.AddRange(modelosSamsung);
        context.ModelosCelular.AddRange(modelosApple);
        context.ModelosCelular.AddRange(modelosMotorola);
        context.ModelosCelular.AddRange(modelosXiaomi);

        context.SaveChanges();
    }
}