using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTDAPI.ResponseModels
{
    public class BaseResponse
    {
        public string status { get; set; }
        public string error { get; set; }
        public int count { get; set; }
        public int id { get; set; }
    }
}