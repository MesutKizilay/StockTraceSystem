using StockTraceSystem.Persistence;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using StockTraceSystem.Application;
using Core.Security.JWT;
using Core.Security.Encryption;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace StockTraceSystem.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddSecurityervices();

            builder.Services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidIssuer = tokenOptions.Issuer,
                                    ValidAudience = tokenOptions.Audience,
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
                                    ClockSkew = TimeSpan.Zero
                                };

                                options.Events = new JwtBearerEvents
                                {
                                    OnMessageReceived = ctx =>
                                    {
                                        if (ctx.Request.Cookies.TryGetValue("AccessToken", out var token))
                                            ctx.Token = token;
                                        //else
                                        //    ctx.Response.Redirect($"/Auth/Login");

                                        return Task.CompletedTask;
                                    }
                                    //OnAuthenticationFailed = a =>
                                    //{
                                    //    a.Response.Redirect($"/Auth/Login");
                                    //    return Task.CompletedTask;
                                    //}
                                };
                            });


            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseStatusCodePages(context =>
            {
                var res = context.HttpContext.Response;
                var req = context.HttpContext.Request;

                if (res.StatusCode == 401)
                {
                    var returnUrl = Uri.EscapeDataString(req.Path + req.QueryString);
                    res.Redirect($"/Auth/Login?returnUrl={returnUrl}");
                }
                //else if (res.StatusCode == 403)
                //{
                //    var returnUrl = Uri.EscapeDataString(req.Path + req.QueryString);
                //    res.Redirect($"/Auth/AccessDenied?returnUrl={returnUrl}");
                //}
                return Task.CompletedTask;
            });

            //if (app.Environment.IsProduction())
                app.ConfigureCustomExceptionMiddleware();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Warehouse}/{action=Stocktaking}/{id?}")
                //pattern: "{controller=Auth}/{action=Login}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}