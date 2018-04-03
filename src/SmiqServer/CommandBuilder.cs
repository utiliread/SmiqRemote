using System.Text;

namespace SmiqServer
{
    public abstract class CommandBuilder : ICommandBuilder
    {
        public byte[] BuildCommand()
        {
            var payload = BuildPayload();

            return Encoding.ASCII.GetBytes(payload + "\n");
        }

        public abstract string BuildPayload();
    }
}
