using Masiv.RouletteProject.Model.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masiv.RouletteProject.Model.Base
{
    public class Response<T>
    {
        public bool status { get; set; }
        public int code { get; set; }
        public String message { get; set; }
        public T body { get; set; }

        public void buildResponseOk(T body, String message) {
            this.status = true;
            this.code = Convert.ToInt32(ResponseApiCodes.OK_CODE);
            this.body = body;
            this.message = message;
        }

        public void buildResponseError(String message)
        {
            this.status = true;
            this.code = Convert.ToInt32(ResponseApiCodes.BAD_REQUEST);
            this.message = message;
        }
    }
}
