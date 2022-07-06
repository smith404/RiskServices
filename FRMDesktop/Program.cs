var builder = WebApplication.CreateBuilder(args);

// Add services to serer html pages
builder.Services.AddRazorPages();

// Add services to serer REST api
builder.Services.AddControllers();

// Build the swagger inforamtion
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();