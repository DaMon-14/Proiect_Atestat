using Microsoft.Extensions.Configuration;

namespace AttendanceAPI
{
    public class Global
    {
        private readonly IConfiguration _configuration;
        public Global(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsUIDValid(string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return false;
            }
            return true;
        }
    }
}
