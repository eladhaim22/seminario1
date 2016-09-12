using Seminario.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Seminario.Validationes
{
    public class ChequeValidator : EntityValidator<Cheque>
    {
        public ChequeValidator()
        {
            RuleFor(x => x.Banco).NotNull().NotEmpty().WithLocalizedMessage(() => "Debe elejir Banco para cada cheque");
            RuleFor(x => x.CuitEmisor).Must(ValidarCuit).WithLocalizedMessage(()=>"El cuit del emistor del cheque debe ser valido");
            RuleFor(x => x.FechaAcreditacion).NotNull().NotEmpty().WithLocalizedMessage(() => "La fecha de acreditacion debe ser valido");
            RuleFor(x => x.FechaAcreditacion).LessThanOrEqualTo(DateTime.Now).WithLocalizedMessage(() => "La fecha de acreditacion debe ser posterior de hoy");
            RuleFor(x => x.NombreEmisor).NotNull().NotEmpty().WithLocalizedMessage(() => "el nombre del emisor debe ser valido");
            RuleFor(x => x.Importe).NotNull().NotEmpty().LessThanOrEqualTo(0).WithLocalizedMessage(()=> "el monto del cheque debe ser mayor que 0")
        }
        
        private bool ValidarCuit(string cuit)
        {
            try
            {
                int[] ia = cuit.ToCharArray().Select(n => Convert.ToInt32(n.ToString())).ToArray();
                if (ia.Length == 11)
                {
                    var sum1 = ia[0] * 5 + ia[1] * 4 + ia[2] * 3 + ia[3] * 2 + ia[4] * 7 + ia[5] * 6 + ia[6] * 5 + ia[7] * 4 + ia[8] * 3 + ia[9] * 2;
                    var sum2 = 11 - sum1 % 11;
                    switch (sum2)
                    {
                        case 11: return ia[10] == 0 ? true : false;
                        case 10: return ia[10] == 9 ? true : false;
                        default: return ia[10] == sum2 ? true : false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
