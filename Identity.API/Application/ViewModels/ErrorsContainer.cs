using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Identity.API.Application.ViewModels
{
    public class ErrorsContainer
    {
        public ErrorsContainer(IReadOnlyCollection<Exception> errors)
        {
            Errors = errors
                .Select(s => new Error(s.GetType().Name, s.Message))
                .ToArray();
        }

        public ErrorsContainer(IReadOnlyCollection<Error> errors)
        {
            Errors = errors;
        }

        public int Count => Errors.Count();

        public IReadOnlyCollection<Error> Errors { get; }

        public class Error
        {
            public Error(string type, string message)
            {
                Type = type;
                Message = message;
            }

            public string Type { get; }
            public string Message { get; }
        }
    }

    public static class ErrorsContainerExtensions
    {
        public static ErrorsContainer ToErrorsContainer(this IReadOnlyCollection<Exception> items) =>
            new ErrorsContainer(items);

        public static ErrorsContainer ToErrorsContainer(this Exception item) =>
            new ErrorsContainer(new Exception[] { item });

        public static ErrorsContainer ToErrorsContainer(this IEnumerable<IdentityError> item) =>
            new ErrorsContainer(item.Select(s => new ErrorsContainer.Error(s.Code, s.Description)).ToList());
    }
}
