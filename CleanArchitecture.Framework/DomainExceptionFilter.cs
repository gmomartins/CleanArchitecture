using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure;

namespace CleanArchitecture.Framework
{
    /// <summary>
    /// Exception Filter para tratar as exceções que ocorrem no sistema
    /// </summary>
    public sealed class DomainExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            if (context.Exception is DomainException)
            {
                DomainException domainException = context.Exception as DomainException;
                string json = JsonConvert.SerializeObject(new ErrorResult(domainException), jsonSettings);

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            else if (context.Exception is CleanArchitecture.Application.ApplicationException)
            {
                CleanArchitecture.Application.ApplicationException applicationException = context.Exception as CleanArchitecture.Application.ApplicationException;
                string json = JsonConvert.SerializeObject(new ErrorResult(applicationException), jsonSettings);

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            else if (context.Exception is InfrastructureException)
            {
                InfrastructureException infrastructureException = context.Exception as InfrastructureException;
                string json = JsonConvert.SerializeObject(new ErrorResult(infrastructureException), jsonSettings);

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                string json = JsonConvert.SerializeObject(new ErrorResult(context.Exception), jsonSettings);

                context.Result = new ObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }

    public class ErrorResult
    {
        public List<string> Errors { get; set; }
        public ErrorResult(Exception ex)
        {
            this.Errors = new List<string>();
            while (ex != null)
            {
                this.Errors.Add(ex.Message);
                ex = ex.InnerException;
            }
        }
    }

}
