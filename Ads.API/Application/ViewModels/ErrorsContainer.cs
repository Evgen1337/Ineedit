using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ads.API.Application.ViewModels
{
    public class ErrorsContainer
    {
        public ErrorsContainer(IReadOnlyCollection<Exception> errors)
        {
            Errors = errors
                .Select(s => new Error(s))
                .ToList();
        }

        public int Count => Errors.Count;

        public List<Error> Errors { get; }

        public class Error
        {
            public Error(Exception exception)
            {
                Type = exception.GetType().Name;
                Message = exception.Message;
            }

            public string Type { get; }
            public string Message { get; }
        }

        public void Add(Exception exception) => 
            Errors.Add(new Error(exception)); 
    }

    public static class ErrorsContainerExtensions
    {
        public static ErrorsContainer ToErrorsContainer(this IReadOnlyCollection<Exception> items) =>
            new ErrorsContainer(items);

        public static ErrorsContainer ToErrorsContainer(this Exception item) =>
            new ErrorsContainer(new Exception[] { item });
    }
}
