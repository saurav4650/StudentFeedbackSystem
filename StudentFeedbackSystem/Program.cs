using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using StudentFeedbackSystem.EntityModels;
using StudentFeedbackSystem.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IRoles, RolesRepo>();
builder.Services.AddTransient<IDepartment, DepartmentRepo>();
builder.Services.AddTransient<ISemester, SemesterRepo>();
builder.Services.AddTransient<IQuestionCat, QuestionCategoriesRepo>();
builder.Services.AddTransient<IUser, UserRepo>();
builder.Services.AddTransient<ICourse, CourseRepo>();
builder.Services.AddTransient<ICourseSchedules, CourseScheduleRepo>();
builder.Services.AddTransient<IEnrollment, EnrollmentRepo>();
builder.Services.AddTransient<IFeedbackQuestion, FeedbackQuestionRepo>();
builder.Services.AddTransient<IFeedback, FeedbackRepo>();


var connectionString = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddDbContext<StudentFeedbackDbContext>(x => x.UseSqlServer(connectionString));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.LogoutPath = "/Login/Logout";
        options.AccessDeniedPath = "/Home/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
