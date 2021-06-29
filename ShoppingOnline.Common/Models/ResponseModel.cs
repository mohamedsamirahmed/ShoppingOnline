using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShoppingOnline.Common.Models
{
    public class ResponseModel<T>
    {
        public bool ReturnStatus { get; set; }
        public List<String> ReturnMessage { get; set; }
        public Hashtable Errors;
        public T Entity;

        public ResponseModel()
        {
            ReturnMessage = new List<String>();
            ReturnStatus = true;
            Errors = new Hashtable();
        }
    }
}
