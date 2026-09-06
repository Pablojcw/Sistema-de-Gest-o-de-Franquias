using System.Runtime.InteropServices;   
using Franquias.Api.Models;

public  class Franquias
{
    
    private readonly List<Franquias> _Franquia;
    private int _proximoId; 
    public Franquias()
    {
        
        _Franquia = new List<Franquias>
        {
            new Franquias
            {
                id = 1 ,
                titulo = "ARROZ DO BRASIL",
                Descricao  =  "Preparamos arroz para todos os Brasileiros",
                status = StatusFranquias.concluidas,
                criadoEm = DateTimeOffset.UtcNow.AddHours(-2),
                concluidaEm  = DateTimeOffset.UtcNow.AddHours(-1)
            }
        };

        _proximoId = _Franquia.Count == 0 ? 1 : _Franquia.Max(Tarefa => Tarefa.id) + 1;
    }
}