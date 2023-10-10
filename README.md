## 簡介

此專案為自主練習項目，搭配 Vue3 翻新舊有 Asp.Net MVC 圖書管理系統基本功能，系統畫面可參考[此處](https://github.com/Shih906/Book-Management-FrontEnd-Vue3)

---

## 配置與設定說明
### appsettings.json
#### 配置 JWT Token 與連線字串
```json=
{
  "JWT": {
    "Token":  "asfewrfgdfagfdcxvfghdshhggr"
  },
  "ConnectionStrings": {
    "BooksManageAppDBConnection": "Data Source=DESKTOP-C8PLHO4\\SQLEXPRESS;database=BookData;Trusted_connection=True;TrustServerCertificate=True"
  }
}
```
### Program.cs
#### 註冊 EF Core DbContext
```csharp=
builder.Services.AddDbContext<BooksAPIDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("BooksManageAppDBConnection")));
```
#### 註冊 Repository 至 IOC 容器
```csharp=
  builder.Services.AddScoped<IBookRepository, BookRepository>();
  builder.Services.AddScoped<IUserRepository, UserRepository>();
  builder.Services.AddScoped<ICodeRepository, CodeRepository>();
  builder.Services.AddScoped<IClassRepository, ClassRepository>();
  builder.Services.AddScoped<IMemberRepository, MemberRepository>();
```
#### 配置 JWT Token Authentication
```csharp=
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("JWT:Token").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
```
#### 配置 SwaggerGen，在 Swagger UI 中顯示使用 OAuth2 身份驗證 JWT 的功能
```csharp=
  builder.Services.AddSwaggerGen(options =>
  {
      options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
      {
          Description = "Authorization header using Bearer token",
          In = ParameterLocation.Header,
          Name = "Authorization",
          Type = SecuritySchemeType.ApiKey
      });
      options.OperationFilter<SecurityRequirementsOperationFilter>();
  });
```

---
## Entity
### ERD
![image](https://github.com/Shih906/Book-Management-WebApi/assets/88469902/cab7885d-2817-4cb1-aee0-b7675979be6d)

---

## 套件與版本環境
* C#: 6
* .Net: 6
* AutoMapper:12.0.1
* Authentication.JwtBearer: 6.0.0
* EntityFrameworkCore.SqlServer: 8.0.0



