// exercício 1:

static bool VerificadorCPF(string cpf)
{
    if (string.IsNullOrWhiteSpace(cpf))
    {
        return false;
    }
    else if (cpf.Length != 11)
    {
        return false;
    }
    
    foreach (var letter in cpf)
    {
        if (!char.Isdigit(letter))
        {
            return false;
        }
    }
    return true;
}