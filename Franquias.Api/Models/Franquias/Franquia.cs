public sealed class Franquia
{
    public int id { get; set; }
    public int cnpj { get; set;}
    public long contato{get; set;}
    public string nomeFranquia {get; set;} = string.Empty;
    public string situacao {get; set;} = string.Empty;
    public DateTimeOffset dataInicio {get; set;}
    public string endereco {get; set;} = string.Empty;
    

}