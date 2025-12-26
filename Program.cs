using Microsoft.EntityFrameworkCore;
using TodoList.Models;
using TodoList.Services;
//using Microsoft.OpenApi;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseInMemoryDatabase("TodoDb");
});

builder.Services.AddControllers();

// Register your Service
builder.Services.AddScoped<TodoService>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();          

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    if (!context.Todos.Any())
    {
        context.Todos.Add(new Todo { TaskName = "Finish Frontend", IsCompleted = false });
        context.Todos.Add(new Todo { TaskName = "Learn CORS", IsCompleted = true });
        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();   
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();


app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();