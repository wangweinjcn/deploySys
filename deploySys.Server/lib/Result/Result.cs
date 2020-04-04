﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;
namespace Ace
{
    public class Result 
    {
        int _status = (int)ResultStatus.OK;
        public Result()
        {
        }
        
        public Result(ResultStatus status)
        {
            this._status = (int)status;
        }

        public Result(ResultStatus status, string msg)
        {
            this.Status = (int)status;
            this.Msg = msg;
        }
        public Result(int statusCode, string msg)
        {
            this.Status = statusCode;
            this.Msg = msg;
        }
        public string toJsonString()
        {
            return JsonHelper.Serialize(this);

        }

        public int Status { get { return this._status; } set { this._status = value; } }
        public string Msg { get; set; }

        public static Result CreateResult(string msg = null)
        {
            Result result = CreateResult(ResultStatus.OK, msg);
            return result;
        }
        public static Result CreateResult(ResultStatus status, string msg = null)
        {
            Result result = new Result(status);
            result.Msg = msg;
            return result;
        }

        public static Result<T> CreateResult<T>(T data)
        {
            Result<T> result = CreateResult<T>(ResultStatus.OK, data);
            return result;
        }
        public static Result<T> CreateResult<T>(ResultStatus status)
        {
            Result<T> result = CreateResult<T>(status, default(T));
            return result;
        }
        public static Result<T> CreateResult<T>(ResultStatus status, T data)
        {
            Result<T> result = new Result<T>(status);
            result.Data = data;
            return result;
        }

    }
}
