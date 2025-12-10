using System.Threading.Tasks;
using Grosu_Andrada_ClinicAppointments.Models;

namespace Grosu_Andrada_ClinicAppointments.Services
{
    public interface INoShowPredictionService
    {
        Task<string> PredictNoShowAsync(ModelInput input);
    }
}
