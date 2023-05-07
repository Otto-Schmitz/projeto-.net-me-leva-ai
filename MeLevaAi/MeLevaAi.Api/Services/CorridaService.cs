using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Domains;
using MeLevaAi.Api.Mappers;
using MeLevaAi.Api.Repositories;

namespace MeLevaAi.Api.Services
{
    public class CorridaService
    {
        private readonly CorridaRepository _corridaRepository;

        private readonly VeiculoRepository _veiculoRepository;

        private readonly MotoristaRepository _motoristaRepository;

        private readonly PassageiroRepository _passageiroRepository;

   
        public CorridaService() {
            _corridaRepository = new CorridaRepository();       
            _veiculoRepository = new VeiculoRepository();
            _motoristaRepository = new MotoristaRepository();
            _passageiroRepository = new PassageiroRepository();
        }

        public CorridaDto Chamar(ChamarCorridaRequest request)
        {
            var response = new CorridaDto();

            var veiculo = ChamarVeiculo();

            if (veiculo == null) {
                response.AddNotification(new Validations.Notification("Nenhum veículo disponível foi encontrados."));
                return response;
            }

            var motorista = _motoristaRepository.Obter(veiculo.MotoristaId);

            var passageiro = _passageiroRepository.Obter(request.PassageiroId);

            if (passageiro == null)
            {
                response.AddNotification(new Validations.Notification("Passageiro inválida."));
                return response;
            }
            if (passageiro.EmCorrida)
            {
                response.AddNotification(new Validations.Notification("Passageiro em corrida."));
                return response;
            }

            var corrida = request.ToCorrida(passageiro, veiculo);
            _corridaRepository.Adicionar(corrida);

            passageiro.AdicionarCorrida(corrida);
            motorista.AdicionarCorrida(corrida);

            response = corrida.ToCorridaDto(passageiro, motorista);

            return response;
        }
         
        private Veiculo? ChamarVeiculo()
        {
            var veiculos = _veiculoRepository.Listar().ToArray();

            var veiculo = new Veiculo();

            foreach (var v in veiculos)
            {
                var motorista = _motoristaRepository.Obter(v.MotoristaId.GetValueOrDefault());

                if (motorista.CarteiraDeHabilitacao.DataVencimento > DateTime.Now)
                    continue;
                if (motorista.EmCorrida)
                    continue;
                veiculo = v;
            }

            return veiculo;
        }
    }
}
