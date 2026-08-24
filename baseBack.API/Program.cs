var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//_______________________________X__________________________Conexão Swagger__________X
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    
    policy.WithOrigins("http://localhost:4200")
           .AllowAnyMethod()
           .AllowAnyHeader());
    
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //_______________________________X__________________________Conexão Swagger_____X
    app.UseCors("AllowAngular");
    app.UseSwagger();
    app.UseSwaggerUI();

    //app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
