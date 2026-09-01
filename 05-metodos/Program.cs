// exercício 1:

static bool VerificadorCPF(string cpf)
{
    if (string.IsNullOrWhiteSpace(cpf))
    {
        return false;
    }
    if (cpf.Length != 11)
    {
        return false;
    }
    
    foreach (var letter in cpf)
    {
        if (!char.IsDigit(letter))
        {
            return false;
        }
    }
    return true;
}

VerificadorCPF("50222222222");