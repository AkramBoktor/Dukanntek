using System;
using System.Collections.Generic;

namespace Dukkantek.Domain.ViewModels.General
{
    public class GeneralResponse<T> 
    {
        public GeneralResponse()
        {
            Data = Activator.CreateInstance<T>();
        }
        public bool IsSucceeded { get; set; }
        public List<Error> Errors { get; set; }
        //public string Message { get; set; }
        public T Data { get; set; }
    }
}
