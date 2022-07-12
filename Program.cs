/*var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");*/


using System.Collections.ObjectModel;
using Cl.Ucn.Disc.Pdis.Fivet.gRPC;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("http://localhost:8080");
var client = new FivetService.FivetServiceClient(channel);

PersonaEntity persona = new PersonaEntity
{
    Nombre = "Sebastian",
    Email = "seba@gmail.com",
    Direccion = "miscasa123",
    Rut = "20218430-8"
};

PersonaEntity veterinario = new PersonaEntity
{
    Nombre = "alexis",
    Direccion = "dondevivo999",
    Email = "veterinario@gmail.com",
    Rut = "19248292-4"
};

FichaMedicaEntity ficha = new FichaMedicaEntity
{
    NumeroFicha = 1,
    NombrePaciente = "perroguau",
    Color = "cafe",
    Duenio = persona,
    Especie = "perro",
    Raza = "labrador",
    Sexo = SexoEntity.Macho,
    Tipo = "exotico",
    FechaNacimiento = "2017-01-13"
};

ControlEntity control = new ControlEntity
{
    Altura = 1.63,
    Diagnostico = "bien de salud",
    Fecha = "2016-10-05T08:20:10+05:30[Asia/Kolkata]",
    Peso = 2.51,
    Temperatura = 3.43,
    Veterinario = veterinario,
    FichaMedica = ficha
};

Console.WriteLine("---------------------------------");
var reply1 = client.addPersona(new AddPersonaReq
    {
        Persona = persona
    }
);
Console.WriteLine(reply1);

var reply11 = client.addPersona(new AddPersonaReq
    {
        Persona = veterinario
    }
);

Console.WriteLine("---------------------------------");
var reply2 = client.autenticate(new AutenticateReq
    {
        Login = "seba@gmail.com",
        Password = "a"
    }
);
Console.WriteLine(reply2);

Console.WriteLine("---------------------------------");
var reply3 = client.addFichaMedica(new AddFichaMedicaReq
    {
        FichaMedica = ficha
    }
);
Console.WriteLine(reply3);

Console.WriteLine("---------------------------------");
var reply4 = client.addControl(new AddControlReq
    {
        Control = control
    }
);
Console.WriteLine(reply4);

Console.WriteLine("---------------------------------");
var reply5 = client.retrieveFichaMedica(new RetrieveFichaMedicaReq
    {
        NumeroFicha = 1
    }
);
Console.WriteLine(reply5);

Console.WriteLine("---------------------------------");
Collection<FichaMedicaReply> reply6 = new Collection<FichaMedicaReply>();
reply6.Add(client.searchFichaMedica(new SearchFichaMedicaReq
    {
        Query = "1"
    }
));
Console.WriteLine(reply6.First());
Console.WriteLine("---------------------------------");