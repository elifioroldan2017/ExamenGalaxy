using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.GestionLibros.DTO
{
    public class BaseResponse
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
        public int? ErrorCode { get; set; }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T? Result { get; set; }
    }
}
