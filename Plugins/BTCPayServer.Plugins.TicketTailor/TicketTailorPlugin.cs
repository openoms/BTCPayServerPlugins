using BTCPayServer.Abstractions.Contracts;
using BTCPayServer.Abstractions.Extensions;
using BTCPayServer.Abstractions.Models;
using BTCPayServer.Abstractions.Services;
using BTCPayServer.Services.Apps;
using Microsoft.Extensions.DependencyInjection;

namespace BTCPayServer.Plugins.TicketTailor
{
    public class TicketTailorPlugin : BaseBTCPayServerPlugin
    {
        public override IBTCPayServerPlugin.PluginDependency[] Dependencies { get; } =
        {
            new() {Identifier = nameof(BTCPayServer), Condition = ">=1.12.0"}
        };

        public override void Execute(IServiceCollection applicationBuilder)
        {
            applicationBuilder.AddStartupTask<AppMigrate>();
            applicationBuilder.AddSingleton<TicketTailorService>();
            applicationBuilder.AddHostedService(s => s.GetRequiredService<TicketTailorService>());

            applicationBuilder.AddSingleton<IUIExtension>(new UIExtension("TicketTailor/NavExtension", "header-nav"));
            applicationBuilder.AddSingleton<AppBaseType, TicketTailorApp>();
            base.Execute(applicationBuilder);
        }
    }
}