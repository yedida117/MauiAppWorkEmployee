using MauiAppWorkEmployee.ViewModel;
using MauiAppWorkEmployee.Views;
using Microsoft.Extensions.Logging;

namespace MauiAppWorkEmployee
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<AddWorkPage2>();
            builder.Services.AddTransient<AddWorkVm>();
            return builder.Build();
        }
    }
}
