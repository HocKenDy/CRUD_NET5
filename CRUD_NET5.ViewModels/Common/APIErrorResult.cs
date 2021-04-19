using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_NET5.ViewModels.Common
{
    public class APIErrorResult<T> : APIResult<T>
    {
        public string[] ValidationError { get; set; }
        public APIErrorResult()
        {
            IsSuccessed = false;
        }
        public APIErrorResult(string messge)
        {
            IsSuccessed = false;
            Message = messge;
        }

        public APIErrorResult(string [] validationError)
        {
            IsSuccessed = false;
            ValidationError = validationError;
        }
    }
}
