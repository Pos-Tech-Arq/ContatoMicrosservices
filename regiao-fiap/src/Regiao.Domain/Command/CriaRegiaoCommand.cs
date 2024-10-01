namespace Regiao.Domain.Command;

public class CriaRegiaoCommand
{
    public string Ddd { get; set; }

    public CriaRegiaoCommand(string ddd)
    {
        Ddd = ddd;
    }
}