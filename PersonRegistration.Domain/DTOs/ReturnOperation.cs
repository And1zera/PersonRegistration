using System;
using System.Collections.Generic;
using System.Text;

namespace PersonRegistration.Domain.DTOs
{
    public class ReturnOperation<T>
    {
        public T Informacao { get; set; }
        public List<string> Mensagens { get; set; }
        public bool? Erro { get; set; }
    }
}
