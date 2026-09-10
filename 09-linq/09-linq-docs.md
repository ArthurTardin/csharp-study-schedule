# Etapa 9 - LINQ

## 1. O que é LINQ

Um conjunto de métodos de extensão que permitem consultar, filtrar, ordenar e transformar coleções (`List`, array, `Dictionary`, etc) de forma declarativa, você descreve **o que** quer, não **como** conseguir, ao contrário de escrever um `foreach` manual passo a passo.

```CSharp
    List<int> numeros = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

    // Sem LINQ (Etapa 3/4, jeito manual)
    List<int> pares = new List<int>();
    foreach(int n in numeros)
    {
        if (n % 2 == 0) pares.Add(n);
    }

    // Com LINQ

    List<int> paresLinq = numeros.where(n => n % 2 == 0). ToList();
```

Os dois produzem o mesmo resultado, LINQ não é "mágica nova", é uma forma mais compacta e legível de expressar operações que você já sabe fazer manualmente.

