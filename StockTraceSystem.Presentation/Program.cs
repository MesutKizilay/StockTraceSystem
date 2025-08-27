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
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.Cookies;

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
            builder.Services
                            //.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddAuthentication(options =>
                            {
                                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                            })
                            //.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
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
                                    },
                                    //OnAuthenticationFailed = a =>
                                    //{
                                    //    a.Response.Redirect($"/Auth/Login");
                                    //    return Task.CompletedTask;
                                    //}
                                    //OnChallenge = ctx =>
                                    //{
                                    //    // Varsayýlan "WWW-Authenticate + body" davranýþýný bastýr
                                    //    ctx.HandleResponse();
                                    //    ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                    //    return Task.CompletedTask;
                                    //}

                                    //OnChallenge = ctx =>
                                    //{
                                    //    ctx.HandleResponse(); // çok kritik
                                    //                          // Hiçbir þey yazma, sadece 401 kalsýn; StatusCodePages bunu yakalayacak
                                    //    return Task.CompletedTask;
                                    //}
                                    //OnAuthenticationFailed = ctx =>
                                    //{
                                    //    // Token süresi bittiyse istemciye ipucu ver
                                    //    if (ctx.Exception is SecurityTokenExpiredException)
                                    //        ctx.Response.Headers["Token-Expired"] = "true";
                                    //    return Task.CompletedTask;
                                    //}
                                    //OnChallenge = ctx =>
                                    //{
                                    //    // Varsayýlan davranýþý bastýr
                                    //    ctx.HandleResponse();

                                    //    if (IsAjax(ctx.Request))
                                    //    {
                                    //        ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                    //        ctx.Response.ContentType = "application/json";
                                    //        //ctx.Response.Redirect($"/Auth/Login?returnUrl={Uri.EscapeDataString(ctx.Request.Path + ctx.Request.QueryString)}");
                                    //        //return Task.CompletedTask;
                                    //        var payload = new { error = "unauthorized", redirect = "/Auth/Login" };
                                    //        return ctx.Response.WriteAsync(JsonSerializer.Serialize(payload));
                                    //    }
                                    //    else
                                    //    {
                                    //        // Normal sayfa isteði => login'e yönlendir
                                    //        ctx.Response.Redirect($"/Auth/Login?returnUrl={Uri.EscapeDataString(ctx.Request.Path + ctx.Request.QueryString)}");
                                    //        return Task.CompletedTask;
                                    //    }
                                    //},
                                };
                            })
                            .AddCookie(config =>
                            {
                                //config.Cookie.HttpOnly = true;
                                //config.ExpireTimeSpan = TimeSpan.FromSeconds(10);
                                //config.SlidingExpiration = true;
                                config.LoginPath = "/Auth/Login";
                                config.AccessDeniedPath = "/Home/AccessDenied";
                            });

            //static bool IsAjax(HttpRequest r) =>
            //                                  r.Headers.TryGetValue("X-Requested-With", out var v) && v == "XMLHttpRequest"
            //                               || r.Headers.Accept.Any(a => a.Contains("application/json", StringComparison.OrdinalIgnoreCase));


            builder.Services.AddAuthorization();

            var app = builder.Build();



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

            app.UseStatusCodePagesWithReExecute("/Home/NotFound404");
            //app.UseStatusCodePages(async context =>
            //{
            //    var res = context.HttpContext.Response;
            //    var req = context.HttpContext.Request;

            //    // Sadece HTML sayfa isteklerinde redirect yap (AJAX/JSON'a dokunma)
            //    var isHtml = req.Headers["Accept"].ToString().Contains("text/html", StringComparison.OrdinalIgnoreCase);
            //    var isApi = req.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase);

            //    if (res.StatusCode == StatusCodes.Status401Unauthorized && isHtml && !isApi && !res.HasStarted)
            //    {
            //        var returnUrl = Uri.EscapeDataString(req.Path + req.QueryString);
            //        res.Redirect($"/Auth/Login?returnUrl={returnUrl}");
            //    }
            //    await Task.CompletedTask;
            //});

            //app.UseStatusCodePages(context =>
            //{
            //    var res = context.HttpContext.Response;
            //    var req = context.HttpContext.Request;

            //    if (res.StatusCode == 401 && !IsAjax(req))
            //    {
            //        var returnUrl = Uri.EscapeDataString(req.Path + req.QueryString);

            //        var logLine = $"{DateTime.Now:u} | ReturnUrl: {returnUrl}{Environment.NewLine}";
            //        var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "returnurls.txt");
            //        File.AppendAllText(logPath, logLine);


            //        res.Redirect($"/Auth/Login?returnUrl={returnUrl}");
            //        //res.Redirect($"/Auth/Login");
            //    }
            //    //else if (res.StatusCode == 403)
            //    //{
            //    //    var returnUrl = Uri.EscapeDataString(req.Path + req.QueryString);
            //    //    res.Redirect($"/Auth/AccessDenied?returnUrl={returnUrl}");
            //    //}
            //    return Task.CompletedTask;
            //});

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Warehouses}/{action=Stocktaking}/{id?}")
                //pattern: "{controller=Auth}/{action=Login}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}