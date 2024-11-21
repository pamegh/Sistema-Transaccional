namespace Proyecto
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin() // Permite todos los orígenes
                           .AllowAnyMethod() // Permite todos los métodos (GET, POST, PUT, DELETE, etc.)
                           .AllowAnyHeader(); // Permite cualquier encabezado
                });
            });

            services.AddControllers(); // Agregar los servicios para los controladores
        }
    

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // Aplica la política de CORS
        app.UseCors("AllowAll");

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Mapea los controladores de la API
        });
    }
}
}
