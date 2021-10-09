using MinimalApi.Data;
using MinimalApi.ViewModels;

var builder = WebApplication.CreateBuilder(args);
ConfigureDbContext(builder.Services);
ConfigureSwagger(builder.Services);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("v1/todos", (AppDbContext context) => {
    var todos = context.Todos.ToList();
    return Results.Ok(todos);
});

app.MapPost("v1/todos", (AppDbContext context, CreateTodoViewModel viewModel) => {
    var todo = viewModel.ValidateAndMapTo();
    if (!viewModel.IsValid)
    {
        return Results.BadRequest(viewModel.Notifications);
    }

    context.Todos.Add(todo);
    context.SaveChanges();
    return Results.Created($"v1/todos/{todo.Id}", todo);
});

void ConfigureSwagger(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}

void ConfigureDbContext(IServiceCollection services) 
{
    services.AddDbContext<AppDbContext>();
}

app.Run();
