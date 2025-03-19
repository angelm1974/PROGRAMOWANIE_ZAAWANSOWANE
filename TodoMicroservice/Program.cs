using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dodajemy kontrolery
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();


// Konfiguracja CORS (opcjonalne, jeśli używasz frontendu)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlite("Data source=TodoList.db"));


var app = builder.Build();
// app.MapOpenApi(); // This method does not exist
app.MapOpenApi();
app.UseSwaggerUI(c => c.SwaggerEndpoint("../openapi/v1.json", "Server v1"));
// Middleware
app.UseCors("AllowAllOrigins");
app.UseRouting();

// Diagnostyczny endpoint
app.MapGet("/", () => "Mikroserwis działa!");

// Mapowanie kontrolerów
app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
    db.Database.EnsureCreated();
    if (!db.TodoItems.Any())
    {
        db.TodoItems.AddRange(
            new TodoItem { Title = "Uruchomienie mikroserwisu", IsComplete = false }
        );
        db.SaveChanges();
}
}
app.Run();

// Model
public class TodoItem
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public bool IsComplete { get; set; }
}

// DbContext
public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }
    public DbSet<TodoItem> TodoItems { get; set; }
}

// Kontroler
[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoDbContext _context;
    public TodoController(TodoDbContext context)
    {
        _context = context;
    }

    // GET: api/todo/gettodos
    [HttpGet("gettodos")]
    public ActionResult<IEnumerable<TodoItem>> GetTodos()
    {
        return Ok(_context.TodoItems.ToList());
    }

    // GET: api/todo/{id}
    [HttpGet("{id}")]
    public ActionResult<TodoItem> GetTodo(long id)
    {
        var todo = _context.TodoItems.FirstOrDefault(x => x.Id == id);
        if (todo == null)
        {
            return NotFound();
        }
        return Ok(todo);
    }

    // POST: api/todo
    [HttpPost]
    public ActionResult<TodoItem> AddTodo([FromBody] TodoItem item)
    {
        _context.TodoItems.Add(item);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetTodo), new { id = item.Id }, item);
    }

    // PUT: api/todo/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateTodo(long id, [FromBody] TodoItem item)
    {
        var todo = _context.TodoItems.FirstOrDefault(x => x.Id == id);
        if (todo == null)
        {
            return NotFound();
        }
        todo.Title = item.Title;
        todo.IsComplete = item.IsComplete;
        _context.SaveChanges();
        return NoContent();
    }

    // DELETE: api/todo/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteTodo(long id)
    {
        var todo = _context.TodoItems.FirstOrDefault(x => x.Id == id);
        if (todo == null)
        {
            return NotFound();
        }
        _context.TodoItems.Remove(todo);
        _context.SaveChanges();
        return NoContent();
    }
}