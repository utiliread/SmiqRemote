using System.Threading.Tasks;

namespace SmiqServer
{
    public static class CommandBuilderExtensions
    {
        public static Task ExecuteAsync(this ICommandBuilder builder, IInstrument instrument)
        {
            return instrument.CommandAsync(builder.BuildCommand());
        }
    }
}
