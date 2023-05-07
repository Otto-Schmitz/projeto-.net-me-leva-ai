using MeLevaAi.Api.Repositories;

namespace MeLevaAi.Api.Services
{
    public class CorridaService
    {
        private readonly CorridaRepository _corridaRepository;
        public CorridaService() {
            _corridaRepository = new CorridaRepository();
        }
    }
}
