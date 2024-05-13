namespace Escola.Application.ExtensionMethod
{
    public static class ExtensionMethod
    {      

        public static bool CpfIsValid(this string cpf)
        {
            if (cpf.Length != 11 || !JustDigits(cpf))
            {
                return false;
            }

            int[] numbersCPF = new int[11];
            for (int i = 0; i < 11; i++)
            {
                numbersCPF[i] = int.Parse(cpf[i].ToString());
            }

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += numbersCPF[i] * (10 - i);
            }

            int resto = sum % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            if (numbersCPF[9] != digitoVerificador1)
            {
                return false;
            }

            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += numbersCPF[i] * (11 - i);
            }

            resto = sum % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            return numbersCPF[10] == digitoVerificador2;
        }

        public static bool JustDigits(this string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
