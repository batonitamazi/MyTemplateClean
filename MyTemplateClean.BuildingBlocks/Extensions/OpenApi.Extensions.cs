using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;

namespace MyTemplateClean.BuildingBlocks.Extensions;

public static partial class  Extensions
{
    public static IApplicationBuilder UseDefaultOpenApi(this WebApplication app)
    {
        var configuration = app.Configuration;
        var openApiSection = configuration.GetSection("OpenApi");
        
        if (!openApiSection.Exists())
        {
            return app;
        }
        app.MapOpenApi();

        if (app.Environment.IsDevelopment())
        {
            app.MapScalarApiReference(options =>
            {
                // Disable default fonts to avoid download unnecessary fonts
                options.DefaultFonts = false;
            });
            app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();
        }

        return app;
    }

    public static IHostApplicationBuilder AddDefaultOpenApi(this IHostApplicationBuilder builder, IApiVersioningBuilder? apiVersioning = default)
    {
        var openApi = builder.Configuration.GetSection("OpenApi");
        if (!openApi.Exists())
        {
            return builder;
        }
        if (apiVersioning is not null)
        {
            // the default format will just be ApiVersion.ToString(); for example, 1.0.
            // this will format the version as "'v'major[.minor][-status]"
            var versioned = apiVersioning.AddApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            string[] versions = ["v1", "v2"];
            foreach (var description in versions)
            {
                builder.Services.AddOpenApi(description, options =>
                {
                });
            }
        }

        return builder;
    }
}