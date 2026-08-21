using System;
using System.Linq;

namespace project05
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
        }

        static double Soma(double a, double b)
        {
            return a + b;
        }

        static double Substrair(double a, double b)
        {
            return a - b;
        }

        static double Multiplicar(double a, double b)
        {
            return a + b;
        }

        static double Dividir(double a, double b)
        {
            return a + b;
        }

        static bool EprimoIterativo(int n)
        {
            if (n <= 1) return false;
            if (n == 2) return true;
            if (n % 2 == 0) return false;

            int limite = (int)Math.Sqrt(n);
            for (int i = 3; i <= limite; i += 2)
            {
                if (n % i == 0) return false;
            }

            return true;
        }

        static bool EPrimoRecursivo(int n, int divisor = 2)
        {
            if (n <= 1) return false;
            if (n == 2) return true;
            if (n % divisor == 0) return false;
            if (divisor * divisor > n) return true;

            return EPrimoRecursivo(n, divisor + 1);
        }

         public static bool Validar(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;

            // 1. Limpa caracteres não numéricos
            string digitos = new string(cpf.Where(char.IsDigit).ToArray());

            // CPF precisa ter exatamente 11 dígitos
            if (digitos.Length != 11) return false;

            // Rejeita CPFs com todos os dígitos iguais (ex: 000.000.000-00)
            if (digitos.All(c => c == digitos[0])) return false;

            // 2. Validação do Primeiro Dígito Verificador
            int soma1 = 0;
            int[] pesos1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            for (int i = 0; i < 9; i++)
            {
                soma1 += (digitos[i] - '0') * pesos1[i];
            }

            int resto1 = (soma1 * 10) % 11;
            if (resto1 == 10) resto1 = 0;

            if (resto1 != (digitos[9] - '0')) return false;

            // 3. Validação do Segundo Dígito Verificador
            int soma2 = 0;
            int[] pesos2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            for (int i = 0; i < 10; i++)
            {
                soma2 += (digitos[i] - '0') * pesos2[i];
            }

            int resto2 = (soma2 * 10) % 11;
            if (resto2 == 10) resto2 = 0;

            return resto2 == (digitos[10] - '0');
        }

        public static long MDC(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return Math.Abs(a);
        }

        public static long MMC(long a, long b)
        {
            if (a == 0 || b == 0) return 0;
            return Math.Abs(a * b) / MDC(a, b);
        }
        public static long Fatorial(int n)
        {
            if (n < 0) throw new ArgumentOutOfRangeException(nameof(n), "Número deve ser não-negativo.");
            if (n > 20) throw new OverflowException("Fatorial excede o limite de um long 64-bit (n > 20).");

            long resultado = 1;
            for (int i = 2; i <= n; i++)
            {
                resultado *= i;
            }
            return resultado;
        }
        public static double Mapear(double valor, double minOrigem, double maxOrigem, double minDestino, double maxDestino)
        {
            if (Math.Abs(maxOrigem - minOrigem) < double.Epsilon)
                throw new ArgumentException("Intervalo de origem não pode ter amplitude zero.");

            return minDestino + (valor - minOrigem) * (maxDestino - minDestino) / (maxOrigem - minOrigem);
        }
    }
} 

    
   