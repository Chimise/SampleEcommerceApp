using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Commerce.Api.Common
{
    public class GenericResponse<TResponse>
    {
        public TResponse? Data { get; set; }
        public bool Success { get; set; }
        public string[]? Errors { get; set;}
        public string DisplayMessage { get; set; }

        private const string _defaultSuccessMessage = "Successful";

        private const string _defaultErrorMessage = "Failed";
        protected GenericResponse(TResponse? response, string? successMessage)
        {
            this.Data = response;
            this.Success = true;
            this.DisplayMessage = successMessage ?? _defaultSuccessMessage;
        }

        protected GenericResponse(string[] errors)
        {
            this.Errors = errors;
            this.Success = false;
            this.DisplayMessage = errors.FirstOrDefault() ?? _defaultErrorMessage;
        }

        public static GenericResponse<TResponse> Ok(TResponse? response = default, string successMessage = _defaultSuccessMessage)
        {
            return new GenericResponse<TResponse>(response, successMessage);
        }

        public static GenericResponse<TResponse> Error(params string[] errors)
        {
            return new GenericResponse<TResponse>(errors);
        }

    }

    public  class GenericResponse : GenericResponse<object>
    {
        private GenericResponse(object? data, string? successMessage) : base(data, successMessage)
        {

        }

        private GenericResponse(string[] errors) : base(errors)
        {

        }
    }
}
